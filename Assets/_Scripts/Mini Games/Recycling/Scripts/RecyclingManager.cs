using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecyclingManager : MonoBehaviour
{
    [Header("Preferences")]
    [Space]
    public float detectionRadius = 2f;
    public float gameDuration = 20f;
    public float selectionThreshold = 0.95f;
    public float dragSmooth = 10f;
    public ShakeSettings multiplierScaleAnimationSettings;
    public ShakeSettings multiplierShakeRotationSettings;

    [Header("References")]
    [Space]
    [SerializeField] GameObject[] litter;
    [SerializeField] PlayerLife playerLife;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Camera mainCam;
    [SerializeField] Camera recyclingCam;
    [SerializeField] Image timerFiller;
    public TextMeshProUGUI multiplierText;
    [SerializeField] GameObject character;


    [Header("Audio")]
    [Space]
    public AudioSource correctBinSound;
    public AudioSource wrongBinSound;

    private GameObject selectedGarbage;
    private Litter selectedLitter;
    private Bin nearestBin;
    private GameObject highlightedBin;
    private float timer;
    private float multiplier = 1.0f;
    private bool GameIsOver = false;
    private Vector3 litterOriginalPos;
    private GameObject currentLitter;
    private PlayerScript playerScript;
    private bool CanDragLitter;
    private bool StartTimer;





    void Update()
    {

        if (GameIsOver) return;

        if (!StartTimer) return;

        timer -= Time.deltaTime;

        timerFiller.fillAmount = timer / gameDuration;

        if (timer <= 0)
        {
            timer = 0;
            OnMiniGameEnd();
        }


        if (!CanDragLitter) return;

        DragLitterWithTouch();
    }

    private void SpawnNewLitter()
    {
        CanDragLitter = false;
        int randomLitterIndex = UnityEngine.Random.Range(0, litter.Length);
        currentLitter = Instantiate(litter[randomLitterIndex], new Vector3(-5.38000011f, -0.109999999f, 48.1800003f), litter[randomLitterIndex].transform.rotation);
        currentLitter.GetComponent<Litter>().canMove = false;
        currentLitter.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        Tween.Position(currentLitter.transform, new Vector3(-5.23999977f, 2.21600008f, 50.32f), 0.5f).OnComplete(target: this, target =>
        {
            litterOriginalPos = currentLitter.transform.position;
            CanDragLitter = true;
        });
    }
    private void MoveLitterToOriginalPos()
    {
        CanDragLitter = false;
        Tween.Position(currentLitter.transform, litterOriginalPos, 0.5f).OnComplete(target: this, target => CanDragLitter = true);

    }

    private void DragLitterWithTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = GetTouchWorldPosition();
            touchPosition.z = selectedGarbage != null ? selectedGarbage.transform.position.z : transform.position.z;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TrySelectGarbage();
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (selectedGarbage != null)
                    {
                        MoveSelectedGarbage(touchPosition);
                        CheckForNearbyBin();
                    }
                    break;

                case TouchPhase.Ended:
                    if (selectedGarbage != null)
                    {
                        CheckIfCorrectBin();

                    }
                    break;
            }
        }
    }
    public void OnMiniGameStart(PlayerScript player)
    {
        playerScript = player;

        playerScript.setMovementStatus(false);
        mainCam.gameObject.SetActive(false);
        recyclingCam.gameObject.SetActive(true);
        character.gameObject.SetActive(false);

        UIManager uIManager = UIManager.instance;


        uIManager.recyclingCanvas.SetActive(true);
        uIManager.LitterAmountText.gameObject.SetActive(false);
        uIManager.triggerUICanvas.SetActive(false);
        uIManager.totalScore.SetActive(false);
        uIManager.mainGameCanvas.SetActive(false);
        Tween.Alpha(uIManager.recyclingFadeInPanel, 1, 0, 0.5f, Ease.Linear).OnComplete(target: this, target => target.SpawnNewLitter());

        timer = gameDuration;
        UpdateMultiplierUI();


        StartTimer = true;

    }
    public void OnMiniGameEnd()
    {
        if (playerScript != null)
        {
            DeselectGarbage();

            if (playerLife.Health <= 0)
            {
                GameIsOver = true;
            }
           
            StartTimer = false;
            UIManager uIManager = UIManager.instance;
            playerScript.setMovementStatus(true);

            Tween.Alpha(uIManager.recyclingFadeInPanel, 1, 0, 0.5f, Ease.Linear);
            recyclingCam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);
            // uIManager.joystickCanvas.SetActive(true);
            uIManager.recyclingCanvas.SetActive(false);
            uIManager.triggerUICanvas.SetActive(true);
            uIManager.LitterAmountText.gameObject.SetActive(true);
            uIManager.totalScore.SetActive(true);
            uIManager.mainGameCanvas.SetActive(true);
            Destroy(currentLitter);
            currentLitter = null;
            playerLife.ResetHealth();
            GameIsOver = false;
            float temp = (float)playerScript.litterCollectedAmount;
            temp *= multiplier;
            GameManager.GetScoreManager().AddToScore(temp);
            playerScript.ResetCollectedLitter();
            //triggerUICanvas.SetActive(false);
            character.SetActive(true);

        }
    }

    Vector3 GetTouchWorldPosition()
    {
        Vector3 screenPos = Input.GetTouch(0).position;
        return recyclingCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(transform.position.z - recyclingCam.transform.position.z)));
    }

    //void TrySelectGarbage()
    //{
    //    RaycastHit hit;
    //    Ray ray = recyclingCam.ScreenPointToRay(Input.GetTouch(0).position);

    //    if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Garbage"))
    //    {
    //        selectedGarbage = hit.collider.gameObject;
    //        selectedLitter = selectedGarbage.GetComponent<Litter>();

    //        HighlightObject(selectedGarbage, true);
    //    }
    //}

    void TrySelectGarbage()
    {
        Ray ray = recyclingCam.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Garbage"))
            {
                Vector3 directionToHit = (hit.point - ray.origin).normalized;
                float dotProduct = Vector3.Dot(ray.direction, directionToHit);

                if (dotProduct >= selectionThreshold)
                {
                    selectedGarbage = hit.collider.gameObject;
                    selectedLitter = selectedGarbage.GetComponent<Litter>();

                    HighlightObject(selectedGarbage, true);
                    break;
                }
            }
        }
    }

    void MoveSelectedGarbage(Vector3 position)
    {
        selectedGarbage.transform.position = Vector3.Lerp(selectedGarbage.transform.position, position, Time.deltaTime * dragSmooth);
    }

    void CheckForNearbyBin()
    {
        Collider[] colliders = Physics.OverlapSphere(selectedGarbage.transform.position, detectionRadius);
        GameObject nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Bin>(out var bin))
            {
                float distance = Vector3.Distance(selectedGarbage.transform.position, bin.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = bin.gameObject;
                    nearestBin = bin;
                }
            }
        }

        UpdateBinHighlight(nearest);
    }

    void UpdateBinHighlight(GameObject nearest)
    {
        if (highlightedBin != nearest)
        {
            HighlightObject(highlightedBin, false);
            HighlightObject(nearest, true);
            highlightedBin = nearest;
        }
    }

    void CheckIfCorrectBin()
    {
        if (nearestBin != null && selectedLitter != null)
        {
            if (nearestBin.litterType == selectedLitter.litterType)
            {
                playerScript.litterCollectedAmount -= 1;

                scoreManager.AddScoreOnBinned();

                if (playerScript.litterCollectedAmount > 0)
                {
                    
                    Tween.PunchScale(multiplierText.transform, multiplierScaleAnimationSettings);
                    Debug.Log("Correct bin!");
                    correctBinSound.Play();
                    nearestBin.AnimateRecycling(selectedLitter.transform);
                    SpawnNewLitter();
                    multiplier += 0.1f;
                    UpdateMultiplierUI();
                }
                else
                {
                    playerScript.collectedLitterLocations.Clear();
                    OnMiniGameEnd();
                }
                
            }
            else
            {
                Tween.ShakeLocalRotation(multiplierText.transform, multiplierShakeRotationSettings);
                Debug.Log("Wrong bin! Try again.");
                wrongBinSound.Play();

                multiplier = 1.0f;
                UpdateMultiplierUI();
                OnWrongBinSelection();
            }

            DeselectGarbage();
        }

    }

    void OnWrongBinSelection()
    {
        //playerLife.DecreasePlayerHealth();
        //if (playerLife.Health <= 0)
        //{
        //    OnMiniGameEnd();
        //}
        //else
        //{
        //    MoveLitterToOriginalPos();
        //}
        MoveLitterToOriginalPos();
    }

    void UpdateMultiplierUI()
    {

        multiplierText.text = multiplier.ToString("F1") + "X";
    }

    void DeselectGarbage()
    {
        HighlightObject(selectedGarbage, false);
        HighlightObject(highlightedBin, false);
        selectedGarbage = null;
        selectedLitter = null;
        nearestBin = null;
        highlightedBin = null;
    }

    void HighlightObject(GameObject obj, bool highlight)
    {
        if (obj != null)
        {
            obj.GetComponent<Outline>().enabled = highlight;
        }
    }

    //======Debug=============
    void OnDrawGizmos()
    {
        if (selectedGarbage != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(selectedGarbage.transform.position, detectionRadius);
        }
    }
}

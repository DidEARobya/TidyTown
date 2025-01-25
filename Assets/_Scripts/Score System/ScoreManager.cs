
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    /*[SerializeField] int _CardboardScore = 0;
    [SerializeField] int _FoodScore = 0;
    [SerializeField] int _GeneralScore = 0;
    [SerializeField] int _CansScore = 0;
    [SerializeField] int _SpillageScore = 0;*/

    public bool ReadyForStore;
    public int totalScore { get; private set; } = 0;
    [SerializeField] private int scorePerLitter = 10;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PlayerScript player;
    private RecyclingManager rManager;

    // updated 21/10/24 Ben S: interaction with the player

    public void Start()
    {
        //finds the player script on start
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerScript>();
            if (player == null)
            {
                Debug.LogError("playerScript is non-existent");
                return;
            }
        }
        if (scoreText == null) return;
        UpdateScoreText();
    }
   /* public void LitterValuCalculator(Collider target)
    {

        if (target.GetComponent<Litter>())
        {
            var currenctLitter = target.GetComponent<Litter>();
            int scoreToAdd = 0;

            

            switch (currenctLitter.litterType)
            {
                case LitterType.Cardboard:
                    scoreToAdd = _CardboardScore;
                    GameManager.instance.PlayerScore += _CardboardScore;
                    //Debug.Log("Cardboard Litter");
                    break;

                case LitterType.FoodGarden:
                    scoreToAdd = _FoodScore;
                    GameManager.instance.PlayerScore += _FoodScore;
                    //Debug.Log("Food Garden Litter");
                    break;
                case LitterType.GeneralWaste:
                    scoreToAdd = _GeneralScore;
                    GameManager.instance.PlayerScore += _GeneralScore;
                    //Debug.Log("General Waste Litter");
                    break;
                case LitterType.CansBottles:
                    scoreToAdd = _CansScore;
                    GameManager.instance.PlayerScore += _CansScore;
                    //Debug.Log("CansBottles Litter");
                    break;
                case LitterType.Spillage:
                    scoreToAdd = _SpillageScore;
                    GameManager.instance.PlayerScore += _SpillageScore;
                    //Debug.Log("Spillage Litter");
                    break;
            }


            totalScore += scoreToAdd;
            //Debug.Log("total score updated" + totalScore);
            UpdateScoreText();
        }
    }*/

    public void AddScoreOnBinned()
    {
        totalScore += scorePerLitter;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText == null) return;
        scoreText.text = totalScore.ToString();
    }

    public void AddToScore(float amount)
    {
        totalScore += (int)amount;
        UpdateScoreText();
    }

    public void StoreScore()
    {
        if (ReadyForStore)
        {
            GameManager.instance.StoredScore += GameManager.instance.PlayerScore;
            GameManager.instance.PlayerScore = 0;
        }
    }


    public void endgame()
    {

        PlayerPrefs.SetInt("FinalScore", totalScore);
        PlayerPrefs.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}

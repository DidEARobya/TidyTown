/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 *
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938
 */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float baseSpeed;
    private float movementSpeed;
    private int heldBlackLitter = 0;
    private int heldRedLitter = 0;
    private int heldBeigeLitter = 0;
    public int HeldBlackLitter
    {
        get { return heldBlackLitter; }
    }
    public int HeldRedLitter
    {
        get { return heldRedLitter; }
    }
    public int HeldBeigeLitter
    {
        get { return heldBeigeLitter; }
    }

    void Start()
    {
        movementSpeed = baseSpeed;
    }

    void Update()
    {
        Move();
        //OnInteractCollision();

    }

    public void AdjustLitter(LitterType type)
    {
        switch(type)
        {
            case LitterType.Beige:
                heldBeigeLitter += Configuration.LitterValue;
                break;
            case LitterType.Black:
                heldBlackLitter += Configuration.LitterValue;
                break;
            case LitterType.Red:
                heldRedLitter += Configuration.LitterValue;
                break;
        }
    }

    public void SetLitter(LitterType type, int amount)
    {
        switch (type)
        {
            case LitterType.Beige:
                heldBeigeLitter = amount;
                break;
            case LitterType.Black:
                heldBlackLitter = amount;
                break;
            case LitterType.Red:
                heldRedLitter = amount;
                break;
        }
    }

    public void ClearLitter(LitterType type) => SetLitter(type, 0);

    public void MultiplyBaseSpeed(float multiplier)
    {
        movementSpeed = baseSpeed * multiplier;
    }
    public void ResetMovementSpeed()
    {
        movementSpeed = baseSpeed;
    }
    private void Move()
    {
        Debug.Log("Move1");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;
        move.Normalize();

        transform.position += move * movementSpeed * Time.deltaTime;
    }

    // Checks for collision with IInteractable object. If the object is litter, checks if the player can carry more
    //private void OnInteractCollision()
    //{
    //    Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f, interactLayer);

    //    foreach (Collider hit in hits)
    //    {
    //        IInteractable target = hit.gameObject.GetComponent<IInteractable>();

    //        if(target == null)
    //        {
    //            continue;
    //        }

    //        target.OnInteract(this);
    //    }
    //}
}

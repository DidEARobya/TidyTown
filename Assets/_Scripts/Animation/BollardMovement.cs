using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BollardMovement : MonoBehaviour
{

    private Animator Bollard;
    public float delay = 5f;
    private bool BollardUp = true;
    // Start is called before the first frame update
    void Start()
    {
        Bollard = GetComponent<Animator>();
        StartCoroutine(AnimateOnTimer());
    }

    IEnumerator AnimateOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if (BollardUp)
            {
                Bollard.SetTrigger("BollardD");
            }
            else
            {
                Bollard.SetTrigger("BollardU");
            }

            BollardUp = !BollardUp;
        }
    }
    
}

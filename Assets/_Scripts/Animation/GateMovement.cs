using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMovement : MonoBehaviour
{
    private Animator gate1;
    private Animator gate2;
    public float delay = 5f;
    private bool gateOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        gate1 = GetComponent<Animator>();
        gate2 = GetComponent<Animator>();
        StartCoroutine(AnimateOnTimer());
    }

    IEnumerator AnimateOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            if (gateOpen)
            {
                gate1.SetTrigger("Open");
                gate2.SetTrigger("Open");
            }
            else
            {
                gate1.SetTrigger("Close");
                gate2.SetTrigger("Close");
            }
            gateOpen = !gateOpen;
        }
    }
    
}

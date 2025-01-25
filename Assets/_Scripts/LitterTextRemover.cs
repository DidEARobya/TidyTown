using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LitterTextRemover : MonoBehaviour
{

    //This script is used to remove the text that shows how much litter the player has collected
    public TMP_Text LitterAmountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //when the player pressed the space key, the text for the litter collected will be put to 0
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LitterAmountText.text = "0";
            Debug.Log("Space key was pressed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if the player collides with the bin, the text for the litter collected will be put to 0

        if (collision.gameObject.tag == "Bins")
        {
            LitterAmountText.text = "0";
        }
    }
}

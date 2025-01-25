using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    //text for the stars
    public TextMeshProUGUI fpsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //display the fps
        fpsText.text = "FPS: " + (1.0f / Time.deltaTime).ToString("0.0");
    }
}

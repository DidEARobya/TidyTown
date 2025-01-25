using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parkSoundPlayer : MonoBehaviour
{
    private bool flag = false;
    private Transform player; 
    [SerializeField] AudioSource parkSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ParkSound();
    }
    
    public void ParkSound()
    {
        if (Vector3.Distance(transform.position, player.position) < 33 && flag == false)
        {
            parkSound.Play();
            flag = true;
        }
        else if (Vector3.Distance(transform.position, player.position) > 33 )
        {
            parkSound.Stop();
            flag = false;
        }
    }
}

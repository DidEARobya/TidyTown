using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingMode : MonoBehaviour
{
    [SerializeField] private RecyclingManager recyclingManager;
    [SerializeField] public bool debugMode;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();

            

            if (debugMode)
            {
                StartMiniGame(playerScript);
            }
            else
            {

                if (playerScript.litterCollectedAmount > 0)
                {
                    StartMiniGame(playerScript);
                }
            }

        }

    }

    private void StartMiniGame(PlayerScript playerScript)
    {
        if (recyclingManager != null)
        {
            recyclingManager.OnMiniGameStart(playerScript);
        }
        else
        {
            Debug.LogError("RecyclingManager is not assigned in the inspector");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Ashkan Soroor (HS)
public class PlayerSpawner : MonoBehaviour
{
    
    public List<Transform> spawnPoints = new List<Transform>();
    private PlayerScript _player;

    private void Start()
    {
        _player = GameManager.GetPlayerScript();
    }

    private void Update()
    {
        if (_player.respawn)
        {
           // print("Respawn");
            _player.CanMove = false;
            //_player.gameObject.transform.position = spawnPoints[0].position;
            _player.PlayerAnimator.SetBool("isDied", true);
            var playerInput = _player.GetComponent<PlayerInput>();
            StartCoroutine(moveAgain());
            _player.respawn = false;
        }
    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(8f);
        _player.CanMove = true;
        _player.PlayerAnimator.SetBool("isDied", false);
    }
}

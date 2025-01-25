using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    public void StartGame()
    {
        //starts the next scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //quits the game
        Application.Quit();
    }

    public void TogglePause()
    {
        //pauses the game
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    public void ReturnToMenu()
    {
        //loads the previous scene
        SceneManager.LoadScene(0);
        //if game is paused it will unpause
        Time.timeScale = 1;
    }

    public void CheckCredits()
    {
        //loads the credits scene
        SceneManager.LoadScene(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

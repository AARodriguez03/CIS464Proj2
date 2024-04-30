using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isPaused = false;                   //bool for if game is paused 
    public GameObject PauseMenu;
    public int scene;                                      //This is used for the reset function to make sure the proper scene is reloaded
    public float resetTime = 0f;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))              // if escape is pressed 
        {
            if(isPaused == true)                           //game is already paused 
            {
                Resume();                                  //resume the game
            }
            else
            {
                Pause();                                   //pause the game 
            }

        }
        if(!isPaused)
        {
            scene = SceneManager.GetActiveScene().buildIndex;     //this makes sure the scene to be reloaded is correct if the level is reset
        }
        if (Input.GetKeyDown(KeyCode.R))                   // checks the reset button
        {
            if (resetTime != 0f) { Reset(); }              // if its been tapped twice, reset the scene
            else{ resetTime = 0.7f; }                      // if its been tapped twice, prepare for the second tap.
        }
        if (resetTime > 0f) { resetTime = resetTime - Time.deltaTime; } // subtract deltatime from the reset timer to make sure that the button must be pressed twice in quick succession.
        if(resetTime < 0f) { resetTime = 0f; }             // if reset time goes below 0 its set to 0

    }

    public void Resume()                                   //resume the game
    { 
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;                               //return game to normal speed
        
    }

    void Pause()                                           //pause the game
    {
        
        isPaused = true;                                   //paused 
        scene = SceneManager.GetActiveScene().buildIndex;  //the current scene is obtained and stored for use in resetting.
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;                               //freeze game


    }

    public void GoToMain()                                 // if main menu is selected 
    {
        isPaused = false;                                  //no longer paused 
        if (PlayerPrefs.GetInt("TimeAttack") == 1)
        {
            PlayerPrefs.SetInt("TimeAttack", 0);               //set a time attack to 0 so no timer is used 
        }
        Time.timeScale = 1f;                               //revert back to normal 
        SceneManager.LoadScene("Menu");                    //load the main menu scene 
    }

    public void Reset()
    {

        SceneManager.LoadScene(scene);                     // the current level is reloaded
        Time.timeScale = 1f;                               // this makes sure using the button from the pause menu works properly
        isPaused = false;

    }

    public void Quit()                                     // if quit is selected
    {
        Application.Quit();                                //quit the application
    }


}

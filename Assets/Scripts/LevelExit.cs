using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour             //script for level exit 
{
    public GameObject CongratsScreen;
    public GameObject StoryScreen;
    public int nextScene;                     // pulls the current scene index to know what index to go to next
    private void Start() 
    {
        nextScene = SceneManager.GetActiveScene().buildIndex+1;

    }
    private void OnTriggerEnter2D(Collider2D other) //whenever any collision is detected 
    {
        GameObject hitObject = other.gameObject;     //grab the object 

        if (hitObject.tag == "Player")              
        {
            if (nextScene == 4)                    //checks if the player is at the final level of the build
            {
                StoryScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (nextScene == 12)                    //checks if the player is at the final level of the build
            {
                CongratsScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                if (PlayerPrefs.GetInt("TimeAttack") == 1) //time attack active
                {
                    GameObject Timer = GameObject.FindGameObjectWithTag("Timer");
                    Timer.GetComponent<TimerScript>().StopTimer();
                }
                
                NextScene();      //loads the next level
            }
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour             //just enter the level when the button is pressed 
{
    public void EnterLevel(string selectedLevel)          //enter a level with the appropriate string/level name ( or scene name) 
    {
        PlayerPrefs.SetInt("TimeAttack", 1);             //set a TimeAttack thing to 1 
        SceneManager.LoadScene(selectedLevel);

    }
}

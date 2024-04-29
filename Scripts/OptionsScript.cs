using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsScript : MonoBehaviour // use to toggle full screen or not. Not sure what else to implement as of right now
{
    public bool cheatInfinite;             //trigger for cheat boolean
    public Toggle cheatToggle;             //toggle for cheat
    public Toggle hintToggle;
    private bool fullscreen;               //same for fullscreen 
    public bool hintsEnabled; 

    private void Start()
    {
        cheatInfinite = PlayerPrefs.GetInt("InfiniteCard", 0) == 1;  //grab the references of the saved preferences 
        cheatToggle.isOn = cheatInfinite;     //toggle them based on the value 
        hintsEnabled = PlayerPrefs.GetInt("Hints", 0) == 1;
        hintToggle.isOn = hintsEnabled;
    }


    public void SetScreen(bool trigger)         //will make the screen full or not depending on the boolean
    {
        if (fullscreen) Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else Screen.fullScreenMode = FullScreenMode.Windowed;

    }
    public void SetHints(bool trigger)
    {
        hintsEnabled = trigger;
        PlayerPrefs.SetInt("Hints", trigger ? 1 : 0);
        PlayerPrefs.Save();

    }    
    public void SetInfiniteCard(bool trigger)   //make the game have infinite cards no matter what 
    {
        cheatInfinite = trigger;
        PlayerPrefs.SetInt("InfiniteCard", trigger ? 1 : 0); //save the int between scene reloads as well for general use
        PlayerPrefs.Save();
    }
}

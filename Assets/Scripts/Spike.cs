using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour             // script for spikes to reset the player
{
    public int scene;                     // pulls the current scene index
    private void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter2D(Collider2D other) //whenever any collision is detected 
    {
        GameObject hitObject = other.gameObject;     //grab the object 

        if (hitObject.tag == "Player")              //if player 
        {
            SceneManager.LoadScene(scene);      //reloads the level

        }
    }
}
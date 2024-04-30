using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class HighlightLevel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  //highlight level text
{
    public TextMeshProUGUI levelText;                   //get the assigned text 
    public TextMeshProUGUI bestText;                    //for timer
    public float bestTime;                             //saved time for the level 
    public string sceneName; 
    public void Start()                                //disable it 
    {
        levelText.enabled = false;                    //set the boxes to false so they dont appear 
        bestText.enabled = false;
        
    }
    public void OnPointerEnter(PointerEventData enterData) //whenever the mouse is over the level
    {
        bestTime = PlayerPrefs.GetFloat("BestTime" + sceneName, Mathf.Infinity); //get the score 

        if (bestTime == Mathf.Infinity)   // if no score
        {
            bestText.text = "Best Time: --.--"; //display nothing
        }
        else
        {
            bestText.text = "Best Time: " + bestTime.ToString("F2");  //otherwise get the score 
        }
        levelText.enabled = true;                          //display it
        bestText.enabled = true;
    }

    public void OnPointerExit(PointerEventData exitData)   //when it leaves the button
    {
        levelText.enabled = false;                         //hide it
        bestText.enabled = false;
    }
}

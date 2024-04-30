using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public bool setTimer;
    private float startTime;
    public TMP_Text timerText;
    private OptionsScript uh;
    void OnEnable()
    {
        if (gameObject.activeSelf == true)
        {
            setTimer = true;
            StartTimer();
            //uh = GameObject.Find
        }

    }

    void Update()
    {
        if (!setTimer)
        {
            return;
        }

        float currentTime = Time.time - startTime;
        DisplayTime(currentTime);


    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void StopTimer()
    {
        gameObject.SetActive(false);
        NewHighScore(Time.time - startTime);
    }

    void DisplayTime(float currTime)
    {
        timerText.text = currTime.ToString("F2");
    }

    void NewHighScore(float time)
    {
        if (PlayerPrefs.GetInt("InfiniteCard") == 1) // if play is CHEATING
        {
            return;
        }
        float bestTime = PlayerPrefs.GetFloat("BestTime" + SceneManager.GetActiveScene().name, Mathf.Infinity);
        if (time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime" + SceneManager.GetActiveScene().name, time);
            PlayerPrefs.Save();
        }
    }
}

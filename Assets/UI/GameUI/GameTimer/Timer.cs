using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float timeLeft;

    public static Timer Instance { get; private set; }

    [SerializeField] private TMP_Text text;

    void Start()
    {
        timeLeft = 300f;
    }
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        Debug.Log(text);
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            OpenVictoryScreen();
        }
        TimeSpan time = TimeSpan.FromSeconds(timeLeft);
        if (timeLeft >= 0) 
        {
            if (timeLeft <= 10)
            {
                text.color = Color.red;
            }
            else if (timeLeft <= 60)
            {
                text.color = Color.yellow;
            }
            if (time.Seconds > 9f)
            {
                if (time.Milliseconds > 99f)
                {
                    text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + "." + time.Milliseconds.ToString();
                } 
                else if (time.Milliseconds > 9f)
                {
                    text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ".0" + time.Milliseconds.ToString();
                } 
                else
                {
                    text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ".00" + time.Milliseconds.ToString();
                }
            } 
            else
            {
                
                if (time.Milliseconds > 99f)
                {
                    text.text = time.Minutes.ToString() + ":0" + time.Seconds.ToString() + "." + time.Milliseconds.ToString();
                } 
                else if (time.Milliseconds > 9f)
                {
                    text.text = time.Minutes.ToString() + ":0" + time.Seconds.ToString() + ".0" + time.Milliseconds.ToString();
                } 
                else
                {
                    text.text = time.Minutes.ToString() + ":0" + time.Seconds.ToString() + ".00" + time.Milliseconds.ToString();
                }
            }
        } else
        {
            text.text = "0:00.000";
        }
    }

    public void OpenVictoryScreen() => SceneManager.LoadScene("VictoryMenuScene");

}
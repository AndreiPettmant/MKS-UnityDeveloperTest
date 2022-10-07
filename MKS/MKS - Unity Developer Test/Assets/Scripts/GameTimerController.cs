using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerController : MonoBehaviour
{
    [SerializeField][Range(0.5f, 3f)] private float increaseAmount;
    [SerializeField] private Text GameSessionText;

    private float maxGameSession = 180;
    private float minGameSession = 60;
    private float amount;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("Timer") == 0)
        {
            minGameSession = 60;
            PlayerPrefs.SetFloat("Timer", amount);
            DisplayTime(PlayerPrefs.GetFloat("Timer"));
            amount = minGameSession;
        }
        else
        {
            DisplayTime(PlayerPrefs.GetFloat("Timer"));
            amount = PlayerPrefs.GetFloat("Timer");
        }
    }

    public void Increase()
    {  
        while (amount != maxGameSession)
        {
            amount += increaseAmount;
            DisplayTime(amount);
            PlayerPrefs.SetFloat("Timer", amount);
            break;
        }
    }

    public void Decrease()
    {
        while (amount != minGameSession)
        {
            amount -= increaseAmount;
            DisplayTime(amount);
            PlayerPrefs.SetFloat("Timer", amount);
            break;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= minGameSession)
        {
            timeToDisplay = minGameSession;
            PlayerPrefs.GetFloat("Timer");
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        GameSessionText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}

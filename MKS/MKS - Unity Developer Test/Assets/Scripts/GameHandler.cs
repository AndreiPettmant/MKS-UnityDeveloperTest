using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private Text GameTimer;
    [SerializeField] private Button[] buttons;
    [SerializeField] private ShowGameOver showGameOver;
    private float timer;
    public bool isOver;

    private void Awake()
    {
        if(timer == 0)
        { 
            PlayerPrefs.GetFloat("Timer");
            timer = 60;
            UpdateTimer(PlayerPrefs.GetFloat("Timer"));
        }
        else
        {
            PlayerPrefs.GetFloat("Timer");
            timer = PlayerPrefs.GetFloat("Timer");
            UpdateTimer(PlayerPrefs.GetFloat("Timer"));
        }
    }

    private void Start()
    {
        CancelInvoke();
        InvokeRepeating("Countdown", 0f, 1.0f);
    }

    private void UpdateTimer(float timeToUpdate)
    {
        float minutes = Mathf.FloorToInt(timeToUpdate / 60);
        float seconds = Mathf.FloorToInt(timeToUpdate % 60);

        GameTimer.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }

    private void Countdown()
    {
        if (timer > 0f)
        {
            timer -= 1f;
            UpdateTimer(timer);
        }
        else
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        isOver = true;
        showGameOver.fadeIn = true;

        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }
}

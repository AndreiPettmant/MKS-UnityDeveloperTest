using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTimerController : MonoBehaviour
{
    [SerializeField] private float minSpawntime = 5;
    [SerializeField] private float increaseAmount;
    [SerializeField] private Text spawnerTimeText;
    private float amount;


    private void Awake()
    {
        if (PlayerPrefs.GetFloat("SpawnTime", amount) == 0)
        {
            minSpawntime = 5;
            PlayerPrefs.SetFloat("SpawnTime", amount);
            DisplayTime(PlayerPrefs.GetFloat("SpawnTime"));
            amount = minSpawntime;
        }
        else
        {
            DisplayTime(PlayerPrefs.GetFloat("SpawnTime"));
            amount = PlayerPrefs.GetFloat("SpawnTime");
        }
    }

    public void Increase()
    {
        amount += increaseAmount;
        PlayerPrefs.SetFloat("SpawnTime", amount);
        DisplayTime(amount);
    }

    public void Decrease()
    {
        while (amount >= minSpawntime)
        {
            amount -= increaseAmount;
            PlayerPrefs.SetFloat("SpawnTime", amount);

            DisplayTime(amount);
            break;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < minSpawntime)
        {
            timeToDisplay = minSpawntime;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        spawnerTimeText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
    }
}

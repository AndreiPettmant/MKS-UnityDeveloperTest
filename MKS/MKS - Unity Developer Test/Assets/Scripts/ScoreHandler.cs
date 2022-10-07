using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private Text GameScore;
    [SerializeField] private Text Highscore;
    [SerializeField] public int score;

    public void UpdateScore()
    {
        if (GameScore != null && Highscore != null)
        {
            GameScore.text = score.ToString("O = " + score);
            Highscore.text = score.ToString();
        }
    }
}

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
            iTween.PunchScale(GameScore.gameObject, new Vector2(0.1f, 0.1f), 0.4f);
            GameScore.text = score.ToString();
            Highscore.text = score.ToString();

            Debug.Log(score.ToString());
        }
    }
}

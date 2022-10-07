using UnityEngine;

public class ShowGameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public bool fadeIn = false;
    private void ShowUI()
    {
        fadeIn = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
    }
}

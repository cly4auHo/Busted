using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image lvlBar;

    private int score = 0;
    [SerializeField] private int minScoreToWin = 10;

    void Update()
    {
        //lvlBar.fillAmount += (float)score / (float)minScoreToWin;
    }

    public void ScoreUP()
    {
        score++;
    }

    public void ScoreZero()
    {
        score = 0;
    }

    public bool CompletedLVL()
    {
        return score >= minScoreToWin;
    }
}

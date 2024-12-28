using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endManager;
    public bool gameOver;

    private PanelController panelController;
    private TextMeshProUGUI scoreTextComponent;

    private int score;

    [HideInInspector]
    public string lvlUnlock = "levelUnlock";

    private void Awake()
    {
        if (endManager == null)
        {
            endManager = this;
            DontDestroyOnLoad(endManager);
        }
        else
            Destroy(gameObject);
    }

    public void updateScore(int addScore)
    {
        score += addScore;
        scoreTextComponent.text = "Score: " + score.ToString();
    }

    public void StartResolveSequece()
    {
        StopCoroutine(nameof(StartResolveSequece));
        StartCoroutine(ResolveSequece());
    }

    private IEnumerator ResolveSequece()
    {
        yield return new WaitForSeconds(2);
        ResolveGame();
    }

    public void ResolveGame()
    {
        if (gameOver == false)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        // set score
        ScoreSet();

        // active the panel
        panelController.ActiveWin();

        // unlock the next level if it hasn't been unlocked yet
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock, nextLevel);
        }
    }

    public void LoseGame()
    {
        // set score
        ScoreSet();

        // active the panel
        panelController.ActiveLose();
    }

    private void ScoreSet() // make sure call this function before active screens
    {
        PlayerPrefs.SetInt("Score" + SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);
        }
        // reset score
        score = 0;
    }

    public void RegisterPanelController(PanelController panelC)
    {
        panelController = panelC;
    }

    public void RegisterScoreText(TextMeshProUGUI scoreTextComp)
    {
        scoreTextComponent = scoreTextComp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadSceneString(string levelName)
    {
        FadeCanvas.fader.FaderLoadString(levelName);
    }

    public void LoadSceneInt(int levelIndex)
    {
        FadeCanvas.fader.FaderLoadInt(levelIndex);
    }

    public void RestartLevel()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FadeCanvas.fader.FaderLoadInt(SceneManager.GetActiveScene().buildIndex);
    }
}

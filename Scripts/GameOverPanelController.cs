using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverPanelController : MonoBehaviour
{

    public void Restart()
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }
    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("NewMenuScene");
    }
}

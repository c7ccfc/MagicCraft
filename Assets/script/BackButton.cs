using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void ReturnToGame()
    {
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
        SceneManager.LoadScene("GameScene");
    }
}

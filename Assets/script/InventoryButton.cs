using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryButton : MonoBehaviour
{
    public void OpenInventory()
    {
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
        SceneManager.LoadScene("InventoryScene");
    }
}

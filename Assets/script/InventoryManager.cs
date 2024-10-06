using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Magic> playerMagics = new List<Magic>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(Magic newMagic)
    {
        playerMagics.Add(newMagic);
    }

    public void RemoveItem(Magic newMagic)
    {
        if (playerMagics.Contains(newMagic))
        {
            playerMagics.Remove(newMagic);
            Debug.Log(newMagic.magicName + " was removed from the inventory.");
        }
        else
        {
            Debug.LogWarning("Item not found in inventory.");
        }
    }

    public List<Magic> GetItems()
    {
        return playerMagics;
    }
}

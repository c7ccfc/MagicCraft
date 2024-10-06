using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public static List<Magic> playerMagics = new List<Magic>();

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

    public static void AddMagic(Magic newMagic)
    {
        playerMagics.Add(newMagic);
    }

    public void RemoveMagic(Magic newMagic)
    {
        if (playerMagics.Contains(newMagic))
        {
            playerMagics.Remove(newMagic);
            Debug.Log(newMagic.magicName + " was removed from the inventory.");
        }
        else
        {
            Debug.LogWarning("Magic not found in inventory.");
        }
    }

    public void RemoveMagicByName(string magicName)
    {
        Magic magicToRemove = playerMagics.Find(magic => magic.magicName == magicName);
        if (magicToRemove != null)
        {
            playerMagics.Remove(magicToRemove);
            Debug.Log(magicName + " was removed from the inventory.");
        }
        else
        {
            Debug.LogWarning("Magic with name " + magicName + " not found.");
        }
    }


    public static List<Magic> GetMagics()
    {
        return playerMagics;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;


    public static Dictionary<string, Magic> playerMagics = new Dictionary<string, Magic>();

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

    private void Start()
    {
        AddItem(new Magic("Air", "Projectile", 60, "#ADD8E6", 1));
        AddItem(new Magic("Fire", "Stab", 40, "#FF4500", 1));
        AddItem(new Magic("Earth", "AoE", 40, "#8B4513", 1));
        AddItem(new Magic("Water", "AoE", 50, "#1E90FF", 1));
        AddItem(new Magic("Grass", "Heal", 50, "#228B22", 1));

    }
    
    public void AddItem(Magic newMagic)
    {
        if (!playerMagics.ContainsKey(newMagic.magicName))
        {
            playerMagics.Add(newMagic.magicName, newMagic);
        }
        else
        {
            playerMagics[newMagic.magicName].IncreaseQuantity(1);
        }
    }

    public void RemoveMagic(Magic newMagic)
    {
        if (playerMagics.ContainsKey(newMagic.magicName))
        {
            if (playerMagics[newMagic.magicName].magicQuantity > 1)
            {
                playerMagics[newMagic.magicName].IncreaseQuantity(-1);
            }
            else if (playerMagics[newMagic.magicName].magicQuantity == 1)
            {
                playerMagics.Remove(newMagic.magicName);
            }            
        }
        else
        {
            return;
        }
    }


    public static List<Magic> GetMagics()
    {
        return new List<Magic>(playerMagics.Values);
    }
}

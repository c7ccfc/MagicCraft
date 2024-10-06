using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Dictionary<string, Magic> playerMagics = new Dictionary<string, Magic>();
    public TMP_Dropdown magicDropdown;
    public TMP_Text selectCardText;
    public TMP_Text selectCardText1;
    public int food;

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


    public List<Magic> GetMagics()
    {
        return new List<Magic>(playerMagics.Values);
    }

    public void UpdateDropdown()
    {
        magicDropdown.ClearOptions();
        List<string> magicNames = new List<string>();
        foreach (Magic magic in playerMagics.Values)
        {
            magicNames.Add(magic.magicName);
        }
        magicDropdown.AddOptions(magicNames);
    }

    public void OnSelectCardButtonClick()
    {
        string selectedOption = magicDropdown.options[magicDropdown.value].text;

        if (selectCardText.text == "Select a Card")
        {
            selectCardText.text = selectedOption;
        }
        else if (selectCardText1.text == "Select a Card")
        {
            selectCardText1.text = selectedOption;
        }
        else
        {
            // Both text boxes are full, do nothing
            return;
        }

        // Remove the selected option from the dropdown
        magicDropdown.options.RemoveAt(magicDropdown.value);
        magicDropdown.value = 0; // Reset the dropdown value
        magicDropdown.RefreshShownValue(); // Refresh the dropdown to show the updated options
    }
}


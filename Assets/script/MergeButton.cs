using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // add this line to use TextMeshProUGUI

public class MergeButton : MonoBehaviour
{
    public static InventoryManager instance;
    // Start is called before the first frame update
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI selectACard1;
    public TextMeshProUGUI selectACard2;
    private Dictionary<string, List<string>> comboDict = new Dictionary<string, List<string>>
    {
        { "AirEarth", new List<string> { "Sandstorm", "AoE", "45", "#A89A8E" } },
        { "AirFire", new List<string> { "Smoke", "Projectile", "75", "#FF6F00" } },
        { "AirGrass", new List<string> { "Pollen", "Projectile", "70", "#9BCB2E" } },
        { "AirWater", new List<string> { "Mist", "Heal", "90", "#D6F5F5" } },
        { "EarthFire", new List<string> { "Volcano", "AoE", "56", "#4B1E0E" } },  // already provided
        { "EarthGrass", new List<string> { "Field", "Stab", "85", "#3B7A24" } },
        { "EarthWater", new List<string> { "Mudslide", "AoE", "50", "#6E4D30" } },
        { "FireGrass", new List<string> { "Wildfire", "AoE", "60", "#FF4500" } },
        { "FireWater", new List<string> { "Steam", "Projectile", "63", "#E5E5E5" } },  // already provided
        { "GrassWater", new List<string> { "Flower", "Stab", "90", "#228B22" } },
        { "AirField", new List<string> { "Breeze", "AoE", "50", "#8FBF8F" } },
        { "AirFlower", new List<string> { "Spore", "Heal", "80", "#FFC0CB" } },
        { "AirMudslide", new List<string> { "Clay", "Projectile", "65", "#A9A9A9" } },
        { "AirSandstorm", new List<string> { "Cyclone", "Projectile", "70", "#F4A460" } },
        { "AirSmoke", new List<string> { "Smoke", "Projectile", "75", "#FF6F00" } },
        { "AirSteam", new List<string> { "Heat", "Projectile", "66", "#E0FFFF" } },
        { "AirVolcano", new List<string> { "Heatwave", "AoE", "60", "#FF6347" } },
        { "EarthFlower", new List<string> { "Garden", "Heal", "95", "#FFB6C1" } },
        { "EarthMist", new List<string> { "Swamp", "AoE", "52", "#708090" } },
        { "EarthPollen", new List<string> { "Allergy", "AoE", "48", "#D2B48C" } },
        { "EarthSandstorm", new List<string> { "Desert", "AoE", "55", "#C2B280" } },
        { "EarthSmoke", new List<string> { "Wasteland", "AoE", "50", "#555555" } },
        { "EarthSteam", new List<string> { "Geyser", "Projectile", "70", "#A9F5F2" } },
        { "EarthVolcano", new List<string> { "Archipelago", "AoE", "60", "#FF4500" } },
        { "FireFlower", new List<string> { "Rose", "Stab", "70", "#FF6347" } },
        { "FireMist", new List<string> { "Fog", "AoE", "55", "#F5FFFA" } },
        { "FirePollen", new List<string> { "Ash", "Projectile", "72", "#FFD700" } },
        { "FireSandstorm", new List<string> { "Gasoline", "AoE", "60", "#FF8C00" } },
        { "FireSmoke", new List<string> { "Inferno", "AoE", "59", "#B22222" } },
        { "FireSteam", new List<string> { "Heatwave", "AoE", "60", "#FF6347" } },
        { "FireVolcano", new List<string> { "Lava", "AoE", "65", "#8B0000" } },
        { "GrassMist", new List<string> { "Dew", "Heal", "85", "#98FB98" } },
        { "GrassPollen", new List<string> { "Pasture", "Heal", "90", "#FFD700" } },
        { "GrassSandstorm", new List<string> { "Oasis", "Projectile", "67", "#F4A460" } },
        { "GrassSmoke", new List<string> { "Weeds", "AoE", "58", "#556B2F" } },
        { "GrassSteam", new List<string> { "Morning", "Heal", "75", "#E0FFFF" } },
        { "GrassVolcano", new List<string> { "Fireflower", "AoE", "60", "#A52A2A" } },
        { "MistPollen", new List<string> { "Forest", "Heal", "80", "#C0C0C0" } },
        { "MistSandstorm", new List<string> { "Beach", "Projectile", "68", "#D3D3D3" } },
        { "MistSmoke", new List<string> { "Smog", "AoE", "55", "#808080" } },
        { "MistSteam", new List<string> { "Boiler", "Projectile", "70", "#E5E5E5" } },
        { "MistVolcano", new List<string> { "Hotspring", "AoE", "60", "#FF6347" } },
        { "PollenSandstorm", new List<string> { "Oasis", "Projectile", "67", "#F4A460" } },
        { "PollenSmoke", new List<string> { "Poison", "Stab", "70", "#6B8E23" } },
        { "PollenSteam", new List<string> { "Lavender", "Projectile", "72", "#FFE4B5" } },
        { "PollenVolcano", new List<string> { "Burn", "AoE", "58", "#FF6347" } },
        { "SandstormSmoke", new List<string> { "Obstruction", "AoE", "70", "#D2B48C" } },
        { "SandstormSteam", new List<string> { "Heatwave", "AoE", "60", "#FF6347" } },
        { "SandstormVolcano", new List<string> { "Obsidian", "Stab", "62", "#B22222" } },
        { "SmokeSteam", new List<string> { "Obstruction", "Projectile", "70", "#D2B48C" } },
        { "SmokeVolcano", new List<string> { "Ashfall", "AoE", "60", "#2F4F4F" } },
        { "SteamVolcano", new List<string> { "Vent", "AoE", "64", "#FF4500" } }
    };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MergeReturn(Magic magic1, Magic magic2)
    {
        string item1 = magic1.magicName;
        string item2 = magic2.magicName;
        
        List<string> currItemList = new List<string> { item1, item2 };
        currItemList.Sort();

        string dictCheck = string.Join("", currItemList);
        if (comboDict.TryGetValue(dictCheck, out List<string> value))
        {
            string magicName = value[0];
            string type = value[1];
            string damage = value[2];

            //if (InventoryManager.instance.playerMagics.ContainsKey(value[0]))
            //{
            //    InventoryManager.instance.playerMagics[value[0]].IncreaseQuantity(1);
            //}
            //else
            //{
            //    string hex = value[3];
            //    float quantity = 1;
            //    float damageFloat = float.Parse(damage);

            //    Magic newMagic = new Magic(magicName, type, damageFloat, hex, quantity);

            //    InventoryManager.instance.AddItem(newMagic);
            //}
            //InventoryManager.instance.RemoveMagic(magic1);
            //InventoryManager.instance.RemoveMagic(magic2);

            cardName.text = $"{magicName}";
            selectACard1.text = $"{type}";
            selectACard2.text = $"{damage}";
        }
        else
        {
            return;
        }
    }
}

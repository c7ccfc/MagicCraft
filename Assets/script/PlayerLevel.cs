using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevel : MonoBehaviour
{
    public static float experience;
    public static int level;
    public List<int> thresh_exp = new List<int> { 10, 20, 40, 80, 160, 320 };
    public GameObject levelUpMenu;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        experience = 0f;
        levelUpMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (experience >= thresh_exp[level]) {
            level++;
            experience = 0f;

            Time.timeScale = 0f;
            PauseMenu.isPaused = true;
            levelUpMenu.SetActive(true);
        }
    }

    public void GainExperience(int amount)
    {
        experience += amount;

    }

    public void AddBaseMagic()
    {
        //to!!!do!!!!!!!!!
        string magicName = "fire";

        Dictionary<string, float> magicStat = new Dictionary<string, float>
            {
                { "attack", 10 },
                { "cooldown", 2f }
            };

        Magic magic = new Magic(magicName, magicName, magicStat);

        InventoryManager.AddItem(magic);
    }

    public void GotoMerge()
    {
        SceneManager.LoadScene("CardMerging");
    }
}

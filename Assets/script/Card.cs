using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{
    List<Magic> equipped = new List<Magic>();
    System.Random rnd = new System.Random();
    int numMagic;

    public Button card1;
    public Button card2;
    public Button card3;
    public Button card4;

    // Start is called before the first frame update
    void Start()
    {
        List<Magic> inInventory = InventoryManager.GetMagics();
        numMagic = inInventory.Count;

        for (int i = 0; i < 4; i++)
        {
            int r = rnd.Next(inInventory.Count);
            Magic curr = inInventory[r];
            Magic copy = new Magic(curr.magicName, curr.magicDescription, curr.magicStat);
            equipped[i] = copy;
        }

        card1.onClick.AddListener(() => CastMagic(0));
        card2.onClick.AddListener(() => CastMagic(1));
        card3.onClick.AddListener(() => CastMagic(2));
        card4.onClick.AddListener(() => CastMagic(3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Equip(Magic magic, int ind)
    {
        equipped[ind] = magic;
        name = magic.magicName;
        // load image
    }


    public void CastMagic(int ind)
    {
        // cast equipped[ind]
        // delete equipped[ind]
    }
}

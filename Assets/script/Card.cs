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

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public GameObject card1;
    public GameObject card1;
    public GameObject card1; public GameObject card1;

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
            Equip(copy, i);
        }

        button1.onClick.AddListener(() => CastMagic(0));
        button2.onClick.AddListener(() => CastMagic(1));
        button3.onClick.AddListener(() => CastMagic(2));
        button4.onClick.AddListener(() => CastMagic(3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Equip(Magic magic, int ind)
    {
        equipped[ind] = magic;
        name = magic.magicName;
        GameObject drawing = LineGenerator.LoadDrawing(name);

        GameObject card;

        switch (ind)
        {
            case 0:
                card = card1;
            case 1:
                card = card2;
            case 2:
                card = card3;
            case 3:
                card = card4;
        }

        if (drawing != null)
        {
            card = GameObject.Find("card1");

            if (card != null)
            {
                drawing.transform.SetParent(card.transform, false);

                // Optionally, adjust the position and scale of the drawing
                drawing.transform.localPosition = Vector3.zero; // Center it on card1
                drawing.transform.localScale = Vector3.one; // Match the scale if needed
            }
            else
            {
                Debug.LogError("card1 GameObject not found in the scene.");
            }
        }
    }

    public void Discard(int ind)
    {
        
    }


    public void CastMagic(int ind)
    {
        // cast equipped[ind]
        // delete equipped[ind]
    }
}

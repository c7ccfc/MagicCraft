using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.VisualScripting;


public class Card : MonoBehaviour
{
    List<Magic> equipped = new List<Magic> {null,null,null,null};
    System.Random rnd = new System.Random();
    int numMagic;
    InventoryManager inventoryManager;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;

    public List<Magic> inInventory;

    // Start is called before the first frame update
    void Start()
    {
        inInventory = inventoryManager.GetMagics();
        numMagic = inInventory.Count;

        for (int i = 0; i < Mathf.Min(4, numMagic); i++)
        {
            putIn(i);
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

    public void putIn(int ind)
    {
        int r = rnd.Next(inInventory.Count);
        Magic curr = inInventory[r];
        Magic copy = new Magic(
            curr.magicName,
            curr.magicType,
            curr.magicDamage,
            curr.hexColor,
            curr.magicQuantity
        );
        Equip(copy, ind);
    }


    public void Equip(Magic magic, int ind)
    {
        equipped[ind] = magic;
        string magicName = magic.magicName;

        // Load the drawing as a new GameObject
        GameObject drawing = LineGenerator.LoadDrawing(magicName);

        GameObject card = null;

        switch (ind)
        {
            case 0:
                card = card1;
                break;
            case 1:
                card = card2;
                break;
            case 2:
                card = card3;
                break;
            case 3:
                card = card4;
                break;
        }


        if (drawing != null)
        {
            if (card != null)
            {
                drawing.transform.SetParent(card.transform, false);

                drawing.transform.localScale = new Vector3(1f / 3f, 1f / 3f, 1);
                float upwardOffset = card.GetComponent<RectTransform>().rect.height / 10;
                drawing.transform.localPosition = new Vector3(0, upwardOffset, 0);
            }
            else
            {
                Debug.LogError("card GameObject not found in the scene.");
            }
        }
    }


    public void Discard(int ind)
    {
        
    }


    public void CastMagic(int ind)
    {
        if (equipped[ind] != null) {
            TopDown.Cast(equipped[ind]);
            Discard(ind);
            Refill(ind);
        }
    }

    public void Refill(int ind)
    {
        Thread.Sleep(2000);
        putIn(ind);
    }

}

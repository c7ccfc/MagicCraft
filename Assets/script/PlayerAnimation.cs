using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerAnimation : MonoBehaviour
{
    public Image targetImage;
    public SpriteRenderer sourceSprite;

    // Start is called before the first frame update
    void Start()
    {
        targetImage = gameObject.GetComponent<Image>();
        sourceSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sourceSprite != null)
        {
            targetImage.sprite = sourceSprite.sprite;
        }
    }
}

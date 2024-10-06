using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Magic
{
    public string magicName;
    public string magicType;
    public float magicDamage;
    public string hexColor;
    public float magicQuantity;

    public Magic(string name, string type, float damage, string hex, float quantity)
    {
        magicName = name;
        magicType = type;
        magicDamage = damage;
        hexColor = hex;
        magicQuantity = quantity;
    }
    public void IncreaseQuantity(float amount)
    {
        this.magicQuantity += amount;
    }
}

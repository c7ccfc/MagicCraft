using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Magic
{
    public string magicName;
    public string magicDescription;
    public Dictionary<string, float> magicStat = new Dictionary<string, float>();

    public Magic(string name, string description, Dictionary<string, float> stat)
    {
        magicName = name;
        magicDescription = description;
        magicStat = stat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "APIKeyHolder", menuName = "ScriptableObjects/APIKeyHolder", order = 1)]
public class APIKeyHolder : ScriptableObject
{
    [SerializeField] private string apiKey;

    // Getter to access the API key
    public string GetApiKey()
    {
        return apiKey;
    }

    // Setter to update the API key (optional, for future use)
    public void SetApiKey(string newKey)
    {
        apiKey = newKey;
    }
}

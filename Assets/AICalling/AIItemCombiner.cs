using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Newtonsoft.Json;
using static UnityEditor.Progress;
using static UnityEngine.ParticleSystem;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using System.Buffers.Text;
using Unity.VisualScripting;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Drawing;

public class AIItemCombiner : MonoBehaviour
{
    public APIKeyHolder apiKeyHolder; // Reference to the ScriptableObject asset

    private string apiURL = "https://api-inference.huggingface.co/models/meta-llama/Llama-3.2-3B-Instruct";

    public void CombineItems(string item1, string item2)
    {
        StartCoroutine(SendRequest(item1, item2));
    }

    private IEnumerator SendRequest(string item1, string item2)
    {
        // Create a custom prompt
        string prompt = $"You are an AI agent that combines two items into one item. Given two items, respond with " +
            $"a nonfictional item that results from interpreting what it would mean to combine the two items. Respond with ONLY 1 WORD. " +
            $"Examples: 1. Given Knife and Cheese, respond with 'Cheese Grater' " +
            $"2. Given Earth and Water, respond with 'Ocean' " +
            $"3. Given Earth and Fire, respond with 'Volcano' " +
            $"4. Given Earth and Air, respond with 'Sky' " +
            $"5. Given Tree and Water, respond with 'Swamp' " +
            $"6. Given Car and Plane, respond with 'Airport' " +
            $"7. Given Leaf and Stone, respond with 'Pebble' " +
            $"8. Given Water and Ice, respond with 'Iceberg' " +
            $"9. Given Water and Steam, respond with 'Cloud' " +
            $"10. Given Water and Air, respond with 'Rain' " +
            $"11. Given Steam and Fire, respond with 'Smoke' " +
            $"You are given the items {item1} and {item2}. Combine these two elements and respond with 1 WORD (like the given examples). Your response can have no more than 20 characters and cannot contain these characters: \\, /, #, :, ;";

        var requestBody = new
        {
            inputs = prompt // Use the custom prompt here
        };
        string jsonBody = JsonConvert.SerializeObject(requestBody);
        Debug.Log("Request JSON: " + jsonBody); // Log the JSON payload

        using (UnityWebRequest request = new UnityWebRequest(apiURL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Add the authorization header
            string apiKey = apiKeyHolder.GetApiKey();
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check the response
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("API Response: " + responseText);

                // Define the prompt
                string currPrompt = $"You are an AI agent that combines two items into one item. Given two items, respond with " +
                    $"a nonfictional item that results from interpreting what it would mean to combine the two items. Respond with ONLY 1 WORD. " +
                    $"Examples: 1. Given Knife and Cheese, respond with 'Cheese Grater' " +
                    $"2. Given Earth and Water, respond with 'Ocean' " +
                    $"3. Given Earth and Fire, respond with 'Volcano' " +
                    $"4. Given Earth and Air, respond with 'Sky' " +
                    $"5. Given Tree and Water, respond with 'Swamp' " +
                    $"6. Given Car and Plane, respond with 'Airport' " +
                    $"7. Given Leaf and Stone, respond with 'Pebble' " +
                    $"8. Given Water and Ice, respond with 'Iceberg' " +
                    $"9. Given Water and Steam, respond with 'Cloud' " +
                    $"10. Given Water and Air, respond with 'Rain' " +
                    $"11. Given Steam and Fire, respond with 'Smoke' " +
                    $"You are given the items {item1} and {item2}. Combine these two elements and respond with 1 WORD (like the given examples). Your response can have no more than 20 characters and cannot contain these characters: \\, /, #, :, ;";

                // Remove any text preceding the prompt and the prompt itself
                int promptIndex = responseText.IndexOf(currPrompt);
                if (promptIndex != -1)
                {
                    responseText = responseText.Substring(promptIndex + currPrompt.Length).Trim();
                }

                // Split the response text into lines
                string[] lines = responseText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                // Find the first alphanumeric word in each line
                string result = null;
                foreach (var line in lines)
                {
                    string[] words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    result = words.FirstOrDefault(word => word.All(char.IsLetterOrDigit));
                    if (!string.IsNullOrEmpty(result))
                    {
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(result))
                {
                    Debug.Log($"Combined Item: {result}");
                }
                else
                {
                    Debug.LogError("Unexpected response format.");
                }
            }
            else
            {
                Debug.LogError("API call failed: " + request.error);
            }
        }
    }

}

using UnityEditor;
using UnityEngine;

public class AIItemCombinerTester
{
    [MenuItem("Tools/Test AIItemCombiner")]
    public static void TestCombineItems()
    {
        // Create a new GameObject and add the AIItemCombiner component
        GameObject testObject = new GameObject("TestAIItemCombiner");
        AIItemCombiner combiner = testObject.AddComponent<AIItemCombiner>();

        // Load the APIKeyHolder asset
        combiner.apiKeyHolder = AssetDatabase.LoadAssetAtPath<APIKeyHolder>("Assets/AICalling/ScriptableObjects/APIKey.asset");

        // Ensure the APIKeyHolder is loaded
        if (combiner.apiKeyHolder == null)
        {
            Debug.LogError("APIKeyHolder asset not found. Please ensure the path is correct.");
            return;
        }

        // Call the CombineItems method with test items
        combiner.CombineItems("Rocket", "Wood");
    }
}
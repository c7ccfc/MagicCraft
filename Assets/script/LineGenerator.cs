using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
// using Newtonsoft.Json;

public class LineGenerator : MonoBehaviour
{
    public RectTransform paperRectTransform;  // Reference to the Paper RectTransform
    public GameObject lineSegmentPrefab;      // Prefab for each line segment (a UI Image)
    public GameObject lineParent;             // Parent GameObject for grouping lines
    public Button resetButton;                // Button to reset lines
    public Button saveButton;                 // Button to save lines

    private List<GameObject> lineSegments = new List<GameObject>(); // List to keep track of line segments
    private Vector2 previousPoint;

    [System.Serializable]
    public class LineSegmentData
    {
        public Vector2 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    [System.Serializable]
    public class DrawingData
    {
        public List<LineSegmentData> lineSegments = new List<LineSegmentData>();
    }

    void Start()
    {
        // Create or assign a parent object to group all line segments
        if (lineParent == null)
        {
            CreateLineParent();
        }

        resetButton.onClick.AddListener(ResetLines);
        saveButton.onClick.AddListener(SaveDrawing);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(paperRectTransform, Input.mousePosition, null, out localPoint);

            // Check if the mouse is within the paper area before starting to draw
            if (RectTransformUtility.RectangleContainsScreenPoint(paperRectTransform, Input.mousePosition, null))
            {
                previousPoint = localPoint;
            }
        }

        if (Input.GetMouseButton(0))
        {
            // Check if the mouse is within the paper area before drawing
            if (RectTransformUtility.RectangleContainsScreenPoint(paperRectTransform, Input.mousePosition, null))
            {
                Vector2 currentPoint;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(paperRectTransform, Input.mousePosition, null, out currentPoint);

                // Only create a new segment if the distance is sufficient
                if (Vector2.Distance(previousPoint, currentPoint) > 5f)
                {
                    CreateLineSegment(previousPoint, currentPoint);
                    previousPoint = currentPoint;
                }
            }
        }
    }

    void CreateLineSegment(Vector2 startPoint, Vector2 endPoint)
    {
        // Instantiate a new line segment from the prefab and set it as a child of the lineParent
        GameObject lineSegment = Instantiate(lineSegmentPrefab, lineParent.transform);
        RectTransform rectTransform = lineSegment.GetComponent<RectTransform>();

        // Set position and size relative to the paper object
        Vector2 direction = endPoint - startPoint;
        float distance = direction.magnitude / paperRectTransform.localScale.x; // Correct the distance by dividing with the parent's scale

        rectTransform.sizeDelta = new Vector2(distance, 5f / paperRectTransform.localScale.y); // Width is the corrected distance, height is fixed but adjusted for scale
        rectTransform.pivot = new Vector2(0, 0.5f); // Set pivot to the start of the line

        // Position the line at the start point
        rectTransform.anchoredPosition = startPoint;

        // Set rotation to match the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rectTransform.rotation = Quaternion.Euler(0, 0, angle);

        // Add to the list of line segments for future reference
        lineSegments.Add(lineSegment);
    }

    void ResetLines()
    {
        // Destroy the existing lineParent object
        if (lineParent != null)
        {
            Destroy(lineParent);
        }

        // Recreate the lineParent object
        CreateLineParent();
        lineSegments.Clear();
    }

    void CreateLineParent()
    {
        lineParent = new GameObject("LineParent");
        lineParent.transform.SetParent(paperRectTransform, false);
        RectTransform lineParentRect = lineParent.AddComponent<RectTransform>();

        // Configure the parent RectTransform to match the paper size and position
        lineParentRect.anchorMin = new Vector2(0, 0);
        lineParentRect.anchorMax = new Vector2(1, 1);
        lineParentRect.sizeDelta = Vector2.zero;
        lineParentRect.anchoredPosition = Vector2.zero;
        lineParentRect.localScale = Vector3.one;  // Ensure uniform scale
    }

    void SaveDrawing()
    {
        string path = Application.dataPath + "/Drawings/drawing.json";
        DrawingData drawingData = new DrawingData();

        foreach (GameObject lineSegment in lineSegments)
        {
            RectTransform rectTransform = lineSegment.GetComponent<RectTransform>();
            LineSegmentData segmentData = new LineSegmentData
            {
                position = rectTransform.localPosition,
                rotation = rectTransform.localRotation,
                scale = rectTransform.localScale
            };
            drawingData.lineSegments.Add(segmentData);
        }

        string json = JsonUtility.ToJson(drawingData);
        File.WriteAllText(path, json);
        Debug.Log("Drawing saved to Drawings folder in Assets at " + path);
        SceneManager.LoadScene("CardMerging");
    }

    public static GameObject LoadDrawing(string magicName)
    {
        string path = Application.dataPath + "/Drawings/" + magicName + ".json";
        if (File.Exists(path))
        {
            // Create a new GameObject to act as the parent of the drawing
            GameObject lineParent = new GameObject("LineParent");
            string json = File.ReadAllText(path);
            DrawingData drawingData = JsonUtility.FromJson<DrawingData>(json);

            // Load the line segment prefab from Resources folder
            GameObject lineSegmentPrefab = Resources.Load<GameObject>("LinePrefab");
            if (lineSegmentPrefab == null)
            {
                Debug.LogError("LinePrefab not found in Resources folder.");
                return null;
            }

            // Instantiate all line segments under the new parent
            foreach (LineSegmentData segmentData in drawingData.lineSegments)
            {
                GameObject lineSegment = Instantiate(lineSegmentPrefab, lineParent.transform);
                RectTransform rectTransform = lineSegment.GetComponent<RectTransform>();
                rectTransform.localPosition = segmentData.position;
                rectTransform.localRotation = segmentData.rotation;
                rectTransform.localScale = segmentData.scale;
            }

            Debug.Log("Drawing loaded from " + path);
            return lineParent;  // Return the new GameObject representing the drawing
        }

        Debug.LogError("Drawing file not found at " + path);
        return null;
    }

}
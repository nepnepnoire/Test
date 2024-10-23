using UnityEngine;
using UnityEditor;

public class LevelEditor : EditorWindow
{
    private GameObject[] levelObjects;
    private GameObject selectedObject;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>("Level Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Level Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Cube"))
        {
            AddLevelObject("Cube");
        }
        if (GUILayout.Button("Add Sphere"))
        {
            AddLevelObject("Sphere");
        }

        selectedObject = (GameObject)EditorGUILayout.ObjectField("Selected Object", selectedObject, typeof(GameObject), true);

        if (GUILayout.Button("Delete Selected"))
        {
            DeleteSelectedObject();
        }
    }

    private void AddLevelObject(string type)
    {
        GameObject obj = GameObject.CreatePrimitive(type == "Cube" ? PrimitiveType.Cube : PrimitiveType.Sphere);
        obj.transform.position = Vector3.zero;
        obj.name = type + "_" + System.DateTime.Now.ToString("HHmmss");
        Selection.activeGameObject = obj; // Select newly created object
    }

    private void DeleteSelectedObject()
    {
        if (selectedObject != null)
        {
            DestroyImmediate(selectedObject);
            selectedObject = null;
        }
    }
}
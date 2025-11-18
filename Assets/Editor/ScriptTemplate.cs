using UnityEditor;
using UnityEngine;

public class ScriptTemplate : EditorWindow
{

    private string scriptName = "NewScript";
    private bool includeStart = true;
    private bool includeUpdate = false;
    private bool includeAwake = false;
    private bool includeReset = false;
    // Add other options as needed

    [MenuItem("Window/Script Generator")]
    public static void ShowWindow()
    {
        GetWindow<ScriptTemplate>("Script Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Script Generation Settings", EditorStyles.boldLabel);

        scriptName = EditorGUILayout.TextField("Script Name", scriptName);
        includeAwake = EditorGUILayout.Toggle("Include Awake()", includeAwake);
        includeStart = EditorGUILayout.Toggle("Include Start()", includeStart);
        includeUpdate = EditorGUILayout.Toggle("Include Update()", includeUpdate);
        includeReset = EditorGUILayout.Toggle("Include Reset()", includeReset);
        // Add more UI elements for other method options

        if (GUILayout.Button("Generate Script"))
        {
            GenerateScript();
        }

        // Implement preview window logic here
        GUILayout.Label("Script Preview:", EditorStyles.boldLabel);
        EditorGUILayout.TextArea(GenerateScriptPreview(), GUILayout.ExpandHeight(true));
    }

    private void GenerateScript()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Script", scriptName, "cs", "Please enter a file name for the new script.");
        if (!string.IsNullOrEmpty(path))
        {
            string scriptContent = GenerateScriptContent();
            System.IO.File.WriteAllText(path, scriptContent);
            AssetDatabase.Refresh();
        }
    }

    private string GenerateScriptContent()
    {
        string content = $"using UnityEngine;\n\npublic class {scriptName} : MonoBehaviour\n{{\n";

        if (includeAwake)
        {
            content += "    void Awake()\n    {\n        // Game logic here\n    }\n\n";

        }
        if (includeStart)
        {
            content += "    void Start()\n    {\n        // Initialization logic here\n    }\n\n";
        }
        if (includeUpdate)
        {
            content += "    void Update()\n    {\n        // Game logic here\n    }\n\n";
        }

        if (includeReset)
        {
            content += "    void Reset()\n    {\n        // Game logic here\n    }\n\n";

        }
        // Add other method generations based on options

        content += "}\n";
        return content;
    }

    private string GenerateScriptPreview()
    {
        // This is essentially the same as GenerateScriptContent but without saving
        return GenerateScriptContent();
    }

}

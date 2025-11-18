using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ScriptGenerator : MonoBehaviour
{
    public static string GetScriptTemplate(string scriptName, bool includeAwake, bool includeStart, bool includeUpdate, bool includeReset)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();
        sb.AppendLine($"public class {scriptName} : MonoBehaviour");
        sb.AppendLine("{");

        if (includeAwake)
        {
            sb.AppendLine("    // Awake is called before Start");
            sb.AppendLine("    void Awake()");
            sb.AppendLine("    {");
            sb.AppendLine("        ");
            sb.AppendLine("    }");
        }

        if (includeStart)
        {
            sb.AppendLine("    // Start is called before the first frame update");
            sb.AppendLine("    void Start()");
            sb.AppendLine("    {");
            sb.AppendLine("        ");
            sb.AppendLine("    }");
        }

        if (includeUpdate)
        {
            if (includeStart) sb.AppendLine();
            sb.AppendLine("    // Update is called once per frame");
            sb.AppendLine("    void Update()");
            sb.AppendLine("    {");
            sb.AppendLine("        ");
            sb.AppendLine("    }");
        }



        if (includeReset)
        {
            sb.AppendLine("    // Reset is called only for reseting inspector values");
            sb.AppendLine("    void Reset()");
            sb.AppendLine("    {");
            sb.AppendLine("        ");
            sb.AppendLine("    }");
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    public static string CreateScriptFile(string scriptName, string folderPath, string template)
    {
        // Ensure the directory exists. Create it if it doesn't.
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            AssetDatabase.Refresh(); // Refresh to register the new folder
        }

        // ... (rest of the method logic, which uses folderPath)
        string filePath = Path.Combine(folderPath, scriptName + ".cs");

        if (File.Exists(filePath))
        {
            Debug.LogWarning($"A script with the name '{scriptName}' already exists at '{filePath}'. Skipping file creation.");
            return null;
        }

        File.WriteAllText(filePath, template);
        AssetDatabase.Refresh();

        return filePath;
        //if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(scriptName))
        //{
        //    Debug.LogError("Invalid folder path or script name.");
        //    return null;
        //}

        //string filePath = Path.Combine(folderPath, scriptName + ".cs");

        //if (File.Exists(filePath))
        //{
        //    Debug.LogWarning($"A script with the name '{scriptName}' already exists at '{filePath}'. Skipping file creation.");
        //    return null;
        //}

        //File.WriteAllText(filePath, template);
        //AssetDatabase.Refresh();

        //return filePath;
    }


}

using UnityEditor;
using UnityEngine;

public class ObjectPreview : EditorWindow
{
    private Sprite previewSprite;
    private bool previewInitialized = false;

    public void Initialize(GameObject obj, Sprite sprite)
    {
        previewSprite = sprite;
        previewInitialized = true;
        Repaint();

        if (obj)
        {
            DestroyImmediate(obj);
        }
    }

    private void OnGUI()
    {
        if (!previewInitialized)
        {
            EditorGUILayout.LabelField("No object data to preview.", EditorStyles.wordWrappedLabel);
            return;
        }

        GUILayout.BeginVertical();
        GUILayout.Label("GameObject Preview", EditorStyles.boldLabel);

        Rect previewRect = GUILayoutUtility.GetAspectRect(1);
        DrawSpritePreview(previewRect);

        GUILayout.EndVertical();
    }

    private void DrawSpritePreview(Rect rect)
    {
        if (previewSprite != null)
        {
            GUI.DrawTextureWithTexCoords(rect, previewSprite.texture, GetUVs(previewSprite));
        }
        else
        {
            GUI.Box(rect, "No Sprite Available", EditorStyles.helpBox);
        }
    }

    private Rect GetUVs(Sprite sprite)
    {
        var textureRect = sprite.rect;
        return new Rect(
            textureRect.x / sprite.texture.width,
            textureRect.y / sprite.texture.height,
            textureRect.width / sprite.texture.width,
            textureRect.height / sprite.texture.height
        );
    }

}

using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;


public class ObjectCreatorMenu : EditorWindow
{

    private Settings config;
    private bool showAdvancedOptions = false;
    private bool addId = false;
    [Range(1, 9)]
    private int createId = 1;
    private Vector2 scrollPosition;
    private bool isBattleOption = false;
    /// <summary>
    /// will add the ability to attach a created script to a new object later.
    /// </summary>
    [MenuItem("Tools/Object Creator")]
    public static void ShowWindow()
    {
        GetWindow<ObjectCreatorMenu>("Object Creator");
    }

    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUI.BeginChangeCheck();
        showAdvancedOptions = EditorGUILayout.Toggle("Show Manager", showAdvancedOptions);
        isBattleOption = EditorGUILayout.Toggle("Battle Character", isBattleOption);
        config = (Settings)EditorGUILayout.ObjectField(
            "Object Type Config",
            config,
            typeof(Settings),
            false
        );

        if (showAdvancedOptions == false)
        {
            GUILayout.Label("Create Custom Object", EditorStyles.boldLabel);

            if (config != null)
            {
                if (GUILayout.Button($"Create {config.objectType}"))
                {
                    CreateObject(config);
                }

                if (GUILayout.Button($"Preview {config.objectType}"))
                {
                    ShowPreview(config);
                }
                if(GUILayout.Button("Reset All"))
                {
                    config = null;
                  
                }
            
            }
        }
        else // Manager Creation UI
        {
            if (config != null)
            {
                addId = EditorGUILayout.ToggleLeft("Include Identification Number", addId, EditorStyles.boldLabel);
                if (addId)
                {
                    EditorGUILayout.HelpBox("Optionally add an ID to the object.", MessageType.Info);
                    createId = EditorGUILayout.IntField("Object ID", createId);
                }
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("These are advanced options for the Manager Object creation.", MessageType.Info);
                GUILayout.Label("Create Manager Object", EditorStyles.boldLabel);
                Settings.ManagerType managerType = (Settings.ManagerType)EditorGUILayout.EnumPopup("Manager Type", config.manager);
                config.manager = managerType;
                string managerNameField = "";
                switch (createId)
                {
                    case 1: managerNameField = config.game; break;
                    case 2: managerNameField = config.audio; break;
                    case 3: managerNameField = config.level; break;
                    case 4: managerNameField = config.userInterface; break;
                    case 5: managerNameField = config.other; break;
                    case 6: managerNameField = config.dialogue; break;
                }
                managerNameField = EditorGUILayout.TextField("Manager Name", managerNameField);
                switch (createId)
                {
                    case 1: config.game = managerNameField; break;
                    case 2: config.audio = managerNameField; break;
                    case 3: config.level = managerNameField; break;
                    case 4: config.userInterface = managerNameField; break;
                    case 5: config.other = managerNameField; break;
                    case 6: config.dialogue = managerNameField; break;
                }

                if (GUILayout.Button($"Create {config.manager}"))
                {
                    CreateManager(config);
                }
            }
            EditorGUILayout.Space();
        } // battle creation UI
        

            EditorGUILayout.Separator();
        EditorGUILayout.EndScrollView();
    }

    private void CreateObject(Settings config)
    {
        showAdvancedOptions = false;
        GameObject newObject;
        if (config.prefab != null)
        {
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(config.prefab);
        }
        else
        {
            newObject = new GameObject(config.objectType.ToString());
        }
        newObject.name = config.objectName.ToString();

        ApplySettings(newObject, config);

        switch (config.objectType)
        {
            case Settings.ObjectType.Player:
                if (newObject.GetComponent<PlayerController>() == null)
                    newObject.AddComponent<PlayerController>();
                newObject.tag = "Player";
                if (newObject.GetComponent<SpriteRenderer>() != null)
                {
                    newObject.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
                    newObject.GetComponent<SpriteRenderer>().sortingOrder = 45;
                }
                //if (newObject.GetComponent<PolygonCollider2D>() != null)
                //    newObject.GetComponent<PolygonCollider2D>().isTrigger = false;
                if (newObject.GetComponent<CapsuleCollider2D>() != null)
                    newObject.GetComponent<CapsuleCollider2D>().isTrigger = false;

                if (config.shootArea != null)
                {
                    GameObject newChild = Instantiate(config.shootArea, newObject.transform);
                    newChild.transform.localPosition = new Vector3(0, -0.5f, 0f);
                    newChild.transform.localRotation = Quaternion.identity;
                }
                if (newObject.GetComponent<Animator>() == null)
                    newObject.AddComponent<Animator>();
                if (config.hitArea != null)
                {
                    GameObject hitInstance = Instantiate(config.hitArea, newObject.transform);
                   
                    if (hitInstance!= null)
                    {
                        hitInstance.transform.localPosition = new Vector3(0, -0.5f, 0f);
                    hitInstance.transform.localRotation = Quaternion.identity;
                        if (hitInstance.GetComponent<BoxCollider2D>() == null)
                            hitInstance.AddComponent<BoxCollider2D>();
                        hitInstance.GetComponent<BoxCollider2D>().isTrigger = config.isTrigger;
                    }
                }
                break;
            case Settings.ObjectType.Enemy:
                //if (newObject.GetComponent<EnemyAI>() == null)
                //    newObject.AddComponent<EnemyAI>();
                newObject.tag = "Enemy";
                if (newObject.GetComponent<SpriteRenderer>() != null)
                {
                    newObject.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
                    newObject.GetComponent<SpriteRenderer>().sortingOrder = 35;
                }
                //if (newObject.GetComponent<PolygonCollider2D>() != null)
                //    newObject.GetComponent<PolygonCollider2D>().isTrigger = false;

                if (newObject.GetComponent<CapsuleCollider2D>() != null)
                    newObject.GetComponent<CapsuleCollider2D>().isTrigger = false;

                if (newObject.GetComponent<Animator>() == null)
                    newObject.AddComponent<Animator>();

                if (config.shootArea != null)
                {
                    GameObject newChild2 = Instantiate(config.shootArea, newObject.transform);
                    newChild2.transform.localPosition = new Vector3(0, -0.5f, 0f);
                    newChild2.transform.localRotation = Quaternion.identity;
                }
                if (config.hitArea != null)
                {
                    GameObject hitInstance2 = Instantiate(config.hitArea, newObject.transform);
                   if(hitInstance2 != null)
                   {
                       hitInstance2.transform.localPosition = new Vector3(0, -0.5f, 0f);
                       hitInstance2.transform.localRotation = Quaternion.identity;
                       if (hitInstance2.GetComponent<BoxCollider2D>() == null)
                           hitInstance2.AddComponent<BoxCollider2D>();
                       hitInstance2.GetComponent<BoxCollider2D>().isTrigger = config.isTrigger;
                   }
                }
                break;
            case Settings.ObjectType.Trigger:
                newObject.tag = "TriggerObject";
                newObject.layer = 5;

                if (newObject.GetComponent<SpriteRenderer>() != null)
                    newObject.GetComponent<SpriteRenderer>().sortingLayerName = "Triggers";
                if (newObject.GetComponent<BoxCollider2D>() != null)
                    newObject.GetComponent<BoxCollider2D>().isTrigger = config.isTrigger;
                break;

            case Settings.ObjectType.NPC:
                newObject.tag = "NPC";

                if (newObject.GetComponent<SpriteRenderer>() != null)
                {
                    newObject.GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
                    newObject.GetComponent<SpriteRenderer>().sortingOrder = 25;
                }
                if (newObject.GetComponent<CapsuleCollider2D>() != null)
                    newObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                if (newObject.GetComponent<Animator>() == null)
                    newObject.AddComponent<Animator>();
                newObject.AddComponent<NPC_Controller>();

                if (config.collisionArea != null)
                {
                    GameObject collisionInstance = Instantiate(config.collisionArea, newObject.transform);
                    if (collisionInstance != null)
                    {
                        collisionInstance.transform.localPosition = Vector3.zero;
                        collisionInstance.transform.localRotation = Quaternion.identity;
                        if (collisionInstance.GetComponent<BoxCollider2D>() == null)
                            collisionInstance.AddComponent<BoxCollider2D>();
                        collisionInstance.GetComponent<BoxCollider2D>().isTrigger = config.isTrigger;
                        collisionInstance.GetComponent<BoxCollider2D>().size = new Vector2(2.0f, 2.0f);
                    }
                }
                break;
          
        }

        Selection.activeGameObject = newObject;
    }

    private void CreateManager(Settings config)
    {
        showAdvancedOptions = true;
        GameObject newObject;

        if (config.prefab2 != null)
        {
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(config.prefab2);
        }

        else
        {
            newObject = new GameObject(config.manager.ToString());
        }


        newObject.name = config.managerName.ToString();

        switch (config.manager)
        {
            case Settings.ManagerType.Game:
                //Debug.Log("Game");
                createId = 1;
                addId = true;
                newObject.AddComponent<GameManager>();

                config.managerName = config.game;

                break;
            case Settings.ManagerType.Audio:

                addId = true;

                createId = 2;
                 newObject.AddComponent<AudioManager>();
                config.managerName = config.audio;

                break;
            case Settings.ManagerType.Level:

                createId = 3;
                newObject.AddComponent<LevelManager>();
                addId = true;

                config.managerName = config.level;
                break;
            case Settings.ManagerType.UI:
                createId = 4;
                if (GameObject.FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>() == null)
                {
                    var eventSystemObject = new GameObject("EventSystem");
                    eventSystemObject.AddComponent<EventSystem>();
                    eventSystemObject.AddComponent<InputSystemUIInputModule>();
                }
                //  newObject.AddComponent<UIManager>();

                Canvas canvas = newObject.AddComponent<Canvas>();

                canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Set desired render mode
                canvas.vertexColorAlwaysGammaSpace = true;
                canvas.pixelPerfect = true;
                // Add CanvasScaler for scaling UI elements
                CanvasScaler newScaler = newObject.AddComponent<CanvasScaler>();
                Vector2 gameViewResolution = new(1920, 1080);
                newScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                newScaler.referenceResolution = gameViewResolution;
                newScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                newScaler.matchWidthOrHeight = 0.5f;
                // Add GraphicRaycaster for UI interaction
                newObject.AddComponent<GraphicRaycaster>();

                addId = true;

                config.managerName = config.userInterface;

                break;
            case Settings.ManagerType.Other:
                createId = 5;
                addId = true;
                /// add custom script here
                config.managerName = config.other;
                break;
            case Settings.ManagerType.Battle: 
                createId = 6;
                newObject.AddComponent<BattleManager>();
                addId = true;
                config.managerName = config.battle;
                break;
            case Settings.ManagerType.Dialogue: 
                createId = 7;
                newObject.AddComponent<DialogueManager>();
                addId = true;
                config.managerName = config.dialogue;
                break;

        }
        Selection.activeGameObject = newObject;

    }



    private void ShowPreview(Settings config)
    {
        var previewWindow = GetWindow<ObjectPreview>("Object Preview");

        GameObject dummyObject = CreateDummyObject(config);
        Sprite previewSprite = null;
        if (config.hasSpriteRenderer && !string.IsNullOrEmpty(config.spritePath))
        {
            previewSprite = AssetDatabase.LoadAssetAtPath<Sprite>(config.spritePath);
        }

        previewWindow.Initialize(dummyObject, previewSprite);
        DestroyImmediate(dummyObject);
    }

    private GameObject CreateDummyObject(Settings config)
    {
        GameObject dummyObject = new GameObject(config.objectName);
        dummyObject.hideFlags = HideFlags.HideAndDontSave;

        if (config.hasSpriteRenderer)
        {
            SpriteRenderer sr = dummyObject.AddComponent<SpriteRenderer>();
            if (!string.IsNullOrEmpty(config.spritePath))
            {
                Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(config.spritePath);
                if (loadedSprite != null)
                {
                    sr.sprite = loadedSprite;
                }
            }
        }
        return dummyObject;
    }

    private void ApplySettings(GameObject newObject, Settings config)
    {
        if (config.hasRigidbody && newObject.GetComponent<Rigidbody2D>() == null)
        {
            newObject.AddComponent<Rigidbody2D>();
        }
        if (config.hasBoxCollider && newObject.GetComponent<BoxCollider2D>() == null)
        {
            BoxCollider2D collider = newObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = config.isTrigger;
        }
        if(config.hasCapsuleCollider && newObject.GetComponent<CapsuleCollider2D>() == null)
        {
            CapsuleCollider2D capsule = newObject.AddComponent<CapsuleCollider2D>();
            capsule.isTrigger = config.isTrigger;
        }
        if(config.hasCircleCollider && newObject.GetComponent<CircleCollider2D>() == null)
        {
            CircleCollider2D circle = newObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = config.isTrigger;
        }
        if(config.hasAnimator && newObject.GetComponent<Animator>() == null)
        {
            newObject.AddComponent<Animator>();
        }
        if (config.hasSpriteRenderer)
        {
            SpriteRenderer spriteRenderer = newObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                spriteRenderer = newObject.AddComponent<SpriteRenderer>();
            }

            if (!string.IsNullOrEmpty(config.spritePath))
            {
                Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(config.spritePath);
                if (loadedSprite != null)
                {
                    spriteRenderer.sprite = loadedSprite;
                }
                else
                {
                    Debug.LogWarning($"Could not load sprite from path: {config.spritePath}");
                }
            }
        }
        if (config.hasPolygonCollider && newObject.GetComponent<PolygonCollider2D>() == null)
        {
            PolygonCollider2D poly = newObject.AddComponent<PolygonCollider2D>();
            poly.isTrigger = config.isTrigger;
        }
    }


}

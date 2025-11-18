using UnityEngine;

[CreateAssetMenu(fileName = "New Object Configuration")]
public class Settings : ScriptableObject
{
    public enum ObjectType
    {
        Player,
        Enemy,
        Trigger,
        Manager,
        NPC,
        Other


    }
    public enum ManagerType
    {
        Game,
        Audio,
        Level,
        UI,
        Battle,
        Dialogue,
        Other,


    }

    [Header("Object Settings")]
    public ObjectType objectType; // Use the enum here
    public string objectName;
    public GameObject prefab; // to hold player


    [Header("Components")]
    public bool hasRigidbody;
    public bool hasBoxCollider;
    public bool hasSpriteRenderer;
    public bool hasPolygonCollider;
    public bool hasCircleCollider;
    public bool hasAnimator;
    public bool hasCapsuleCollider;

    [Header("Sprite")]
    public string spritePath;

    [Header("Collider Properties")]
    public bool isTrigger;

    [Header("Player/Enemy Specific")]
    public GameObject shootArea;
    public GameObject hitArea;

    [Header("NPC Specific")]
    public GameObject collisionArea;
    public string dialogueFilePath;
    public float interactionRadius;
    public bool isQuestGiver;
    public bool isShopkeeper;
    public bool isStoryCharacter; // if the character is important to the story
    public bool isAlly; // if the character is an ally


    [Header("Trigger Specific")]
    public GameObject triggerArea;
    public LayerMask triggerLayer;
    public float triggerRadius;

    [Header("Manager Settings")]
    public ManagerType manager;
    public string managerName;
    public bool isManager = false;
    public GameObject prefab2;

    [Header("Manager Names")]
    public string game;
    public string audio;
    public string level;
    public string userInterface;
    public string other;
    public string battle;
    public string dialogue;

}

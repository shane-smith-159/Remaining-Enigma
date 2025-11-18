using UnityEngine;

[CreateAssetMenu(fileName = "BattleSettings", menuName = "ScriptableObjects/BattleSettings", order = 1)]
public class BattleSettings : ScriptableObject
{
    public int maxHealth;
    public int health;

    public int stamina;
    public int maxStamina;

    public int staminaRegenRate;
    public int healthRegenRate;

    public int staminaCost;

    public int attackPower;
    public int defense;
    public int resistance;
    public int intellectPower;
    public int dexterity;

    public float moveSpeed;
  public int criticalChance;
  public int evasionChance;
    [HideInInspector]
    [Range(0f, 1f), Tooltip("Resistance to poison effects")]
    public float poisonResistance;
    [Range(0f, 1f), Tooltip("Resistance to burn effects")]
    public float burnResistance;
    [Range(0f, 1f), Tooltip("Resistance to freeze effects")]
    public float freezeResistance;



}   



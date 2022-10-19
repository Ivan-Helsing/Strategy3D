using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Basics Humanoid Property", fileName = "New Humanoids")]
public class HumanoidProperty : ScriptableObject
{

    [Space]
    [Header("Property")]
    [Tooltip("Humanoid Strength for calculation Damage dealing")]
    [SerializeField] int strength;
    [Tooltip("Humanoid Agility for calculation Attack Speed")]
    [SerializeField] int agility;
    [Tooltip("Humanoid Stamina for calculation Maximum Health & Regeneration")]
    [SerializeField] int stamina;
    [Tooltip("Humanoid Intellence for calculation of Mana and Magic Damage Dealing")]
    [SerializeField] int intellence;
    [Tooltip("Humanoid Aura for calculation Magic Resistance")]
    [SerializeField] int aura;
    [Tooltip("Humanoid Movement Speed for moving")]
    [SerializeField] int movementSpeed;

    [Space]
    [Header("Property Multipliers")]
    [Tooltip("Humanoid Basic Attack Speed")]
    [SerializeField] float basicAttackSpeed;
    [Tooltip("Humanoid Basic Attack Range")]
    [SerializeField] float basicAttackRange;
    [Tooltip("Humanoid Basic Damage Multiplier")]
    [SerializeField] float basicDamageMultiplier;
    [Tooltip("Humanoid Basic Deffence Multiplier")]
    [SerializeField] float deffenceMultiplier;


    public int Strength { get => strength; }
    public int Agility { get => agility; }
    public int Stamina { get => stamina; }
    public int Intellect { get => intellence; }
    public int Aura { get => aura; }
    public float MovementSpeed { get => movementSpeed; }

    public float BasicAttackSpeed { get => basicAttackSpeed; }
    public float BasicAttackRange { get => basicAttackRange; }
    public float BasicDamageMultiplier { get => basicDamageMultiplier; }
    public float DeffenceMultiplier { get => deffenceMultiplier; }

}

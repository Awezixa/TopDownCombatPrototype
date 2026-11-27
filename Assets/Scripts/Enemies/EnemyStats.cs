using UnityEngine;


public enum StatusEffects { None, Burn, Blind}

[CreateAssetMenu(menuName = "Combat/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    //general info
    public float maxHP = 20f;
    public float moveSpeed = 2f;

    //attack stats
    public float attackDmg = 5f;
    public float attackRange;
    public float telegraphTime;
    public float attackCD = 3f;

    //for status eefects from player are applied
    public StatusEffects statusOnHit = StatusEffects.None;
    public float statusDuration = 3f;
    public float statusStrength = 1f;



}

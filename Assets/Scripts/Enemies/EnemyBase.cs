using UnityEngine;

// Base class for all enemies: loads stats from EnemyStats, sets up health and status effects.
[RequireComponent(typeof(Health), typeof(StatusEffectHandler))]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyStats data;

    protected Health health;
    protected StatusEffectHandler status;
    protected Transform player;

    protected virtual void Awake()
    {
        //set up and establish individual enemies health, effects and whether are dead or not
        //add weaknessess and types here as well
        health = GetComponent<Health>();
        status = GetComponent<StatusEffectHandler>();
        health.Init(data.maxHP);
        health.Died += OnDied;
    }

    protected virtual void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    protected void HitTarget(GameObject target)
    {
        target.GetComponent<IDamageable>()?.TakeDamage(data.attackDmg);
        target.GetComponent<StatusEffectHandler>()?.Apply(data.statusOnHit, data.statusDuration, data.statusStrength);
    }

    protected virtual void OnDied() => Destroy(gameObject);
}
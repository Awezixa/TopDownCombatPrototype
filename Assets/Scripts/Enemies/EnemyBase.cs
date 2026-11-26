using UnityEngine;

/// Base class for all enemies: loads stats from EnemyData and provides a shared way to hit the player.

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyStats data;

    protected Health health;
    //protected StatusEffectHandler status;
    protected Transform player;

    protected virtual void Awake()
    {
        health = GetComponent<Health>();
        //status = GetComponent<StatusEffectHandler>();
        health.Init(data.maxHP);
        health.Died += OnDied;
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    protected void HitTarget(GameObject target)
    {
        target.GetComponent<IDamageable>()?.TakeDamage(data.attackDmg);
        //target.GetComponent<StatusEffectHandler>()?.Apply(data.statusOnHit, data.statusDuration, data.statusStrength);
    }

    protected virtual void OnDied() => Destroy(gameObject);
}
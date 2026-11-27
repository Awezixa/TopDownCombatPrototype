using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projSpeed = 8f;
    [SerializeField] private float Damage = 10f;
    [SerializeField] private float lifetime = 3f;

    private StatusEffects statusType = StatusEffects.None;//for if the projectile being fired applied a status effect
    private float statusDuration, statusStrength; // info for the status effect
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //helper function to allow different entities to change damage and whether or not status applied
    public void Configure(float dmg, StatusEffects type, float duration, float strength)
    {
        Damage = dmg;
        statusType = type;
        statusDuration = duration;
        statusStrength = strength;
    }



    public void fireProj(Vector2 Dir)
    {
        Dir.Normalize();
        transform.right = Dir; //make projectile face where is being shot
        rb.linearVelocity = Dir * projSpeed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add damage stuff here and destroy the object when makes collision

        collision.GetComponentInParent<IDamageable>()?.TakeDamage(Damage);
        if (statusType != StatusEffects.None)
            collision.GetComponentInParent<StatusEffectHandler>()?.Apply(statusType, statusDuration, statusStrength);

        Destroy(gameObject);

    }








}

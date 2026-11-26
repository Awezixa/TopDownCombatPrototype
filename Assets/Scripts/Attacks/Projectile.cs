using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projSpeed = 8f;
    [SerializeField] private float Damage = 10f;
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }








}

using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, ICooldown
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float cooldown = 1f;


    private float nextFireTime;
    public bool IsReady => Time.time >= nextFireTime;
    public float CooldownNormalized => Mathf.Clamp01((nextFireTime - Time.time) / cooldown);
    public float RemainingTime => Mathf.Max(0f, nextFireTime - Time.time);



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsReady)
        {
            shoot();
        }
    }

    private void shoot()
    {
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 origin = firePoint.position;
        Vector2 direction = mouseWorld - origin;

        Projectile p = Instantiate(projectilePrefab, origin, Quaternion.identity);
        p.fireProj(direction);

        nextFireTime = Time.time + cooldown;
    }





}

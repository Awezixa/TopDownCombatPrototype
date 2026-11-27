using System.Collections.Generic;
using UnityEngine;

public class UltimateAttack : MonoBehaviour, ICooldown
{
    [SerializeField] private UltimateBomb bombPrefab;
    [SerializeField] private Transform throwPoint;
    //[SerializeField] private KeyCode key = KeyCode.Q;
    [SerializeField] private float cooldown = 20f;
    [SerializeField] private float maxThrowRange = 8f;
    //[SerializeField] private GameObject explosionVfx;

    [SerializeField] private float radius = 2.5f;//explosion radius
    [SerializeField] private float damage = 15f;
    [SerializeField] private float burnDps = 4f;
    [SerializeField] private float burnDuration = 3f;
    [SerializeField] private float blindDuration = 2.5f;
    [Range(0f, 1f)]
    [SerializeField] private float executeThreshold = 0.25f;   // 0 = no execute
    [SerializeField] private LayerMask enemyLayers;

    private float nextUseTime;

    public bool IsReady => Time.time >= nextUseTime;
    public float CooldownNormalized => Mathf.Clamp01((nextUseTime - Time.time) / cooldown);
    public float RemainingTime => Mathf.Max(0f, nextUseTime - Time.time);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsReady) Throw();
    }

    private void Throw()
    {
        //throw bomb to player mouse position within throwable radius
        Vector2 origin = throwPoint != null ? throwPoint.position : transform.position;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = origin + Vector2.ClampMagnitude(mouse - origin, maxThrowRange);

        UltimateBomb bomb = Instantiate(bombPrefab, origin, Quaternion.identity);
        bomb.Init(target, Explode);

        nextUseTime = Time.time + cooldown;//reset bomb cooldown
    }

    private void Explode(Vector2 point)
    {
        HashSet<Health> alreadyHit = new HashSet<Health>();   // an enemy with 2 colliders is hit once

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(point, radius, enemyLayers))
        {
            Health h = hit.GetComponentInParent<Health>();
            if (h == null || !alreadyHit.Add(h)) continue;

            h.TakeDamage(damage);
            if (h.Current <= 0f) continue;

            // execute
            if (h.execute <= executeThreshold)
            {
                h.TakeDamage(h.Current);
                continue;
            }

            StatusEffectHandler s = h.GetComponent<StatusEffectHandler>();
            if (s != null)
            {
                s.Apply(StatusEffects.Burn, burnDuration, burnDps);
                s.Apply(StatusEffects.Blind, blindDuration, 1f);
            }
        }

        //if (explosionVfx != null)
        //{
        //    GameObject fx = Instantiate(explosionVfx, point, Quaternion.identity);
        //    fx.transform.localScale = Vector3.one * radius * 2f;
        //    Destroy(fx, 1f);
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxThrowRange);
    }
}
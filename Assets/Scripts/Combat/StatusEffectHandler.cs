using UnityEngine;

// Applies timed status effects (burn, blind) to whatever it is attached to and tints the sprite.
public class StatusEffectHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;//sprite attached to that will cause effect

    private const float TickInterval = 0.5f;   // burn damages ticks
    private Health health;
    private float burnTimer, burnDps, burnTick;
    private float blindTimer;

    public bool IsBlinded => blindTimer > 0f;
    public bool IsBurning => burnTimer > 0f;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Apply(StatusEffects type, float duration, float strength)
    {
        // if add more effects can add and add what does and for how long
        switch (type)
        {
            case StatusEffects.Burn:
                if (burnTimer <= 0f) burnTick = TickInterval;
                burnTimer = duration;
                burnDps = strength;
                break;
            case StatusEffects.Blind:
                blindTimer = duration;
                break;
        }
    }

    private void Update()
    {
        if (burnTimer > 0f)
        {
            burnTimer -= Time.deltaTime;
            burnTick -= Time.deltaTime;
            if (burnTick <= 0f)
            {
                burnTick += TickInterval;
                health.TakeDamage(burnDps * TickInterval);
            }
        }
        if (blindTimer > 0f) blindTimer -= Time.deltaTime;

        //changing of sprite colour to add/ look like effect taking place
        if (sprite != null)
        {
            sprite.color = IsBurning ? new Color(1f, 0.5f, 0.2f) : IsBlinded ? new Color(0.4f, 0.4f, 0.4f) : Color.white;
        }
    }
}

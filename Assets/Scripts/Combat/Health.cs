using System;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
}

// Shared health system for player and enemies
public class Health : MonoBehaviour, IDamageable
{
    public float Max { get; private set; }
    public float Current { get; private set; }

    public event Action<float> Damaged;
    public event Action Died;

    public void Init(float max)
    {
        Max = max;
        Current = max;
    }

    public void TakeDamage(float amount)
    {
        if (Current <= 0f) return;
        Current = Mathf.Max(0f, Current - amount);
        Damaged?.Invoke(amount);
        if (Current <= 0f) Died?.Invoke();
    }
}
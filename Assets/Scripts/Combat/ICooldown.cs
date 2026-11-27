using UnityEngine;


public interface ICooldown
{
    bool IsReady { get; }
    float CooldownNormalized { get; }
    float RemainingTime { get; }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolDowns : MonoBehaviour
{
    [SerializeField] private MonoBehaviour source;   // any script that implements ICooldown
    [SerializeField] private Image cooldownOverlay;  // the Filled image
    [SerializeField] private TMP_Text cooldownText;  // optional

    private ICooldown cooldown;

    private void Awake()
    {
        cooldown = source as ICooldown;
        if (cooldown == null)
            Debug.LogError($"{name}: Source must be a script that implements ICooldown", this);
    }

    private void Update()
    {
        if (cooldown == null) return;

        cooldownOverlay.fillAmount = cooldown.CooldownNormalized;

        if (cooldownText != null)
        {
            bool onCooldown = !cooldown.IsReady;
            cooldownText.enabled = onCooldown;
            if (onCooldown)
                cooldownText.text = Mathf.CeilToInt(cooldown.RemainingTime).ToString();
        }
    }



}
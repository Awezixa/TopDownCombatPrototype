using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolDowns : MonoBehaviour
{
    [SerializeField] private PlayerMechanic mechanic;
    [SerializeField] private Image cooldownOverlay;   // the Filled image
    [SerializeField] private TMP_Text cooldownText;   // optional


    // Update is called once per frame
    void Update()
    {
        cooldownOverlay.fillAmount = mechanic.CooldownNormalized;

        if (cooldownText != null)
        {
            bool onCooldown = !mechanic.IsReady;
            cooldownText.enabled = onCooldown;
            if (onCooldown)
                cooldownText.text = Mathf.CeilToInt(mechanic.RemainingTime).ToString();
        }
    }
}

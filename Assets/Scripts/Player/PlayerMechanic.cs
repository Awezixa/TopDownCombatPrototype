using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMechanic : MonoBehaviour
{
    [SerializeField] private float teleportRadius = 5.0f;
    [SerializeField] private float cooldown = 8f;
    private float nextTpTime;

    // For the UI cooldown bar (0 = ready, 1 = just used)
    public float CooldownNormalized => Mathf.Clamp01((nextTpTime - Time.time) / cooldown);
    //bool to check if tp is available
    public bool IsReady => Time.time >= nextTpTime;
    public float RemainingTime => Mathf.Max(0f, nextTpTime - Time.time);
    public float CooldownDuration => cooldown;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        if (!IsReady) return;
        //mouse poition for teleporting to locaiton
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 current = transform.position;

        Vector2 offset = Vector2.ClampMagnitude(mouseWorld - current, teleportRadius);
        Vector2 target = current + offset;

        rb.position = target;
        transform.position = target;

        //reset the tp cooldown
        nextTpTime = Time.time + cooldown;

    }

}

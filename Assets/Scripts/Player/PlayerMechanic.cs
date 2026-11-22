using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMechanic : MonoBehaviour
{
    [SerializeField]
    private float teleportRadius = 5.0f;

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
        //mouse poition for teleporting to locaiton
        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 current = transform.position;

        Vector2 offset = Vector2.ClampMagnitude(mouseWorld - current, teleportRadius);
        Vector2 target = current + offset;

        rb.position = target;
        transform.position = target;

    }

}

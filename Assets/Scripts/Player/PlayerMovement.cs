using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 mousePos;
    public Transform rotationPoint;
    public Transform staff;

    public Rigidbody2D playerRB;
    public float xInput;
    public float yInput;
    public float speed = 5;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseworld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (Vector2)mouseworld - (Vector2)rotationPoint.position;
        //mouse rotation toward the mouse
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotationPoint.rotation = Quaternion.Euler(0f, 0f, angle);

        if (staff != null)
        {
            Vector3 scale = staff.localScale;
            scale.y = Mathf.Abs(scale.y) * (dir.x < 0 ? -1f : 1f);
            staff.localScale = scale;
        }



        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        playerRB.linearVelocity = new Vector2(speed * xInput, speed * yInput);
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


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
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        playerRB.linearVelocity = new Vector3(speed * xInput, speed *yInput, 0);
    }
}

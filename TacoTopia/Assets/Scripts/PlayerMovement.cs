using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator animate;

    private float xSpeed = 10;
    private float ySpeed = 4;

    private float scaleMultiplier = 1;

    private bool isGrounded;

    //This method is called once upon start
    private void Awake() {

        //Assigning the physics and animation of the player
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();

    }

    //This method is called every frame
    private void Update() {

        float direction = Input.GetAxis("Horizontal");
        
        //Checking for left/right and a/d key input, and moving the respective direction
        body.velocity = new Vector2(direction * xSpeed, body.velocity.y);

        //Jumping when space is pressed
        if (Input.GetKey(KeyCode.Space) && isGrounded) Jump();     

        //Flip directions based on input
        if (direction > 0.01f) {
            transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
        } else if (direction < -0.01f) {
            transform.localScale = new Vector3 (scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);
        }
    }

    //Changes Y values and specifies not on ground
    private void Jump() {

        body.velocity = new Vector2(body.velocity.x, ySpeed);
        isGrounded = false;

    }

    //When touching the ground, set isGrounded to true
    private void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }
}

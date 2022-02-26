using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private BoxCollider2D collision;
    private Animator animate;

    [SerializeField] private LayerMask ground;

    [SerializeField] private float xSpeed = 10;
    [SerializeField] private float ySpeed = 10;

    [SerializeField] private float scaleMultiplier = 1;

    //This method is called once upon start
    private void Awake() {

        //Assigning the physics and animation of the player
        body = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();

    }

    //This method is called every frame
    private void Update() {

        float direction = Input.GetAxis("Horizontal");
        
        //Checking for left/right and a/d key input, and moving the respective direction
        body.velocity = new Vector2(direction * xSpeed, body.velocity.y);

        //Jumping when space is pressed
        if (Input.GetButton("Jump") && IsGrounded()) Jump();     

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

    }

    //When touching the ground, set isGrounded to true
    private bool IsGrounded () {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}

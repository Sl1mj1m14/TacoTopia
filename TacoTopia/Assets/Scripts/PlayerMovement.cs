using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;

    private float xSpeed = 10;
    private float ySpeed = 4;

    private float scaleMultiplier = 10;

    //This method is called once upon start
    private void Awake() {

        //Assigning the "body" of the player (basically just the physics)
        body = GetComponent<Rigidbody2D>();

    }

    //This method is called every frame
    private void Update() {

        float direction = Input.GetAxis("Horizontal");
        
        //Checking for left/right and a/d key input, and moving the respective direction
        body.velocity = new Vector2(direction * xSpeed, body.velocity.y);

        //Jumping when space is pressed
        if (Input.GetKey(KeyCode.Space)) 
            body.velocity = new Vector2(body.velocity.x, ySpeed);
        

        //Flip directions based on input
        if (direction > 0.01f) {
            transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
        } else if (direction < -0.01f) {
            transform.localScale = new Vector3 (scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);
        }

    }

}

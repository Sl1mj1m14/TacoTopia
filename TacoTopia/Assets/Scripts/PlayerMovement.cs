using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;

    private int xSpeed = 10;

    //This method is called once upon start
    private void Awake() {

        //Assigning the "body" of the player (basically just the physics)
        body = GetComponent<Rigidbody2D>();

    }

    //This method is called every frame
    private void Update() {

        //Checking for left/right and a/d key input, and moving the respective direction
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * xSpeed,body.velocity.y);

    }

}

//Created by Keiler
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private CapsuleCollider2D collision;
    private Animator animate;

    private int sceneNumber;

    [SerializeField] private LayerMask ground;

    [SerializeField] private float xSpeed = 10;
    [SerializeField] private float ySpeed = 10;

    private bool isSquashed;

    public float health = 100;

    [SerializeField] private float scaleMultiplier = .6f;

    //This method is called once upon start
    private void Awake() {

        //Ensuring only one player exists
        if (GameObject.FindObjectsOfType<PlayerMovement>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);
        
        //Assigning the physics and animation of the player
        body = GetComponent<Rigidbody2D>();
        collision = GetComponent<CapsuleCollider2D>();
        animate = GetComponent<Animator>();

        //Caling a method to determine player spawn position based on scene
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    //This method is called every frame
    private void Update() {

        //If player is dead, reset health and drop all items
        if (health <= 0) {

            GetComponent<ItemCollector>().DropAll();
            health = 100;
            SetSpawn();
            
        }

        float direction = Input.GetAxis("Horizontal");
        
        if(sceneNumber >= 2) {
        
            //Checking for left/right and a/d key input, and moving the respective direction
            body.velocity = new Vector2(direction * xSpeed, body.velocity.y);

            //Jumping when space is pressed
            if (Input.GetButton("Jump") && IsGrounded()) Jump(); 

            //Flip directions based on input
            if (direction > 0.01f) {

                //if (IsGrounded()) 
                transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
                //else transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier + 0.1f, scaleMultiplier);

            } else if (direction < -0.01f) {

                //if (IsGrounded()) 
                transform.localScale = new Vector3 (scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);
                //else transform.localScale = new Vector3 (scaleMultiplier * -1, scaleMultiplier + 0.1f, scaleMultiplier);

            } else {
                if (isSquashed) {
                    
                } else {

                }
            }

            if (IsGrounded()) 
                transform.localScale = new Vector3 (transform.localScale.x, scaleMultiplier, scaleMultiplier);
            else 
                transform.localScale = new Vector3 (transform.localScale.x, scaleMultiplier + 0.05f, scaleMultiplier);
        }
    }

    //Changes Y values and specifies not on ground
    private void Jump() {

        body.velocity = new Vector2(body.velocity.x, ySpeed);

    }

    //When touching the ground, set isGrounded to true
    private bool IsGrounded() {

        return Physics2D.CapsuleCast(collision.bounds.center, collision.bounds.size, CapsuleDirection2D.Vertical, 0f, Vector2.down, .1f, ground);
    }

    //Subtracts the damage from player health
    public void Damage(float damage) {
        health -= damage;
    }

    //This method places the player in the correct location in the scene when it spawns in
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Checks what level is loaded
        sceneNumber = scene.buildIndex;

        if (health <= 25) health += 25;

        SetSpawn();
    }

    public void SetSpawn() {
        //Resets player movement and health
        body.velocity = new Vector2(0, 0);
        
        //Checks for player spawn location based on level    
        switch (sceneNumber)
        {
            case 0:
                gameObject.transform.position = new Vector3(-100f,-100f,0f);
                transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
                break;
            
            case 1:
                gameObject.transform.position = new Vector3(-47.43f,9.49f,-5.07f);
                transform.localScale = new Vector3 (1, 1, 1);
                break;
            
            case 2:
                gameObject.transform.position = new Vector3(-83f,25f,0);//Real Level 1 code
                transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
                //gameObject.transform.position = new Vector3(-39f,9f,-5f);//Testing level code
                break;

            default:
                gameObject.transform.position = new Vector3(0,0,0);
                transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
                break;
        }
    }

    //  Method for handling collisions with wall entities
   /* private bool WallCollideAction(int direct){
        if(death.IsDead()){
            return false;
        }else if(direct > 0.01f){
            //return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector3.right, .0f, wall);
        }else if(direct < -0.01f){
            //return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector3.left, .0f, wall);
        }else{
            return false;
        }
    }*/
}

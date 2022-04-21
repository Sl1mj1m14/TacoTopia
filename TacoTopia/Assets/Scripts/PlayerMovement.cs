//Created by Keiler
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    private BoxCollider2D collision;
    private Animator animate;

    private Death death;

    private int sceneNumber;

    [SerializeField] private LayerMask ground;

    [SerializeField] private float xSpeed = 10;
    [SerializeField] private float ySpeed = 10;

    [SerializeField] private float scaleMultiplier = 1;

    //This method is called once upon start
    private void Awake() {

        if (GameObject.FindObjectsOfType<PlayerMovement>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);
        
        //Assigning the physics and animation of the player
        body = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().buildIndex < 2)
            Physics.gravity = new Vector3(0, 0, 0);
        else
            Physics.gravity = new Vector3(0, -9.81f, 0);

    }

    private void Start() {
        death = GetComponent<Death>();
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    //This method is called every frame
    private void Update() {

        float direction = Input.GetAxis("Horizontal");
        
        if(sceneNumber >= 2) {
        
            //Checking for left/right and a/d key input, and moving the respective direction
            body.velocity = new Vector2(direction * xSpeed, body.velocity.y);

            //Jumping when space is pressed
            if (Input.GetButton("Jump") && IsGrounded()) Jump();     

            //Flip directions based on input
            if (direction > 0.01f) {
                //if(!WallCollideAction((int)(direction)))
                    transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
            } else if (direction < -0.01f) {
                //if(!WallCollideAction((int)direction))
                    transform.localScale = new Vector3 (scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);
            }
        }
    }

    //Changes Y values and specifies not on ground
    private void Jump() {

        body.velocity = new Vector2(body.velocity.x, ySpeed);

    }

    //When touching the ground, set isGrounded to true
    private bool IsGrounded() {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    /*
    * This method places the player in the correct location in the scene when it spawns in
    */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        //if (scene.buildIndex == 0) gameObject.SetActive(false);

        switch (scene.buildIndex)
        {
            case 0:
                gameObject.transform.position = new Vector3(-100f,-100f,0f);
                break;
            
            case 1:
                gameObject.transform.position = new Vector3(-47.43f,9.49f,-5.07f);
                break;
            
            case 2:
                //gameObject.transform.position = new Vector3(-83,23,0);//Real Level 1 code
                gameObject.transform.position = new Vector3(-39f,9f,-5f);//Testing level code
                break;

            default:
                gameObject.transform.position = new Vector3(0,0,0);
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

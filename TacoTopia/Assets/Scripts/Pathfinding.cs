//Created by Keiler on 3/8/22
//Last Edited by Andrew R on 3/15/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    private static readonly string PLAYER_REFERENCE = "Player";
    private Rigidbody2D body;
    private BoxCollider2D physicsCollision;

    [SerializeField] private LayerMask ground;

    [SerializeField] private float xSpeed = 3;
    [SerializeField] private float ySpeed = 3;

    private float collisionChecker = .5f;

    private float posX,negX,posY,negY;
    private int target,dir;
    private bool atTarget = true;
    private bool isTargetPlayer = false;

    private System.Random rand = new System.Random();

    
    // Start is called before the first frame update
    void Start()
    {
        posX = GameObject.Find("Right Constraint").transform.position.x;
        negX = GameObject.Find("Left Constraint").transform.position.x;
        posY = GameObject.Find("Top Constraint").transform.position.y;
        negY = GameObject.Find("Bottom Constraint").transform.position.y;
        body = GetComponent<Rigidbody2D>();
        physicsCollision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(target);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<Collider2D>());
        
        if (isTargetPlayer) TrackPlayer();
        
        if (atTarget) 
            GetRandTarget();
         else 
            atTarget = Move(dir);
        
    }

    /*
    * This method determines a random pathfinding target
    */
    public void GetRandTarget()
    {
        target = rand.Next((int)negX,(int)posX);
        //Debug.Log(target);
        atTarget = false;
        if (transform.position.x < target) 
            dir = 1;
        else
            dir = -1;
    }
    
    /*
    * This method allows the enemy to move towards the target
    */
    public bool Move(int dir)
    {
        if (IsObstacle(dir) && IsGrounded()) body.velocity = new Vector2(body.velocity.x, ySpeed);
        body.velocity = new Vector2(dir * xSpeed, body.velocity.y);

        if ((dir > 0 && transform.position.x > target) || (dir < 0 && transform.position.x < target)) {
            if (isTargetPlayer) {
                //Damage()
                //Idle()
                return false;
            }
            return true;
        } else {
            return false;
        }
    }

    /*
    * This method checks if there is an object in front of the enemy, so it knows when to jump
    */
    public bool IsObstacle(int dir)
    {
        if (dir > 0) {
            return Physics2D.BoxCast(physicsCollision.bounds.center, physicsCollision.bounds.size, 
            0f, Vector2.right, collisionChecker, ground);
        } else {
            return Physics2D.BoxCast(physicsCollision.bounds.center, physicsCollision.bounds.size, 
            0f, Vector2.left, collisionChecker, ground);
        }
       
    }

    /*
    * This method checks if the enemy is on the ground
    */
    private bool IsGrounded() {
        return Physics2D.BoxCast(physicsCollision.bounds.center, physicsCollision.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    public void SetTarget(float pos)
    {
        target = (int)pos;
        atTarget = false;
    }

    public void TargetPlayer()
    {
        if (isTargetPlayer) return;
        TrackPlayer();
        atTarget = false;
        if(!Death.get);     //if pc is dead, targeting doesn't work
        isTargetPlayer = true;
    }

    private void TrackPlayer()
    {
        target = (int)GameObject.Find(PLAYER_REFERENCE).transform.position.x;
        if (transform.position.x < target) {
                dir = 1;
                target += 3;
        } else {
                dir = -1;
                target -= 3;
        }
    }
}

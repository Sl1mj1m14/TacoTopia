//Created by Keiler on 3/8/22
//Last Edited by Keiler on 3/8/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    private Rigidbody2D body;
    private BoxCollider2D collision;

    [SerializeField] private LayerMask ground;

    [SerializeField] private float xSpeed = 3;
    [SerializeField] private float ySpeed = 3;
    private float collisionChecker = .5f;

    private float posX,negX,posY,negY;
    private int target,dir;
    private bool atTarget = true;

    private System.Random rand = new System.Random();

    
    // Start is called before the first frame update
    void Start()
    {
        posX = GameObject.Find("Right Constraint").transform.position.x;
        negX = GameObject.Find("Left Constraint").transform.position.x;
        posY = GameObject.Find("Top Constraint").transform.position.y;
        negY = GameObject.Find("Bottom Constraint").transform.position.y;
        body = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atTarget) 
            GetRandTarget();
         else 
            atTarget = Move(dir);
        
    }

    public void GetRandTarget()
    {
        target = rand.Next((int)negX,(int)posX);
        Debug.Log(target);
        atTarget = false;
        if (transform.position.x < target) 
            dir = 1;
            else
            dir = -1;
    }
    
    public bool Move(int dir)
    {
        if (IsObstacle(dir) && IsGrounded()) body.velocity = new Vector2(body.velocity.x, ySpeed);
        body.velocity = new Vector2(dir * xSpeed, body.velocity.y);
        if ((dir > 0 && transform.position.x > target) || (dir < 0 && transform.position.x < target))
            return true;
            else
            return false;
    }

    public bool IsObstacle(int dir)
    {
        if (dir > 0)
            return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.right, collisionChecker, ground);
            else
            return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.left, collisionChecker, ground);
    }

    private bool IsGrounded() {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, ground);
    }
}

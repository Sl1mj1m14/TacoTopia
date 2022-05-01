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

    private Inventory inventory;
    private GameObject[] prefabs;

    //[SerializeField] private LayerMask ground;

    [SerializeField] private float scaleMultiplier = 0.5f;
    [SerializeField] private float xSpeed = 3;

    [SerializeField] public int posX,negX;
    private int xTarget,dir;

    private bool isIdle = false;
    private float idleAmount,idleTimer;

    private float health = 50;

    private System.Random rand = new System.Random();

    
    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        physicsCollision = GetComponent<BoxCollider2D>();
        inventory = GetComponent<Inventory>();

        prefabs = GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<ItemCollector>().GetPrefabs();

        xTarget = RandX();
        if (xTarget < gameObject.transform.position.x) dir = -1; 
        else dir = 1;
        Debug.Log(xTarget);

        idleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {

            DropAll();
            Destroy(this.gameObject);
            
        }

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<CapsuleCollider2D>());
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.Find("Level1Tables").GetComponent<Collider2D>());

        if (isIdle) {

            idleTimer += Time.deltaTime;

            if (idleTimer >= idleAmount) {
                isIdle = false;
                idleTimer = 0;
            }

        } else {

            if (Target()) {

                xTarget = RandX();

                if (rand.Next(1,10) > 3) {
                    idleAmount = (float)rand.Next(1,8);
                    isIdle = true;
                    body.velocity = new Vector2(0, body.velocity.y);
                }

                if (xTarget < gameObject.transform.position.x) dir = -1; 
                else dir = 1;

                Debug.Log(xTarget);
            }
        }  
    }

    //Subtracts the damage from the health
    public void Damage(float damage) {
        health -= damage;
    }

    //Turns the entire inventory into items
    public void DropAll()
    {
        for (int i = 0; i < inventory.GetInventory(); i++) {

            for (int k = 0; k < prefabs.Length; k++) {

                Debug.Log(prefabs[k].name);
                
                while (string.Equals(prefabs[k].name, inventory.GetItem(i))) {

                Instantiate(prefabs[k], 
                    new Vector3(gameObject.transform.position.x + rand.Next(-2,2), gameObject.transform.position.y + rand.Next(-2,2),0), 
                    Quaternion.identity);
                    inventory.RemoveItem(i);
                
                }
            }
        }
    }

    public bool Target()
    {

        if (xTarget < gameObject.transform.position.x && dir < 0) {
            body.velocity = new Vector2(xSpeed * -1, body.velocity.y);
            transform.localScale = new Vector3(scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);
            return false;

        } else if (xTarget > gameObject.transform.position.x && dir > 0) {
            body.velocity = new Vector2(xSpeed, body.velocity.y);
            transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
            return false;

        } else {
            return true;
        }
    }

    public int RandX()
    {
        return rand.Next(negX,posX);       
    }
}

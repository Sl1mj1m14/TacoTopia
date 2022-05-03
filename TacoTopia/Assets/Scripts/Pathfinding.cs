//Created by Keiler on 3/8/22
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

    private bool isInvincible = true;
    public bool isSatisfied = false;

    public bool isAggressive = false;
    public int aggressiveTimer = 30;

    private bool canAttack = false;
    private float attackCooldown = 0;

    private float health = 50;

    public string[] foodItems;
    public string foodItem;

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

        idleTimer = 0;
        foodItem = foodItems[rand.Next(0,foodItems.Length-1)];
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.AddItem("Fork")) {
            inventory.SwitchItems(0,1);
            inventory.AddItemCheck(foodItem);
        }
        
        if (health <= 0) {

            body.velocity = new Vector2(body.velocity.x, body.velocity.y + 10);
            DropAll();
            Destroy(this.gameObject);
            
        }

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<CapsuleCollider2D>());
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.Find("Level1Tables").GetComponent<Collider2D>());

        GameObject[] ignoreCollisions = GameObject.FindGameObjectsWithTag("Patron");

        for (int i = 0; i < ignoreCollisions.Length; i++) {

            //if (ignoreCollisions[i].name != gameObject.name) {

                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),ignoreCollisions[i].GetComponent<BoxCollider2D>());
            //}
        }

        if (inventory.GetItem(0) == foodItem || isSatisfied == true) {
            isSatisfied = true;
            isAggressive = false;
            isIdle = false;
            idleTimer = 0;

            xTarget = negX - 10;
            dir = -1;

            if (Target()) Destroy(this.gameObject);
            //else return;
        }

        if (isAggressive) {

            TargetPlayer();

        } else if (isIdle) {

            idleTimer += Time.deltaTime;

            if (idleTimer >= idleAmount) {
                isIdle = false;
                idleTimer = 0;

                if (rand.Next(1,aggressiveTimer) == 1) {

                    isAggressive = true;
                    isInvincible = false;
                    xSpeed += 2;

                    inventory.RemoveItemCheck(foodItem);
                    inventory.AddItemCheck("Void");
                    inventory.SwitchItems(0,1);

                } else {
                    aggressiveTimer--;
                }
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
            }
        }  
    }

    //Subtracts the damage from the health
    public void Damage(float damage) {
        if (isInvincible) return;

        if (GameObject.FindWithTag(PLAYER_REFERENCE).transform.position.x < gameObject.transform.position.x)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + 10);
        else 
            body.velocity = new Vector2(body.velocity.x, body.velocity.y + 10);

        health -= damage;
    }

    //Turns the entire inventory into items
    public void DropAll()
    {
        for (int i = 0; i < inventory.GetInventory(); i++) {

            for (int k = 0; k < prefabs.Length; k++) {
                
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

    public void TargetPlayer()
    {
        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;

        if (canAttack && attackCooldown <= 0) {
            GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<PlayerMovement>().Damage(10);
            attackCooldown = 5f;

        } else if (!canAttack) {

            if (GameObject.FindWithTag(PLAYER_REFERENCE).transform.position.x < gameObject.transform.position.x) {

                body.velocity = new Vector2(xSpeed * -1, body.velocity.y);
                transform.localScale = new Vector3(scaleMultiplier * -1, scaleMultiplier, scaleMultiplier);

            } else if (GameObject.FindWithTag(PLAYER_REFERENCE).transform.position.x > gameObject.transform.position.x){

                body.velocity = new Vector2(xSpeed, body.velocity.y);
                transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);

            } else {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        } else {
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    public int RandX()
    {
        return rand.Next(negX,posX);       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character") canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character") canAttack = false;
    }
}

//Created by Keiler on 3/8/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pathfinding : MonoBehaviour
{
    
    private static readonly string PLAYER_REFERENCE = "Player";
    private Rigidbody2D body;
    private BoxCollider2D physicsCollision;

    private AudioSource audioSource;
    public AudioClip clip;

    private Inventory inventory;
    private GameObject[] prefabs;

    [SerializeField] private float scaleMultiplier = 0.5f;
    [SerializeField] private float xSpeed = 3;

    [SerializeField] public int posX,negX;
    [SerializeField] private int xTarget,dir;

    private bool isIdle = false;
    private float idleAmount,idleTimer;

    private bool isInvincible = true;
    public bool isSatisfied = false;

    public bool isAggressive = false;
    public int aggressiveTimer = 100;

    public bool canAttack = false;
    private float attackCooldown = 0;

    private float health = 50;

    public string[] foodItems;
    public string foodItem;

    private System.Random rand = new System.Random();

    
    // Start is called before the first frame update
    void Start()
    {

        //Assigning game components
        body = GetComponent<Rigidbody2D>();
        physicsCollision = GetComponent<BoxCollider2D>();
        inventory = GetComponent<Inventory>();
        audioSource = GetComponent<AudioSource>();

        //Getting list of prefabs for item drops
        prefabs = GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<ItemCollector>().GetPrefabs();

        //Setting initial pathfinding target and setting idle timer to 0
        if (gameObject.name != "Final Patron(Clone)") xTarget = RandX();
        else xTarget = posX;
        if (xTarget < gameObject.transform.position.x) dir = -1; 
        else dir = 1;
        idleTimer = 0;

        //Assigning a valid food item
        foodItem = foodItems[rand.Next(0,foodItems.Length-1)];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name == "Final Patron(Clone)" && Target()) 
            body.velocity = new Vector2(0, body.velocity.y);
        
        //Adding a fork as a weapon to the enemy and creating a valid item check
        //based on the food item
        if (inventory.AddItem("Fork")) {
            inventory.SwitchItems(0,1);
            inventory.AddItemCheck(foodItem);
        }
        
        //Killing the player and dropping items if health is below 0
        if (health <= 0) {

            body.velocity = new Vector2(body.velocity.x, body.velocity.y + 10);
            DropAll();
            Destroy(this.gameObject);
            
        }

        //Ignoring collisions of tables, the player, and other enemies
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),
            GameObject.FindWithTag(PLAYER_REFERENCE).GetComponent<CapsuleCollider2D>());
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.Find("Level1Tables").GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),GameObject.Find("Level1KitchenObjects").GetComponent<Collider2D>());

        GameObject[] ignoreCollisions = GameObject.FindGameObjectsWithTag("Patron");

        for (int i = 0; i < ignoreCollisions.Length; i++)
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),ignoreCollisions[i].GetComponent<BoxCollider2D>());

        //Checking if the enemy is satisfied, if so travel out of restaurant and despawn
        if (inventory.GetItem(0) == foodItem || isSatisfied == true) {

            if (gameObject.name == "Final Patron(Clone)") {
                Instantiate(prefabs[1], gameObject.transform.position, Quaternion.identity);
                GameObject.Find("GameControl").GetComponent<GameControl>().SetLevelBegin(2);
                Destroy(this.gameObject);
            }
            
            isSatisfied = true;
            isAggressive = false;
            isIdle = false;
            idleTimer = 0;

            xTarget = negX - 10;
            dir = -1;

            if (Target()) {
                GameObject.Find("GameControl").GetComponent<GameControl>().level1Satisfaction++;
                Destroy(this.gameObject);
            }
            //else return;
        }

        //Checking if enemy is aggressive, if so target player
        if (isAggressive) {

            TargetPlayer();

        //Checking if enemy is idle
        } else if (isIdle) {

            idleTimer += Time.deltaTime;

            if (idleTimer >= idleAmount) {
                isIdle = false;
                idleTimer = 0;

                //Randomly deciding if enemy should be aggressive, if not
                //decreasing the random checks for aggressiveness
                if (rand.Next(1,aggressiveTimer) == 1) {

                    isAggressive = true;
                    isInvincible = false;
                    xSpeed += 2;

                    //Removing ability to pick up items if aggressive
                    inventory.RemoveItemCheck(foodItem);
                    inventory.AddItemCheck("Void");
                    inventory.SwitchItems(0,1);

                } else {
                    aggressiveTimer--;
                }
            }

        } else if (!isSatisfied) {

            //Checking if a valid target has been reached, if so either idle
            //or travel to new target
            if (Target() && gameObject.name != "Final Patron(Clone)") {

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

    //Subtracts the damage from the health, adds knockback, and plays hurt sound effect
    public void Damage(float damage) {
        if (isInvincible) return;

        body.velocity = new Vector2(body.velocity.x, body.velocity.y + 10);

        health -= damage;

        audioSource.clip = clip;
        audioSource.Play();
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

    //Travels to the set X value, returns true if target is reached or passed
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

    //Tracks the player's X value, stopping movement if within attacking range
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

    //Returns a valid random X value
    public int RandX()
    {
        return rand.Next(negX,posX);       
    }
}

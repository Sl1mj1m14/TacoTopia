//Created by Keiler
//Last edited by Keiler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ItemCollector : MonoBehaviour
{
  
    private string[] itemTags;
    [SerializeField] private string[] enemyTags;

    public GameObject[] prefabs;
    private TraversableQueue<Collider2D> itemColliders = new TraversableQueue<Collider2D>();
    private TraversableQueue<Collider2D> enemyColliders = new TraversableQueue<Collider2D>();

    private Inventory inventory;
    private PlayerMovement playerMovement;

    private AudioSource audioSource;
    public AudioClip clip;

    private int slot = 0;

    private float attackCooldown = 0;

    private System.Random rand = new System.Random();

    // Start is called before the first frame update
    private void Start() {

        //Assigning game components
        inventory = GetComponent<Inventory>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        //Creating a list of valid item tags that the player can
        //interact with
        itemTags = new string[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++) {
            itemTags[i] = prefabs[i].name;
        }
    }

    // Update is called once per frame
    //Runs button input checks to determine player action
    private void Update() {

        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
        
        if (!itemColliders.IsEmpty() && Input.GetKeyDown(KeyCode.E)) PickUp();
        
        //First tries giving an item to a valid enemy
        //If it fails, it creates an item entity
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!Give()) Drop();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) Attack();

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) Heal();      

        slot = SlotInput();
    
    }
    
    //Adds any item and enemies to respective lists upon collision
    private void OnTriggerEnter2D(Collider2D collision) 
    {

        foreach (string tag in itemTags) 
            if (collision.gameObject.CompareTag(tag)) itemColliders.Add(collision);
        foreach (string tag in enemyTags)
            if (collision.gameObject.CompareTag(tag)) enemyColliders.Add(collision);
    }

    
    //Removes any item and enemies to respective lists upon collision
    private void OnTriggerExit2D(Collider2D collision) 
    {

        for (int i=0; i < itemColliders.Size(); i++) {
            if (itemColliders.Peek(i).gameObject ==  collision.gameObject) {
                itemColliders.Remove(i);
                return;
            }
        }
        for (int i=0; i < enemyColliders.Size(); i++) {
            if (enemyColliders.Peek(i).gameObject ==  collision.gameObject) {
                enemyColliders.Remove(i);
                return;
            }
        }
    }

    //Adds the item last collided with into the inventory
    public void PickUp() {

        if (inventory.AddItem(itemColliders.Peek().gameObject.tag))
        Destroy(itemColliders.Peek().gameObject);
    }

    //If possible, transfers the selected item from this game object's
    //inventory into the inventory of the last valid collider
    public bool Give()
    {
        for (int i = 0; i < enemyColliders.Size(); i++) {
            if (enemyColliders.Peek(i).GetComponent<Inventory>().AddItem(inventory.GetItem(slot))) {
                inventory.RemoveItem(slot);
                return true;
            }
        }

        return false;
    }

    //Creates an item entity of the selected item
    public void Drop()
    {
        string item = inventory.GetItem(slot);

        if (item == "Air") return;

        for (int i = 0; i < prefabs.Length; i++) {
            if (string.Equals(prefabs[i].name, item)) {
                Instantiate(prefabs[i], 
                    new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), 
                    Quaternion.identity);
                inventory.RemoveItem(slot);
                return;
            }
        }
    }

    //Converts entire inventory into item entities, used on death
    public void DropAll()
    {
        for (int i = 0; i < inventory.GetInventory(); i++) {

            for (int k = 0; k < prefabs.Length; k++) {
                
                while (string.Equals(prefabs[k].name, inventory.GetItem(i))) {

                Instantiate(prefabs[k], 
                    new Vector3(gameObject.transform.position.x + rand.Next(-2,2), gameObject.transform.position.y + rand.Next(-1,2),0), 
                    Quaternion.identity);
                    Debug.Log(inventory.RemoveItem(i));
                
                }
            }
        }
    }

    //Deals damage to the last valid enemy collider
    //Amount of damage is based on currently held item
    private void Attack() {

        if (attackCooldown > 0) return;

        string weapon = inventory.GetItem(slot);
        Pathfinding enemy = enemyColliders.Peek().GetComponent<Pathfinding>();

        switch (weapon) {

            case "RedGun":
                Debug.Log("Fire Bullet");
                break;

            case "BlueGun":
                Debug.Log("Fire Bullet");
                break;

            case "Pan":
                enemy.Damage(10);
                break;

            case "Fork":
                enemy.Damage(10);
                break;         

            default:
                enemy.Damage(5);
                break;
        }

        attackCooldown = 0.5f;
    }

    //Consumes the active item if applicable and restores the
    //respective amount of health
    private void Heal()
    {
        string food = inventory.GetItem(slot);

        switch (food)
        {
            case "Taco":
                playerMovement.Heal(10f);
                inventory.RemoveItem(slot);
                audioSource.clip = clip;
                audioSource.Play();
                break;

            default:
                break;
        }
    }

    //Returns the key input to determine the active slot
    public int SlotInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return 2;
        //if (Input.GetKeyDown(KeyCode.Alpha4)) return 3;
        //if (Input.GetKeyDown(KeyCode.Alpha5)) return 4;
        //if (Input.GetKeyDown(KeyCode.Alpha6)) return 5;
        //if (Input.GetKeyDown(KeyCode.Alpha7)) return 6;
        //if (Input.GetKeyDown(KeyCode.Alpha8)) return 7;
        //if (Input.GetKeyDown(KeyCode.Alpha9)) return 8;
        return slot;
    }

    //Sets active slot
    public void SetActiveSlot(int slotChange)
    {
        slot = slotChange;
    }

    
    //Returns active slot for public methods
    //Used for main hand rendering and gui
    public int GetActiveSlot()
    {
        return slot;
    }

    //Returns a list of valid item prefabs
    //Mirrored by enemy
    public GameObject[] GetPrefabs()
    {
        return prefabs;
    }  
}

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

    private void Start() {
        inventory = GetComponent<Inventory>();
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();

        itemTags = new string[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++) {
            itemTags[i] = prefabs[i].name;
        }
    }

    private void Update() {

        if (attackCooldown > 0) attackCooldown -= Time.deltaTime;
        
        if (!itemColliders.IsEmpty() && Input.GetKeyDown(KeyCode.E)) PickUp();
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!Give()) Drop();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) Attack();

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) Heal();      

        slot = SlotInput();
    
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        foreach (string tag in itemTags) 
            if (collision.gameObject.CompareTag(tag)) itemColliders.Add(collision);
        foreach (string tag in enemyTags)
            if (collision.gameObject.CompareTag(tag)) enemyColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {

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

    public void PickUp() {

        if (inventory.AddItem(itemColliders.Peek().gameObject.tag))
        Destroy(itemColliders.Peek().gameObject);
    }

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

        attackCooldown = 2.0f;
    }

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

    public void SetActiveSlot(int slotChange)
    {
        slot = slotChange;
    }

    public int GetActiveSlot()
    {
        return slot;
    }

    public GameObject[] GetPrefabs()
    {
        return prefabs;
    }  
}

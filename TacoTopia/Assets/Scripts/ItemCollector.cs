//Created by Keiler
//Last edited on 4/5/22 by Andrew Roby
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
  
    [SerializeField] private string[] itemTags;
    [SerializeField] private string[] enemyTags;
    private TraversableQueue<Collider2D> itemColliders = new TraversableQueue<Collider2D>();
    private TraversableQueue<Collider2D> enemyColliders = new TraversableQueue<Collider2D>();
    private Inventory inventory;
    private Death death;

    private void Start() {
        inventory = GetComponent<Inventory>();
        death = GetComponent<Death>();
    }

    private void Update() {

        if (!itemColliders.IsEmpty() && Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.Q)) Give();
    
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

    private void PickUp() {
        if(death.IsDead() == false){
            if (inventory.AddItem(itemColliders.Peek().gameObject.tag))
            Destroy(itemColliders.Peek().gameObject);
        }else{
            Debug.Log("Your hands pass through the object. Having an intangible form is limiting...");
        }
    }

    private void Give()
    {
        if(death.IsDead() == false){
            for (int i = 0; i < enemyColliders.Size(); i++) {
                if (enemyColliders.Peek(i).GetComponent<Inventory>().AddItem(inventory.GetItem(0))) {
                    inventory.RemoveItem(0);
                    return;
                }
            }
        }else{
            Debug.Log("You cannot give Ectoplasm");
        }
    }
}

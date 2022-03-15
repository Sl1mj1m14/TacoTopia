//Created by Keiler
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

    private void Start() {
        inventory = GetComponent<Inventory>();
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
        if (inventory.AddItem(itemColliders.Peek().gameObject.tag))
        Destroy(itemColliders.Peek().gameObject);
    }

    private void Give()
    {
        for (int i = 0; i < enemyColliders.Size(); i++) {
            if (enemyColliders.Peek(i).GetComponent<Inventory>().AddItem(inventory.GetItem(0))) {
                inventory.RemoveItem(0);
                return;
            }
        }
    }
}

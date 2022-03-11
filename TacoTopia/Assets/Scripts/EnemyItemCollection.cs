using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemCollection : MonoBehaviour
{
  
    [SerializeField] private string[] itemTags;
    private TraversableQueue<Collider2D> colliders = new TraversableQueue<Collider2D>();
    private Inventory inventory;

    private void Start() {
        inventory = GetComponent<Inventory>();
    }

    private void Update() {

        if (!colliders.IsEmpty() && Input.GetKeyDown(KeyCode.E)) PickUp();
    
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        foreach (string tag in itemTags) 
            if (collision.gameObject.CompareTag(tag)) colliders.Add(collision);   
    }

    private void OnTriggerExit2D(Collider2D collision) {

        for (int i=0; i < colliders.Size(); i++) {
            if (colliders.Peek(i).gameObject ==  collision.gameObject) {
                colliders.Remove(i);
                return;
            }
        }
    }

    private void PickUp() {
        if (inventory.AddItem(colliders.Peek().gameObject.tag))
        Destroy(colliders.Peek().gameObject);
    }
}

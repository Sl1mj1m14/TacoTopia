using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
  
    [SerializeField] private string[] itemTags;
    TraversableQueue<Collider2D> colliders = new TraversableQueue<Collider2D>();
    private int index;

    private void Update() {

        if (!colliders.IsEmpty() && Input.GetKeyDown(KeyCode.E)) PickUp(index);
    
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

    private void PickUp(int index) {
        Destroy(colliders.Peek().gameObject);
    }
}

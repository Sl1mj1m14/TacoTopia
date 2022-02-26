using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    private Collider2D collider;
    
    [SerializeField] private string[] itemTags;
    private int index;

    private bool isCollided;

    private void Update() {

        if (isCollided && Input.GetKeyDown(KeyCode.E)) PickUp(index);
    
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(itemTags[0])); {
            index = 0;
            isCollided = true;
            collider = collision;
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.CompareTag(itemTags[0])); {
            index = -1;
            isCollided = false;
        }

    }

    private void PickUp(int index) {
        Destroy(collider.gameObject);
    }
}

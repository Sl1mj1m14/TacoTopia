using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{

    [SerializeField] private string[] itemTags;
    private void OnTriggerEnter2D (Collider2D collision) {

        if (collision.gameObject.CompareTag(itemTags[0]) && Input.GetKeyDown(KeyCode.E)); {
            Destroy(collision.gameObject);
        }

    }
}

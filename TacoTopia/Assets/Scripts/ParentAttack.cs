using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character") gameObject.transform.parent.GetComponent<Pathfinding>().canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Character") gameObject.transform.parent.GetComponent<Pathfinding>().canAttack = false;
    }
}

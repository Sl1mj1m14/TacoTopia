using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUnrenderer : MonoBehaviour
{

    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Character") render.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Character") render.enabled = true;
    }
}

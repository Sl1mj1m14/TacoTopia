using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloating : MonoBehaviour
{

    private float baseY, radius, speed;

    private bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        radius = .5f;
        baseY = gameObject.transform.position.y-(radius/2);
        speed = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp) {
            transform.Translate(Vector3.up * (-1*radius) * (Time.deltaTime*speed));
            if (gameObject.transform.position.y <= baseY) isUp = false;

        } else {
            transform.Translate(Vector3.up * (radius) * (Time.deltaTime*speed));
            if (gameObject.transform.position.y >= baseY+(radius)) isUp = true;
        }
    }
}

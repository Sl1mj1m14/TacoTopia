using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundItem : MonoBehaviour
{

    private float speed;

    void Start()
    {
        speed = Random.Range(2,6);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime*speed));

        if(gameObject.transform.position.y < -10) Destroy(this.gameObject);
    }      
}

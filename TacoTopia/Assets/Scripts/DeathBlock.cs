
//  Last edited on 4/5/22 by Andrew Roby
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    private Death death;
    // Start is called before the first frame update
    void Start()
    {
        death = GetComponent<Death>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D( Collision2D collider )
    {
        Debug.Log(collider.gameObject.tag);
        Destroy( collider.gameObject );
        Death death = new Death();
        death.OutofBounds();
    }

}

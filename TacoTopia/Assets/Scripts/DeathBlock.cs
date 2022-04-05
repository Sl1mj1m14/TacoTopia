
//  Last edited on 3/31/22 by Andrew Roby
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D( Collision2D collider )
    {
        Debug.Log(collider.gameObject.tag);
        Destroy( collider.gameObject );
        /*Death death = new Death();
        death.OutofBounds();*/
    }

}


//  last edited on 3/29/22 by Andrew Roby
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    // Players Health Amount
    
    public int maxHealth = 100;

    public int curHealth = 100;

    public float healthBarLength;

    void Start ( ) {

        healthBarLength = Screen.width / 2;
    }

    void Update ( ) {

        AdjustCurrentHealth( 0 );

    }

    private void OnGUI ( ){

        GUI.Box( new Rect( 10, 10, healthBarLength, 20 ), curHealth + "/" + maxHealth );
    }

    public void AdjustCurrentHealth( int adj ) {

         curHealth += adj;

        if( curHealth < 0 ){
            curHealth = 0;
            Death(curHealth);
        }
        

        if( curHealth > maxHealth )
            curHealth = maxHealth;

            healthBarLength = ( Screen.width / 2 ) * ( curHealth / ( float )maxHealth );
    }

}
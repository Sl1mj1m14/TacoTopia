using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    // Enemys Health Amount 

    public int maxHealth = 125;

    public int curHealth = 125;

    private Death death;

    public float healthBarLength;

    void Start ( ) {

        death = GetComponent<Death>();

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
            EasterEgg.EnemyDefeatEgg();
            death.EnemyDead(curHealth);
        }
            

        if( curHealth > maxHealth )
            curHealth = maxHealth;

            healthBarLength = ( Screen.width / 2 ) * ( curHealth / ( float )maxHealth );
        }

    }
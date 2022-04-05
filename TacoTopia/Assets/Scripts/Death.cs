//Created on:       3/8/22 by Andrew Roby
//Last Edited:      4/5/22 by Andrew Roby
using UnityEngine;
//im new to C#. please be gentle.
/*
 * yeah...that was genuinely cringe...why cant i think of a better way to word that...
 */
//namespace Death{
    /*
     *  This class allows for a pc (player character) to die
     *
     *  
     *  call "Death(<insert health variable here>);" whenever the pc takes damage 
     */
    public class Death : MonoBehaviour{
        private bool isDead;
        private bool enemyDead;

        /*
         *  This Property allows for returning whether or not the PC is dead
         *
         *  Call "Death.get" as the condition of an if() statement containing code if said code depends on pc being alive or dead
         */
        public bool IsDead()
        {
            return isDead;
        }

        /*
        *   This method sets the death value
        */
        public void DeadSet(bool value)
        {
            isDead = value;
        }

        /*
         *  This method is the constructor
         *
         *  If health is 0, sets IsDead property to "true"
         *
         *  call "Death(<insert health variable here>);" whenever the pc takes damage 
         */
        public Death(float health){
            if(health <= 0f){
                DeadSet(true);
                System.Console.WriteLine("Your body falls to the ground as the world goes dark.");
                EasterEgg.DeathEgg();
            }else{
                DeadSet(false);
            }
        }

        //  This method is called when the player character collides with a "DeathBlock" element
        public void OutofBounds(){
            DeadSet(true);
            EasterEgg.OoBEgg();
        }

        /*
        *   These methods are related to enemy deaths
        */

        //  Method for testing an enemy's death status
        public bool EnemyDeadGet(){
            return enemyDead;
        }

        //  Method for setting an enemy's status as dead
        public void EnemyDeadSet(bool value){
            enemyDead = value;
        }

        //  Method for automated application of "dead" status on an enemy
        public void EnemyDead(float enemyHealth){
            if(enemyHealth <= 0f){
                EnemyDeadSet(true);
            }else{
                EnemyDeadSet(false);
            }
        }
        /*
        *   End enemy death segment
        */

        
        /*
         *  This method enables revival
         *
         *  Call "Death.Revival" if you have a condition that causes the pc to revive
         */
        public void Revival(){
            //Death(100);
            System.Console.WriteLine("You reclaim your body, feeling reinvigorated for the journey ahead.");
            System.Console.WriteLine("Inventory restored!");
            EasterEgg.RevivalEgg();
        }
    }
//}
//Created on:       3/8/22 by Andrew Roby
//Last Edited:      3/18/22 by Keiler MacNeal
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

        /*
         *  This Property allows for returning whether or not the PC is dead
         *
         *  Call "Death.get" as the condition of an if() statement containing code if said code depends on pc being alive or dead
         */
        public bool Get()
        {
            return isDead;
        }

        /*
        *   This method sets the death value
        */
        public void Set(bool value)
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
                Set(true);
                System.Console.WriteLine("Your body falls to the ground as the world goes dark.");
                Debug.Log("You died");
                EasterEgg.DeathEgg();
            }else{
                Set(false);
            }
        }
        
        /*
         *  This method enables revival
         *
         *  Call "Death.Revival" if you have a condition that causes the pc to revive
         */
        public void Revival(){
            Set(false);
            System.Console.WriteLine("You reclaim your body, feeling reinvigorated for the journey ahead.");
            System.Console.WriteLine("Inventory restored!");
            Debug.Log("You have revived");
            EasterEgg.RevivalEgg();
        }
    }
//}
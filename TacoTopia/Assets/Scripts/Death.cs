using UnityEngine;
//im new to C#. please be gentle.
/*
 * yeah...that was genuinely cringe...why cant i think of a better way to word that...
 */
namespace Death{
    /*
     *  This class allows for a pc (player character) to die
     *
     *  call "Death(<insert health variable here>);" whenever the pc takes damage 
     */
    class Death{
        private bool isDead;

        /*
         *  This Property allows for returning whether or not the PC is dead
         */
        static bool IsDead
        {
            get{
                return isDead;
            }
            set{
                isDead = value;
            }
        }

        /*
         *  This method is the constructor
         *
         *  If health is 0, sets IsDead property to "true"
         *  
         *  Call "Death.get" as the condition of an if() statement containing code if said code depends on pc being alive or dead
         */
        static Death(int health){
            if(health = 0){
                Death.set = true;
            }else{
                Death.set = false;
            }
        }
        
        /*
         *  This method enables revival
         *
         *  Call "Death.Revival" if you have a condition that causes the pc to revive
         */
        static void Revival(){
            Death.set = false;
        }
    }
}
//  created by Andrew Roby on 3/29/22
//  last edited by Andrew Roby on 3/29/22
using unityEngine;

    /*
    *   This script contains methods that invoke Easter Eggs
    *
    *   all Easter Eggs are determined via the random number object titled "ran"
    */
    public class EasterEgg : MonoBehavior{
        private System.Random ran = new System.Random();

        /*
        *   This method's Easter Eggs relate to player death
        */
        public void DeathEgg(){
            int randNum1 = ran.Next(0,68);
            if(randNum1 == 68){
                Debug.Log("*sad violins playing*");
            }else{
                randNum2 = ran.Next(0,68);
                if(randNum2 == 68){
                    Debug.Log("That's gonna hurt tomorrow...");
                }else{
                    randNum3 = ran.Next(0,68);
                    if(randNum3 == 68){
                        Debug.Log("Git Gud Scrub");
                    }
                }
            }
        }

        /*
        *   This method's Easter Eggs relate to player revival
        */
        public void RevivalEgg(){
            int rand_num = ran.Next(0,68);
            if(rand_num == 68){
                Debug.Log("*Halo shield regen noises*");
            }
        }

        //  This method's Easter Eggs relate to enemy defeat
        public void EnemyDefeatEgg(){
            int rand_num = ran.Next(0,420);
            if(rand_num == 1){
                Debug.Log("*FF Victory Trumpets*");
            }
        }


    }
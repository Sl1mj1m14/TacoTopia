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

        
        //   This method's Easter Eggs relate to player death
        public void DeathEgg(){
            int randNum1 = ran.Next(0,68);
            int randNum2 = ran.Next(0,68);
            int randNum3 = ran.Next(0,68);

            if(randNum1 == 68){
                Debug.Log("*sad violins playing*");
            }else{

                if(randNum2 == 68){
                    Debug.Log("That's gonna hurt tomorrow...");
                }else{

                    if(randNum3 == 68){
                        Debug.Log("Git Gud Scrub");
                    }
                }
            }
        }

        
        //   This method's Easter Eggs relate to player revival
        public void RevivalEgg(){
            int randNum = ran.Next(0,68);
            if(randNum == 68){
                Debug.Log("*Halo shield regen noises*");
            }
        }

        //  This method's Easter Eggs relate to enemy defeat
        public void EnemyDefeatEgg(){
            int randNum = ran.Next(0,420);
            if(randNum == 1){
                Debug.Log("*FF Victory Trumpets*");
            }
        }

        //  This method's Easter Eggs relate to saving
        public void SaveEgg(){
            int randNum = ran.Next(0,1);
            if (randNum > 0.8)
                Debug.Log("Bonfire Lit");
            else
                Debug.Log("File Saved");
        }
    }
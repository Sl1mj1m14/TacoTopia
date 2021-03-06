//  created by Andrew Roby on 3/29/22
//  Last edited on 4/5/22 by Andrew Roby
using UnityEngine;

    /*
    *   This script contains methods that invoke Easter Eggs
    *
    *   all Easter Eggs are determined via the random number object titled "ran"
    */
    public static class EasterEgg {
        
        static System.Random ran;
        static EasterEgg()
        {
            ran = new System.Random();
        }
        

        
        //   This method's Easter Eggs relate to player death
        public static void DeathEgg(){
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
                    }else{
                        Debug.Log("You died");
                    }
                }
            }
        }

        
        //   This method's Easter Eggs relate to player revival
        public static void RevivalEgg(){
            int randNum = ran.Next(0,68);
            if(randNum == 68){
                Debug.Log("*Halo shield regen noises*");
            }else{
                Debug.Log("You have revived");
            }
        }

        //  This method's Easter Eggs relate to enemy defeat
        public static void EnemyDefeatEgg(){
            int randNum = ran.Next(0,420);
            if(randNum == 1){
                Debug.Log("*FF Victory Trumpets*");
            }else{
                Debug.Log("Enemy slain");
            }
        }

        //  This method's Easter Eggs relate to saving
        public static void SaveEgg(){
            int randNum = ran.Next(0,1);
            if (randNum > 0.8)
                Debug.Log("Bonfire Lit");
            else
                Debug.Log("File Saved");
        }

        //  This method's Easter Eggs relate to deaths triggered by going Out-of-Bounds 
        public static void OoBEgg(){
            int randNum = ran.Next(0,120);
            if(randNum == 120){
                Debug.Log("You noclip into The Backrooms");
            }else{
                Debug.Log("You are fired for bad work ethic");
            }
        }
    }
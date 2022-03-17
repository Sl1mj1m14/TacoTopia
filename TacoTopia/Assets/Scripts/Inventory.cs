//Created by Keiler
//Last Edited by Andrew R on 3/17/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] private int INVENTORY_SIZE = 9;
    [SerializeField] private int STACK_SIZE = 64;

    private Item<string>[] slots;

    void Start()
    {
        slots = new Item<string>[INVENTORY_SIZE];
        
        //Initializing the inventory to be full of "air"
        for (int i=0; i<INVENTORY_SIZE; i++) 
            slots[i] = new Item<string>("Air"); 

    }

    /*
    *   This method returns the item name at the specified index
    */
    public string GetItem(int index) {
        if(Death.get == false){
            return slots[index].GetItem();
        }else{
            Console.WriteLine("Inventory is nothing but ectoplasm. You are a ghost.");
            return "Ectoplasm";
        }
    }
    
    /*
    *   This method adds an item in the first available space, or 
    *   increases the count of an already existing item
    */
    public bool AddItem(string item) {
 
        if(Death.get == false){
            for (int i=0; i<slots.Length; i++) {
                if (slots[i].Equals("Air")) {
                    slots[i].Set(item, 1);
                    return true;
                } else if (slots[i].Equals(item) && slots[i].GetAmount() < STACK_SIZE) {
                    slots[i].IncreaseAmount();
                    return true;
                }
            }
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
        }   
        return false;
    }

    /*
    *   This method removes the specified amount of the items at the
    *   specified index, changing the spot to air if more items are 
    *   removed than exist
    */
    public bool RemoveItems(int position, int amount) {
        
        if(Death.get == false){
            if (position < 0 || position >= slots.Length) return false;

            slots[position].DecreaseAmount(amount);

            if (slots[position].GetAmount() <= 0) slots[position].Set("Air", 0);

            return true;
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
        }
    }

    /*
    *   This method removes the specified amount of the specified item, 
    *   changing the spot to air if more items are removed than exist
    */
    public bool RemoveItems(string name, int amount) {
    
        if(Death.get == false){
            for (int i=0; i<slots.Length; i++) {
                if (slots[i].Equals(name)) return RemoveItems(i, amount);
            }
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
        }
        return false;
    }

    /*
    *   This method removes one of the item at the 
    *   specified position
    */
    public bool RemoveItem(int position) {
    
        if(Death.get == false){
            return RemoveItems(position, 1);
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
        }
    }

    /*
    *   This method removes one of the specified item
    */
    public bool RemoveItem(string name) {
        
        if(Death.get == false){
            return RemoveItems(name, 1);
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
        }
    }

    /*
    *   This method switchs the positions of two items
    */
    public bool SwitchItems(int num1, int num2) {

        if(Death.get == false){
            if (num1 < 0 || num1 >= slots.Length || num2 < 0 || num2 >= slots.Length || num1 == num2) 
                return false;

            Item<string> temp = new Item<string>(slots[num1].GetItem(),slots[num1].GetAmount());

            slots[num1] = new Item<string>(slots[num2].GetItem(),slots[num2].GetAmount());
            slots[num2] = new Item<string>(temp.GetItem(),temp.GetAmount());

            return true;
        }else{
            Console.WriteLine("Inventory is locked. Return to your body to regain access.");
            return false;
        }

    }

    /*
    *   This method returns the amount of empty slots
    */
    public int GetEmptySlots() {

        int emptySlots = 0; 

        if(Death.get == false){
            for (int i=0; i<INVENTORY_SIZE; i++) {
                if (slots[i].Equals("Air")) emptySlots++;
            }
        }

        return emptySlots;

    }

}

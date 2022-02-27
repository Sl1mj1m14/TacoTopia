using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    private static readonly int INVENTORY_SIZE = 9;

    private Item<string>[] slots = new Item<string>[INVENTORY_SIZE];

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<INVENTORY_SIZE; i++) 
            slots[i] = new Item<string>("Air"); 

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(slots[0]);
    }

    public string GetItem(int index) {
        return slots[index].GetItem();
    }
    
    public bool AddItem(string item) {

        for (int i=0; i<INVENTORY_SIZE; i++) {
            if (slots[i].Equals("Air")) {
                slots[i].Set(item, 1);
                return true;
            } else if (slots[i].Equals(item)) {
                slots[i].IncreaseAmount();
                return true;
            }
        }

        return false;
    }

    public bool SwitchItems(int num1, int num2) {

        if (num1 < 0 || num1 > slots.Length || num2 < 0 || num2 > slots.Length) return false;

        Item<string> temp = new Item<string>(slots[num1].GetItem(),slots[num1].GetAmount());

        slots[num1] = new Item<string>(slots[num2].GetItem(),slots[num2].GetAmount());
        slots[num2] = new Item<string>(temp.GetItem(),temp.GetAmount());

        return true;


    }

    public int GetEmptySlots() {

        int emptySlots = 0; 

        for (int i=0; i<INVENTORY_SIZE; i++) {
            if (slots[i].Equals("Air")) emptySlots++;
        }

        return emptySlots;

    }

}

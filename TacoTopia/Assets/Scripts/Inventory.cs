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
        Debug.Log(slots[0]);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    private int INVENTORY_SIZE = 9;

    private string[] items;
    private int[] itemCounts;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<INVENTORY_SIZE; i++) {
            items[i] = "Air";
            itemCounts[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

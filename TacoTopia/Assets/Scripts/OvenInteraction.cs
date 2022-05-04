using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OvenInteraction : MonoBehaviour
{

    
    public GameObject[] itemNames;
    public AudioClip clip;

    private Inventory inventory;
    private AudioSource audioSource;

    public GameObject taco;

    private bool isStarted;

    private System.Random rand = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        audioSource = GetComponent<AudioSource>();

        itemNames = GameObject.Find("GameControl").GetComponent<GameControl>().level1Prefabs;

        //ResetInventory();

    }

    // Update is called once per frame
    void Update()
    {

        if (!isStarted) ResetInventory();

        if (inventory.GetEmptySlots() < inventory.GetValidItemsAmount()) {
            inventory.RemoveItemCheck(inventory.GetItem(Mathf.Abs(inventory.GetEmptySlots()-2)));
            audioSource.clip = clip;
            audioSource.Play();
        }
        
        if (inventory.GetEmptySlots() <= 0) {
            Instantiate (taco, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, 0), Quaternion.identity);
            ResetInventory();
        }

        isStarted = true;

        //Debug.Log("1: "+inventory.GetItemCheck(0)+" 2: "+inventory.GetItemCheck(1)+" 3: "+inventory.GetItemCheck(2));
        //Debug.Log(itemNames[Random.Range(0,itemNames.Length-1)].name);
    }

    public void ResetInventory()
    {
        foreach (GameObject item in itemNames) inventory.RemoveItemCheck(item.name);
        
        for (int i = 0; i < 3; i++) {
            inventory.RemoveItem(i);
            inventory.AddItemCheck(itemNames[Random.Range(0,itemNames.Length-1)].name);
        }
    }

}

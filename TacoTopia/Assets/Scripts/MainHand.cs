//Created by Keiler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{

    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private float scaleMultiplier;

    private Inventory inventory;
    private ItemCollector itemCollector;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //Assigning some stuff, yada yada
        inventory = gameObject.transform.parent.gameObject.GetComponent<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Copying the main hand from the character if not a character object
        if (gameObject.transform.parent.gameObject.name != "Character") {
            itemSprites = GameObject.Find("Character").transform.GetChild(0).GetComponent<MainHand>().GetSprites();
        } else {
            itemCollector = gameObject.transform.parent.gameObject.GetComponent<ItemCollector>();
        }
            
    }

    
    void Update()
    {
        //Setting the sprite to the item at inventory position 0
        if (gameObject.transform.parent.gameObject.name == "Character") {
            spriteRenderer.sprite = ChangeSprite(inventory.GetItem(itemCollector.GetActiveSlot()));
        } else {
            spriteRenderer.sprite = ChangeSprite(inventory.GetItem(0));
        }
        transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
    }

    /*
    *   This method searches the sprites array for the corresponding 
    *   sprite to the item name
    */
    public Sprite ChangeSprite(string name) {

        foreach (Sprite sprite in itemSprites) {
            if ((sprite != null) && string.Equals(sprite.name, name))
            return sprite;
        }

        return null;

    }

    //Returns a list of all item sprites
    //Used by main hand objects of enemies
    public Sprite[] GetSprites() {

        return itemSprites;
    }
}

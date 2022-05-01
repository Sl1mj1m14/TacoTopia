//Created by Keiler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    
    //public string ENTITY_REFERENCE = "Player";

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

    public Sprite[] GetSprites() {

        return itemSprites;
    }
}

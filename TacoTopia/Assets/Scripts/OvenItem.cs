using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenItem : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    private Inventory inventory;
    private MainHand hand;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        inventory = gameObject.transform.parent.GetComponent<Inventory>();
        hand = GameObject.Find("Character").transform.GetChild(0).GetComponent<MainHand>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = hand.ChangeSprite(inventory.GetItemCheck(gameObject.name[4] - '0'));
    }
}

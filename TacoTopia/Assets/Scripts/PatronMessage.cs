using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMessage : MonoBehaviour
{

    
    private SpriteRenderer spriteRenderer;
    private Pathfinding pathfinding;
    private MainHand hand;

    private string spriteName;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (gameObject.name == "MessageItem")
            pathfinding = gameObject.transform.parent.gameObject.transform.parent.GetComponent<Pathfinding>();
        else 
            pathfinding = gameObject.transform.parent.GetComponent<Pathfinding>();

        hand = GameObject.Find("Character").transform.GetChild(0).GetComponent<MainHand>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pathfinding.isAggressive || pathfinding.isSatisfied) spriteRenderer.enabled = false;
        else spriteRenderer.enabled = true;
        
        if (gameObject.name == "MessageItem") {
            spriteName = pathfinding.foodItem;
            spriteRenderer.sprite = hand.ChangeSprite(spriteName);
        }


    }
}

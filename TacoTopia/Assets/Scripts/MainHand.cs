using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : MonoBehaviour
{
    
    private static readonly string PLAYER_REFERENCE = "char";

    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private float scaleMultiplier;

    private Inventory inventory;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find(PLAYER_REFERENCE).GetComponent<Inventory>();
        spriteRenderer = GetComponent<SpriteRenderer>();
            
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = ChangeSprite(inventory.GetItem(0));
        transform.localScale = new Vector3 (scaleMultiplier, scaleMultiplier, scaleMultiplier);
        Debug.Log(inventory.GetItem(0));
    }

    private Sprite ChangeSprite(string name) {

        foreach (Sprite sprite in itemSprites) {
            if ((sprite != null) && string.Equals(sprite.name, name))
            return sprite;
        }

        return null;

    }
}

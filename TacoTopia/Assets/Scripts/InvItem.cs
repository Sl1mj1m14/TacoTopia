using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InvItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Inventory inventory;
    private MainHand hand;
    private Sprite[] sprites;

    private int itemIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        hand = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<MainHand>();

        itemIndex = gameObject.name[4] - '0'; 
        Debug.Log(itemIndex);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = hand.ChangeSprite(inventory.GetItem(itemIndex));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex < 2) spriteRenderer.enabled = false;
        else spriteRenderer.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    public Sprite[] heartStages;

    private int heartIndex;
    private double healthPercent;

    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        heartIndex = gameObject.name[5] - '0'; 
    }

    // Update is called once per frame
    void Update()
    {
        healthPercent = playerMovement.GetHealth()/playerMovement.GetMaxHealth();
        Debug.Log(healthPercent);

        if (healthPercent > (5f/6f)) {

            spriteRenderer.sprite = heartStages[2];

        } else if (healthPercent > (4f/6f)) {

            if (heartIndex == 2) spriteRenderer.sprite = heartStages[1];
            else spriteRenderer.sprite = heartStages[2];

        } else if (healthPercent > (3f/6f)) {

            if (heartIndex == 2) spriteRenderer.sprite = heartStages[0];
            else spriteRenderer.sprite = heartStages[2];

        } else if (healthPercent > (2f/6f)) {

            if (heartIndex == 2) spriteRenderer.sprite = heartStages[0];
            else if (heartIndex == 1) spriteRenderer.sprite = heartStages[1];
            else spriteRenderer.sprite = heartStages[2];

        } else if (healthPercent > (1f/6f)) {

            if (heartIndex == 0) spriteRenderer.sprite = heartStages[2];
            else spriteRenderer.sprite = heartStages[0];

        } else if (healthPercent > (0f/6f)) {

            if (heartIndex == 0) spriteRenderer.sprite = heartStages[1];
            else spriteRenderer.sprite = heartStages[0];

        } else {

            spriteRenderer.sprite = heartStages[0];

        }
    }
}

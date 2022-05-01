using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    private int option;

    private System.Random rand = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        option = rand.Next(0,sprites.Length-1);
        spriteRenderer.sprite = sprites[option];
        
        if (gameObject.name != "hair" && gameObject.name != "eyes") {
            animator = GetComponent<Animator>();
            animator.SetInteger("Option", option);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "hair" && gameObject.name != "eyes")
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x));
    }
}

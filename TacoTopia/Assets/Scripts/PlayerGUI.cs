using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGUI : MonoBehaviour
{

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    private int sceneNumber;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectsOfType<PlayerGUI>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);
        
        player = GameObject.FindWithTag("Player");
        //spriteRenderer = GetComponent<SpriteRenderer>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,5f);

        //if (sceneNumber < 2) spriteRenderer.enabled = false;
       // else spriteRenderer.enabled = true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneNumber = scene.buildIndex;
    }
}

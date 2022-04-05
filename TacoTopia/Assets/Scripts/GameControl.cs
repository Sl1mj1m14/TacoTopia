using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject player;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Instantiate(player, new Vector3(0,0,0), Quaternion.identity);

        DontDestroyOnLoad(player);
    }
    
    void Start()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;

        Debug.Log(sceneNumber);

        if (sceneNumber == 0) player.SetActive(false);
        else player.SetActive(true);

        /*if (sceneNumber < 2) player.GetComponent<PlayerMovement>().enabled == false;
        else player.GetComponent<PlayerMovement>().enabled == true;*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCharBuilder()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject player;
    public string PLAYER_TAG = "Player";
    
    void Awake()
    {
        player = GameObject.FindWithTag(PLAYER_TAG);
        
        //Making sure this object is not duplicated when returning to menu
        if (GameObject.FindObjectsOfType<GameControl>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*
    * This method makes sure the player is inactive during the menu scene
    */
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        //if (scene.buildIndex != 0) player.SetActive(true);
    }

}

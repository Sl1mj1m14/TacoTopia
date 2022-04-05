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
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCharBuilder()
    {
        SceneManager.LoadScene(1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        if (scene.buildIndex == 0) player.SetActive(false);
        else player.SetActive(true);
    }
}

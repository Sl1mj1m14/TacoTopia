using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

    private int sceneNumber;
    private int levelBegin = 0;

    public GameObject[] level1Prefabs;
    public GameObject[] level1Patrons;
    private Vector3[] level1Spawns;
    private float[] level1PatronSpawns = new float[]{-2.5f, 15f, 33f};

    public float itemSpawnTimerMax = 20.0f;
    public float itemSpawnTimer = 0f;

    public float entitySpawnTimerMax = 180.0f;
    public float entitySpawnTimer = 0f;

    private System.Random rand = new System.Random();
    
    void Awake()
    {
        
        //Making sure this object is not duplicated when returning to menu
        if (GameObject.FindObjectsOfType<GameControl>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        
    }

    void Update() 
    {
        switch (sceneNumber)
        {
            case 2:
                
                GameObject.Find("Inventory").GetComponent<SpriteRenderer>().enabled = true;
                
                if (levelBegin == 1) {
                    itemSpawnTimer += Time.deltaTime;
                    entitySpawnTimer += Time.deltaTime;
                }

                if (itemSpawnTimer >= itemSpawnTimerMax) {

                    int foodIndex = rand.Next(0,20);

                    if (foodIndex >= 10) foodIndex = rand.Next(6,9);
                    else if (foodIndex >= 4) foodIndex = rand.Next(2,5);
                    else foodIndex = rand.Next(0,1);

                    Instantiate (level1Prefabs[foodIndex],level1Spawns[foodIndex],Quaternion.identity);
                    Debug.Log(level1Prefabs[foodIndex].name);

                    itemSpawnTimer = 0;
                }

                if (entitySpawnTimer >= entitySpawnTimerMax) {
                    Instantiate (level1Patrons[0], new Vector3(-22f, level1PatronSpawns[rand.Next(0,2)], 0), Quaternion.identity);

                    entitySpawnTimer = 0;
                }
                
                break;

            default:

                GameObject.Find("Inventory").GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        sceneNumber = scene.buildIndex;

        switch (sceneNumber)
        {
            case 2:

                level1Spawns = new Vector3[level1Prefabs.Length];
                
                for (int i = 0; i < level1Prefabs.Length; i++) {
                    level1Spawns[i] = GameObject.FindWithTag(level1Prefabs[i].name).transform.position;
                }

                break;

            default:
                break;
        }
    }

    public void SetLevelBegin(int num)
    {
        
        if (levelBegin != 1 && num == 1) {
            Instantiate (level1Patrons[0], new Vector3(-22f, level1PatronSpawns[0], 0), Quaternion.identity);
        }

        levelBegin = num;
    }
}

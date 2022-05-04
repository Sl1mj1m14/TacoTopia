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
    
    // Start is called before the first frame update
    void Awake()
    {
        
        //Making sure this object is not duplicated when returning to menu
        if (GameObject.FindObjectsOfType<GameControl>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);

        //Assigning active scene number
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update() 
    {
        Debug.Log(level1Prefabs[Random.Range(0,level1Prefabs.Length)].name);
        
        
        //Runs entity and item spawning for level 1
        switch (sceneNumber)
        {
            case 2:
                
                //Enables the rendering of player inventory
                GameObject.Find("Inventory").GetComponent<SpriteRenderer>().enabled = true;
                
                //Starts increasing the spawning timers for enemies and items
                //Starts after player enters building
                if (levelBegin == 1) {
                    itemSpawnTimer += Time.deltaTime;
                    entitySpawnTimer += Time.deltaTime;
                }

                //Spawns an item at a restock location
                if (itemSpawnTimer >= itemSpawnTimerMax) {

                    //Picking a random food item to refill
                    int foodIndex = Random.Range(0,level1Prefabs.Length);

                    //if (foodIndex >= 10) foodIndex = Random.Range(6,9);
                    //else if (foodIndex >= 4) foodIndex = Random.Range(2,5);
                    //else foodIndex = Random.Range(0,1);

                    //Creating the food item
                    Instantiate (level1Prefabs[foodIndex],level1Spawns[foodIndex],Quaternion.identity);

                    //Resetting the food item timer
                    itemSpawnTimer = 0;
                }

                //Spawning an enemy when the enemy timer reaches a certain point
                if (entitySpawnTimer >= entitySpawnTimerMax) {
                    Instantiate (level1Patrons[0], new Vector3(-22f, level1PatronSpawns[rand.Next(0,2)], 0), Quaternion.identity);

                    entitySpawnTimer = 0;
                }
                
                break;

            default:

                //Disabling inventory rendering if the level is not level 1
                GameObject.Find("Inventory").GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    //Sets the scene number and creates list of valid item spawns
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

    //Enables item and enemy spawning for level one
    //Triggered when building is entered
    public void SetLevelBegin(int num)
    {
        
        if (levelBegin != 1 && num == 1) {
            Instantiate (level1Patrons[0], new Vector3(-22f, level1PatronSpawns[0], 0), Quaternion.identity);
        }

        levelBegin = num;
    }
}

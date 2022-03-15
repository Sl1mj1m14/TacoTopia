//Created by Keiler on 3/14/2022
//Last Edited by Keiler on 3/14/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private static readonly string PLAYER_REFERENCE = "Player";
    
    private Pathfinding pathfinding;
    private Collider2D[] objectCollisions;
    
    [SerializeField] private float viewRadius = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        objectCollisions = Physics2D.OverlapCircleAll(transform.position, viewRadius);
        foreach (Collider2D playerCheck in objectCollisions) {
            if (playerCheck.tag == PLAYER_REFERENCE) {
                pathfinding.TargetPlayer();
                Debug.Log(playerCheck.transform.position.x);
                return;
            }
        }
    }

    public bool IsColliding(Collider2D check)
    {
        foreach (Collider2D checks in objectCollisions) {
            if (GameObject.ReferenceEquals(checks, check)) return true;
        }

        return false;
    }
}

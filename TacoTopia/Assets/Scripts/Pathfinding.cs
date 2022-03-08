//Created by Keiler on 3/8/22
//Last Edited by Keiler on 3/8/22
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    private float posX,negX,posY,negY;
    
    // Start is called before the first frame update
    void Start()
    {
        posX = GameObject.Find("Right Constraint").transform.position.x;
        negX = GameObject.Find("Left Constraint").transform.position.x;
        posY = GameObject.Find("Top Constraint").transform.position.y;
        negY = GameObject.Find("Bottom Constraint").transform.position.y;
        Debug.Log(posX);
        Debug.Log(negX);
        Debug.Log(posY);
        Debug.Log(negY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

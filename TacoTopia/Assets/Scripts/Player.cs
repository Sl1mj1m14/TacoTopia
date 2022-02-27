using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;
	void Awake(){
		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		} else {
			Destroy(this);
		}
	}
}

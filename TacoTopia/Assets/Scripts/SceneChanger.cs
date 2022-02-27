using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public string ChangeTo;
	public void SceneTransition(){
		SceneManager.LoadScene(ChangeTo);
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public int ChangeTo;
	public void SceneTransition(){
		SceneManager.LoadScene(ChangeTo);
	}

	public void MenuReturn(){
		SceneManager.LoadScene(0);
	}

	public void CharacterCreator(){
		SceneManager.LoadScene(1);
	}
	
}

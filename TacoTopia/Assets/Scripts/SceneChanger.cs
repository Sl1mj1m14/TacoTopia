//last updated 5/4/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public int ActiveScene;
	void Awake(){
		ActiveScene = SceneManager.GetActiveScene().buildIndex;
	}

	public int ChangeTo;
	public void SceneTransition(){
		SceneManager.LoadScene(ChangeTo);
	}

	public void MenuReturn(){
		SceneManager.LoadScene(0);
	}

	public void CharacterCreator(){
		SceneManager.LoadScene(1);

		LoadChar load = GameObject.Find("LoginSystem").GetComponent<LoadChar>();
		load.load_char();
	}

	public void StartGame(){
		ChangeTo = 2;
		SceneManager.LoadScene(ChangeTo);
	}
	
	public void StartHardGame(){
		SceneManager.LoadScene(5);
	}

	public void NextScene(){
		ChangeTo = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(ChangeTo);
	}

	public void Respawn(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (SceneManager.GetActiveScene().buildIndex > 1){
			NextScene();
		}
	}
	
}

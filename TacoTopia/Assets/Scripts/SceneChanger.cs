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

	public void StartGame(){
		SceneManager.LoadScene(2);
	}

	public void NextScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	private void OnTriggerEnter2D(Collider2D collision){
		if (SceneManager.GetActiveScene().buildIndex > 1){
			NextScene();
		}
	}
	
}

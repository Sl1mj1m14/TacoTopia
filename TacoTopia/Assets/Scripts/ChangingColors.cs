using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingColors : MonoBehaviour
{
	public GameObject panel;
	
	public SpriteRenderer body;
	public Image squareHeadDisplay;

	public int childID;
	
	public Color[] colors;
	public int whatColor = 1;

	void Start()
	{
		body = GameObject.FindWithTag("Player").transform.GetChild(childID).GetComponent<SpriteRenderer>();
	}
	
	void Update(){
		squareHeadDisplay.color = body.color;
		for (int i = 0; i < 6; i++) {
			if (i == whatColor){
				body.color = colors[i];
			}
		}
	}
	public void OpenPanel(){
		panel.SetActive(true);
	}
	public void ClosePanel(){
		panel.SetActive(false);
	}	
	
	public void ChangeHeadColor(int index){
		whatColor = index;
	}
}

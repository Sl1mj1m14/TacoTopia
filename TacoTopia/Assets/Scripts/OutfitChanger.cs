using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitChanger : MonoBehaviour
{
	[Header("Sprite To Change")]
	public SpriteRenderer bodyPart;
	public Image icon;
	
	[Header("Sprites To Cycle Through")]
	public List<Sprite> options = new List<Sprite>();
	
	void Update(){
		icon.sprite = bodyPart.sprite;
		bodyPart.sprite = options[currentOption];
	}
	
	private int currentOption = 0;
	
	public void NextOption()
	{
		currentOption++;
		if(currentOption>=options.Count)
		{
			currentOption = 0;
		}
		
		bodyPart.sprite = options[currentOption];
	}
	
	public void PreviousOption()
	{
		currentOption--;
		if(currentOption<=0)
		{
			currentOption = options.Count -1;
		}
		bodyPart.sprite = options[currentOption];
	}
}

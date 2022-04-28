using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitChanger : MonoBehaviour
{
	[Header("Sprite To Change")]
	public SpriteRenderer bodyPart;
	public Image icon;

	public int childID;
	
	[Header("Sprites To Cycle Through")]
	public List<Sprite> options = new List<Sprite>();

	private Animator playerAnimator, optionAnimator;

	void Start()
	{
		bodyPart = GameObject.FindWithTag("Player").transform.GetChild(childID).GetComponent<SpriteRenderer>();

		if (GameObject.FindWithTag("Player").transform.GetChild(childID).name != "hair" && 
		GameObject.FindWithTag("Player").transform.GetChild(childID).name != "eyes") {
			playerAnimator = GameObject.FindWithTag("Player").transform.GetChild(childID).GetComponent<Animator>();
		}
		optionAnimator = GetComponent<Animator>();
	}
	
	void Update(){
		icon.sprite = bodyPart.sprite;
		bodyPart.sprite = options[currentOption];

		if (GameObject.FindWithTag("Player").transform.GetChild(childID).name != "hair" && 
		GameObject.FindWithTag("Player").transform.GetChild(childID).name != "eyes") {
			playerAnimator.SetInteger("Option", currentOption);
		}
	}
	
	public int currentOption = 0;
	
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

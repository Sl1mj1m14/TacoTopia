//created by Devin
//last updated 3/10/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadChar : MonoBehaviour
{
    //references renderers for character objects
    SpriteRenderer body;
    SpriteRenderer eyes;
    SpriteRenderer hair;
    SpriteRenderer shirt;
    SpriteRenderer legs;

    //lists of sprites fro each character object
    public List<Sprite> eyeSprites = new List<Sprite>();
    public List<Sprite> hairSprites = new List<Sprite>();
    public List<Sprite> shirtSprites = new List<Sprite>();
    public List<Sprite> legSprites = new List<Sprite>();

    //lists of colors for each character object
    public List<Color> bodyColors = new List<Color>();
    public List<Color> hairColors = new List<Color>();
    public List<Color> shirtColors = new List<Color>();
    public List<Color> legColors = new List<Color>();

    //reference to custom class for importing save file
    SerializeChar import = new SerializeChar();


    //gets the instance of renderer from neccessary character objects
    void Start()
    {
        body = GameObject.Find("body").GetComponent<SpriteRenderer>();
        eyes = GameObject.Find("eyes").GetComponent<SpriteRenderer>();
        hair = GameObject.Find("hair").GetComponent<SpriteRenderer>();
        shirt = GameObject.Find("shirt").GetComponent<SpriteRenderer>();
        legs = GameObject.Find("legs").GetComponent<SpriteRenderer>();
    }

    //loads character from json file and applies it to current character
    public void load_char()
    {
        string text = File.ReadAllText(@"\temp\char.json");
        import = JsonUtility.FromJson<SerializeChar>(text);

        body.color = bodyColors[import.bodyIndex];
        eyes.sprite = eyeSprites[import.faceIndex];
        hair.sprite = hairSprites[import.hairIndex];
        hair.color = hairColors[import.hairCIndex];
        shirt.sprite = shirtSprites[import.shirtIndex];
        shirt.color = shirtColors[import.shirtCIndex];
        legs.sprite = legSprites[import.pantsIndex];
        legs.color = legColors[import.pantsCIndex];
        Debug.Log("File loaded");
    }


    //class to hold all saved character data for importing
    private class SerializeChar
    {
        [SerializeField]
        public int hairIndex;
        [SerializeField]
        public int shirtIndex;
        [SerializeField]
        public int faceIndex;
        [SerializeField]
        public int pantsIndex;
        [SerializeField]
        public int bodyIndex;
        [SerializeField]
        public int hairCIndex;
        [SerializeField]
        public int shirtCIndex;
        [SerializeField]
        public int pantsCIndex;
    }
}

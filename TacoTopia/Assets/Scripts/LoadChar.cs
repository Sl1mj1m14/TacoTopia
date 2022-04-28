//created by Devin
//last updated 4/28/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
        WWWForm form = new WWWForm();
        LoginSystem sys = new LoginSystem();
        string username = sys.userName;

        form.AddField("username", username);
        form.AddField("table_name", "player_data");
        form.AddField("field_name", "char_data");
        var download = UnityWebRequest.Post("tacotopia.org/Download.php", form);
        download.SendWebRequest();

        string text = download.downloadHandler.text;
        import = JsonUtility.FromJson<SerializeChar>(text);

        if (download.result != UnityWebRequest.Result.Success)
            Debug.Log(download.error);
        else
        {
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

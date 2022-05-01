//created by Devin
//last updated 4/30/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class SaveChar : MonoBehaviour
{
    //objects to hold references to other scripts
    OutfitChanger hair;
    OutfitChanger shirt;
    OutfitChanger face;
    OutfitChanger pants;
    ChangingColors body;
    ChangingColors hairColor;
    ChangingColors shirtColor;
    ChangingColors pantsColor;

    //reference to custom class for exporting save file
    SerializeChar export = new SerializeChar();


    //gets the instance of the other scripts from neccessary objects
    void Start()
    {
        hair = GameObject.Find("hairSelector").GetComponent<OutfitChanger>();
        shirt = GameObject.Find("shirtSelector").GetComponent<OutfitChanger>();
        face = GameObject.Find("faceSelector").GetComponent<OutfitChanger>();
        pants = GameObject.Find("pantSelector").GetComponent<OutfitChanger>();

        body = GameObject.Find("Char Creation Mang").GetComponent<ChangingColors>();
        hairColor = GameObject.Find("haircolorer").GetComponent<ChangingColors>();
        shirtColor = GameObject.Find("Shirt Creation Mang").GetComponent<ChangingColors>();
        pantsColor = GameObject.Find("pantcolorer").GetComponent<ChangingColors>();
    }

    //keeps index of sprites and colors updated
    void Update()
    {
        export.hairIndex = hair.currentOption;
        export.shirtIndex = shirt.currentOption;
        export.faceIndex = face.currentOption;
        export.pantsIndex = pants.currentOption;

        export.bodyIndex = body.whatColor;
        export.hairCIndex = hairColor.whatColor;
        export.shirtCIndex = shirtColor.whatColor;
        export.pantsCIndex = pantsColor.whatColor;
    }
    
    //saves character data to json file
    public void save_char()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        LoginSystem sys = new LoginSystem();
        string username = sys.userName;

        string text = JsonUtility.ToJson(export);
        form.AddField("username", username);
        form.AddField("table_name", "player_data");
        form.AddField("field_name", "char_data");
        form.AddField("char_data", text);

        using (UnityWebRequest upload = UnityWebRequest.Post("https://tacotopia.org/upload.php", form))
        {
            yield return upload.SendWebRequest();

            if (upload.result != UnityWebRequest.Result.Success)
                Debug.Log(upload.error);
            else
            {
                string responseText = upload.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                    Debug.Log("Character Saved");
                else
                    Debug.Log(responseText);
            }  
        }
    }


    //class to hold all saved character data for exporting
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
//created by Devin
//last updated on 5/4/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class Save : MonoBehaviour
{
    SceneChanger SC;

    SerializeSav export = new SerializeSav();

    public void save()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        LoginSystem sys = GameObject.Find("LoginSystem").GetComponent<LoginSystem>();
        string username = sys.userName;

        SC = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        export.currentLevel = SC.ChangeTo;

        string saveLevel = JsonUtility.ToJson(export);
        form.AddField("username", username);
        form.AddField("field_name", "save_data");
        form.AddField("save_data", saveLevel);

        using (UnityWebRequest upload = UnityWebRequest.Post("https://tacotopia.org/upload.php", form))
        {
            yield return upload.SendWebRequest();

            if (upload.result != UnityWebRequest.Result.Success)
                Debug.Log(upload.error);
            else
            {
                string responseText = upload.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                    EasterEgg.SaveEgg();
                else
                    Debug.Log(responseText);
            }
        }
    }

    private class SerializeSav
    {
        [SerializeField]
        public int currentLevel;
    }
}

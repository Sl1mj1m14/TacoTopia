//created by Devin
//last updated 5/4/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Load : MonoBehaviour
{
    SerializeSav import = new SerializeSav();

    public void load()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        WWWForm form = new WWWForm();
        LoginSystem sys = GameObject.Find("LoginSystem").GetComponent<LoginSystem>();
        string username = sys.userName;

        form.AddField("username", username);
        form.AddField("field_name", "save_data");

        using (UnityWebRequest download = UnityWebRequest.Post("https://tacotopia.org/download.php", form))
        {
            yield return download.SendWebRequest();

            if (download.result != UnityWebRequest.Result.Success)
                Debug.Log(download.error);
            else
            {
                string responseText = download.downloadHandler.text;
                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    string loadLevel = dataChunks[1];

                    import = JsonUtility.FromJson<SerializeSav>(loadLevel);

                    SceneManager.LoadScene(import.currentLevel);
                }
                else
                {
                    Debug.Log(responseText);
                }
            }
        }
    }

    private class SerializeSav
    {
        [SerializeField]
        public int currentLevel;
    }
}

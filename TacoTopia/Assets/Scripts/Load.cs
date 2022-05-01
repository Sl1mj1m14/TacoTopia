//created by Devin
//last updated 4/30/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Load : MonoBehaviour
{
    string currentLevel;

    public void load()
    {
        StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        WWWForm form = new WWWForm();
        LoginSystem sys = new LoginSystem();
        string username = sys.userName;

        form.AddField("username", username);
        form.AddField("table_name", "player_data");
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

                    currentLevel = JsonUtility.FromJson<string>(loadLevel);

                    SceneManager.LoadScene(currentLevel);
                }
                else
                {
                    Debug.Log(responseText);
                }
            }
        }
    }
}

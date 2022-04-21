//created by Devin
//last updated 3/15/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Load : MonoBehaviour
{
    string currentLevel;

    private void load()
    {
        WWWForm form = new WWWForm();

        form.AddField("table_name", "player_data");
        form.AddField("field_name", "save_data");
        var download = UnityWebRequest.Post("tacotopia.org/saveDownload.php", form);
        download.SendWebRequest();

        if (download.result != UnityWebRequest.Result.Success)
            Debug.Log(download.error);
        else
        {
            string loadLevel = download.downloadHandler.text;
            currentLevel = JsonUtility.FromJson<string>(loadLevel);

            SceneManager.LoadScene(currentLevel);
        }
    }
}

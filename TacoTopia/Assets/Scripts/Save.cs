//created by Devin
//last updated on 4/12/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class Save : MonoBehaviour
{
    SceneChanger SC;
    string currentLevel;

    private void save()
    {
        WWWForm form = new WWWForm();

        SC = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        currentLevel = SC.ChangeTo.ToString();

        string saveLevel = JsonUtility.ToJson(currentLevel);
        form.AddField("table_name", "player_data");
        form.AddField("save_data", saveLevel);
        var upload = UnityWebRequest.Post("tacotopia.org/saveUpload.php", form);
        upload.SendWebRequest();
        if (upload.result != UnityWebRequest.Result.Success)
            Debug.Log(upload.error);
        else
            EasterEgg.SaveEgg();
    }
}

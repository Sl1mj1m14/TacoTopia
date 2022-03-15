using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save : MonoBehaviour
{
    SceneChanger SC;
    string currentLevel;

    private void save()
    {
        SC = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        currentLevel = SC.ChangeTo;

        string saveLevel = JsonUtility.ToJson(currentLevel);
        File.WriteAllText(@"\temp\level.json", saveLevel);
        if (Random.value > 0.8)
            Debug.Log("Bonfire Lit");
        else
            Debug.Log("File Saved");
    }
}

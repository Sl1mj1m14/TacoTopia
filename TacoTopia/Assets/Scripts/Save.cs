//created by Devin
//last updated on 3/29/2022 by Andrew Roby

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
        currentLevel = SC.ChangeTo.ToString();

        string saveLevel = JsonUtility.ToJson(currentLevel);
        File.WriteAllText(@"\temp\level.json", saveLevel);
        EasterEgg.SaveEgg();
    }
}

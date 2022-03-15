//created by Devin
//last updated 3/15/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    string currentLevel;

    private void load()
    {
        string loadLevel = File.ReadAllText(@"\temp\level.json");
        currentLevel = JsonUtility.FromJson<string>(loadLevel);

        SceneManager.LoadScene(currentLevel);
    }
}

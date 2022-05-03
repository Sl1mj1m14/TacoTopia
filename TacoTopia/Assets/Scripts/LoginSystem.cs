//created by Devin
//last updated 5/3/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public enum CurrentWindow {Login, Register}
    public CurrentWindow currentWindow = CurrentWindow.Login;

    string loginUsername = "";
    string loginPassword = "";
    string registerUsername = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string errorMessage = "";

    bool isWorking = false;
    bool registrationCompleted = false;
    public bool isLoggedIn = false;

    public Button PlayButton;
    public Button CharacterButton;

    //Logged-in user data
    public string userName = "";

    string rootURL = "https://tacotopia.org/";

    void Awake()
    {
        //Making sure this object is not duplicated when returning to menu
        if (GameObject.FindObjectsOfType<GameControl>().Length == 1)
            DontDestroyOnLoad(gameObject);
        else 
            Destroy(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);

        if(scene.name == "Menu")
        {
            if(isLoggedIn == true)
            {
                PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
                CharacterButton = GameObject.Find("CharacterButton").GetComponent<Button>();

                PlayButton.interactable = true;
                CharacterButton.interactable = true;
            }
        }
    }


    void OnGUI()
    {
        if (!isLoggedIn)
        {
            if (currentWindow == CurrentWindow.Login)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 115, 250, 230),
                    LoginWindow, "Login");
            }
            if (currentWindow == CurrentWindow.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 165, 250, 330),
                    RegisterWindow, "Register");
            }
        }

        GUI.Label(new Rect(555, 5, 1000, 500), "Status: " + (isLoggedIn ? "Logged-in Username: " + userName : "Logged-out"));
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(600, 30, 100, 25), "Log Out"))
            {
                isLoggedIn = false;
                userName = "";
                currentWindow = CurrentWindow.Login;

                SceneManager.LoadScene(0);

                PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
                CharacterButton = GameObject.Find("CharacterButton").GetComponent<Button>();

                PlayButton.interactable = false;
                CharacterButton.interactable = false;
            }
        }
    }

    void LoginWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }
        if (registrationCompleted)
        {
            GUI.color = Color.green;
            GUILayout.Label("Registration Completed");
        }

        GUI.color = Color.white;
        GUILayout.Label("Username:");
        loginUsername = GUILayout.TextField(loginUsername, 20);
        GUILayout.Label("Password:");
        loginPassword = GUILayout.PasswordField(loginPassword, '*', 40);

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(125)))
        {
            StartCoroutine(LoginEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Do not have an acount?");
        if (GUILayout.Button("Register", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Register;
        }
    }

    void RegisterWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Username:");
        registerUsername = GUILayout.TextField(registerUsername, 20);
        GUILayout.Label("Password:");
        registerPassword1 = GUILayout.PasswordField(registerPassword1, '*', 40);
        GUILayout.Label("Confirm Password:");
        registerPassword2 = GUILayout.PasswordField(registerPassword2, '*', 40);

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Already have an account?");
        if (GUILayout.Button("Login", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Login;
        }
    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        var hash = new Hash128();
        hash.Append(registerPassword1);
        var hash2 = new Hash128();
        hash2.Append(registerPassword2);

        WWWForm form = new WWWForm();
        form.AddField("username", registerUsername);
        form.AddField("password1", hash.ToString());
        form.AddField("password2", hash2.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    currentWindow = CurrentWindow.Login;
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        var hash = new Hash128();
        hash.Append(loginPassword);

        WWWForm form = new WWWForm();
        form.AddField("username", loginUsername);
        form.AddField("password", hash.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if(responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    isLoggedIn = true;

                    PlayButton = GameObject.Find("PlayButton").GetComponent<Button>();
                    CharacterButton = GameObject.Find("CharacterButton").GetComponent<Button>();

                    PlayButton.interactable = true;
                    CharacterButton.interactable = true;

                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

    void ResetValues()
    {
        errorMessage = "";
        loginPassword = "";
        registerUsername = "";
        registerPassword1 = "";
        registerPassword2 = "";
    }
}

//created by Devin
//last updated 4/26/2022 by Devin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
    bool isLoggedIn = false;

    
    

    //Logged-in user data
    public string userName = "";

    string rootURL = "tacotopia.org";

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

        GUI.Label(new Rect(5, 5, 1000, 500), "Status: " + (isLoggedIn ? "Logged-in Username :" + userName : "Logged-out"));
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log Out"))
            {
                isLoggedIn = false;
                userName = "";
                currentWindow = CurrentWindow.Login;
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

        var hash = new Hash128();

        GUI.color = Color.white;
        GUILayout.Label("Username:");
        loginUsername = GUILayout.TextField(loginUsername);
        GUILayout.Label("Password:");
        hash.Append(GUILayout.PasswordField(loginPassword, '*'));
        loginPassword = hash.ToString();

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(125)))
        {
            StartCoroutine(RegisterEnumerator());
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

        var hash = new Hash128();
        var hashp2 = new Hash128();

        GUI.color = Color.white;
        GUILayout.Label("Username:");
        registerUsername = GUILayout.TextField(registerUsername, 20);
        GUILayout.Label("Password:");
        hash.Append(GUILayout.PasswordField(registerPassword1, '*', 19));
        registerPassword1 = hash.ToString();
        GUILayout.Label("Confirm Password:");
        hashp2.Append(GUILayout.PasswordField(registerPassword2, '*', 19));
        registerPassword2 = hashp2.ToString();

        GUILayout.Space(5);

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

        WWWForm form = new WWWForm();
        form.AddField("username", registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);

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

        WWWForm form = new WWWForm();
        form.AddField("username", loginUsername);
        form.AddField("password", loginPassword);

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
                    userName = responseText;
                    isLoggedIn = true;

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

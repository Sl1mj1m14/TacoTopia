using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testCon : MonoBehaviour
{
    public void testPost()
    {
        using (UnityWebRequest www = UnityWebRequest.Post("tacotopia.org/upload", "testName"))
        {
            //yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload Complete");
            }
        }
    }
}

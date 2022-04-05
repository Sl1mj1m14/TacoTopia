using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DatabaseConnect : MonoBehaviour
{
    private static IDbConnection dbCon;

    void Start()
    {
        openSqlConnection();
    }

    void OnApplicationQuit()
    {
        closeSqlConnection();
    }

    //Connect to database
    private static void openSqlConnection()
    {
        string conStr = "Server=localhost;" +
                        "Database=db30;" +
                        "User ID=db30;" +
                        "Password=icq;" +
                        "Pooling=false;";
        MySqlConnection dbCon = new MySqlConnection(conStr);
        if (dbCon == null)
            Debug.Log("Failed to Connect");
        else
            Debug.Log("Connection Successful");
        dbCon.Open();
    }

    //Disconnect from Database
    private static void closeSqlConnection()
    {
        dbCon.Close();
        dbCon = null;
        Debug.Log("Disconnected from database");
    }
}

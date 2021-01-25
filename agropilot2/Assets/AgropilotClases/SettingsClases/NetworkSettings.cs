using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// TODO: DISPLAY ERRORS!
/// </summary>
[System.Serializable]
public class NetworkSettings : MonoBehaviour
{

    public string name;
    public string gpsIp;
    public string moduleIp;


    // Start is called before the first frame update
    void Start()
    {

    }
    public void Read() {
        try
        {
            if (name == "" || name == null)
                name = "Default";
            string path = Application.persistentDataPath + "/" + this.GetType().Name
    + "/";
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path + name + ".json"), this);
            if (moduleIp == "" || moduleIp == null)
                moduleIp = "224.5.6.7:1892";
            if (moduleIp == "" || moduleIp == null)
                moduleIp = "192.168.1.1:8080";
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    // Update is called once per frame
    void Save()
    {
        if (name == "" || name == null)
            name = "Default";
        string path = Application.persistentDataPath + "/" + this.GetType().Name
+ "/";
        if (!Directory.Exists(path))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(path);

        }
        Debug.Log(Application.persistentDataPath);
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + name + ".json", json);

    }
    public void setGpsIP(string s)
    {
        gpsIp = s;
        Save();
    }
    public void setModuleIP(string s)
    {
        moduleIp = s;
        Save();
    }
}
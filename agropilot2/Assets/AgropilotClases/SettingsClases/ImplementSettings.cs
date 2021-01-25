using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImplementSettings : MonoBehaviour
{

    public string name;

    /// <summary>
    ///  0: Fixed
    ///  1: Rear with axis
    /// </summary>
    public int linkMode=0;
    public int[] sections;
    public double toolDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setSections(int[] s)
    {
        sections = s;
        Save();
    }

    // Update is called once per frame
    public void Read()
    {
        if (name == "" || name == null)
            name = "Default";
        string path = Application.persistentDataPath + "/" + this.GetType().Name
+ "/";
        JsonUtility.FromJsonOverwrite(File.ReadAllText(path + name + ".json"), this);


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
}

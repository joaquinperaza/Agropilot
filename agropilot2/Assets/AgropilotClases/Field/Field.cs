using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using DotNetCoords;

[Serializable]
public class Field
{
    public String name;
    private List<Path> fieldPaths=new List<Path>();
    private LatLng center;

    public void Save()
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
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + name + ".json", json);

    }

    public void savePath(Path p)
    {
        fieldPaths.Add(p);
        Save();
    }
    public void removePath(Path p)
    {
        fieldPaths.Remove(p);
        Save();
    }

    public LatLng getCenter(LatLng c)
    {
        return center;
    }
    public void setCenter(LatLng c)
    {
        center = c;
        Save();
    }
    public void Read()
    {
        try
        {
            if (name == "" || name == null)
                name = "Default";
            string path = Application.persistentDataPath + "/" + this.GetType().Name
    + "/";
            JsonUtility.FromJsonOverwrite(File.ReadAllText(path + name + ".json"), this);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public Field()
    {

    }
}

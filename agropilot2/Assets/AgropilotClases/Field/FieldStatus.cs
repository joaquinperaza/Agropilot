using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldStatus : MonoBehaviour
{
    public Field currentField;

    public void LoadField(string name) {
        Field temp = new Field();
        temp.name = name;
        temp.Read();
        currentField = temp;
        
    }
    public void NewField(string name)
    {
        Field temp = new Field();
        temp.name = name;
        currentField = temp;
        temp.Save();
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateField : MonoBehaviour
{
    [SerializeField] private InputField fieldName;
    [SerializeField] private FieldStatus fieldStatus;
    [SerializeField] private Text currentField;

    public void create(){
        fieldStatus.NewField(fieldName.text);
        currentField.text = "Current field: " + fieldStatus.currentField.name;
    }
    public void saveAndClose() {
        fieldStatus.currentField.Save();
        fieldStatus.currentField = null;
        currentField.text = "No field opened";
    }

    void Start()
    {
        currentField.text = "Start or open field";
    }

    // Update is called once per frame
    void Update()
    {
    }
}

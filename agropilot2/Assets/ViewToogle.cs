using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewToogle : MonoBehaviour
{
    [SerializeField]
    private Text btn;
    [SerializeField]
    private CameraFollow cam;
    private void Start()
    {

    }
    public void toogle() {
        if (btn.text == "3D")
        {
            cam.back();
            btn.text = "2D";
        }
        else if (btn.text == "2D")
        {
            cam.top();
            btn.text = "3D";
        }


    }
}

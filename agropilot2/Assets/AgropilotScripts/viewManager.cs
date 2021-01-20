using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private UnityEngine.UI.Text txt;
    private int status;
    void Start()
    {
        txt = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    public void zoomIn() {
        cam.fieldOfView -= 10;
    }
    public void zoomOut()
    {
        cam.fieldOfView += 10;
    }

    public void toggle()
    {
        if (status == 0)
        {
            status = 1;
            txt.text = "2D";
            cam.transform.localPosition = new Vector3(0,7,-15);
            cam.transform.localRotation = Quaternion.Euler(12,0,0);

        }
        else if (status == 1)
        {
            status = 0;
            txt.text = "3D";
            cam.transform.localPosition = new Vector3(0, 25, 0);
            cam.transform.localRotation = Quaternion.Euler(90, 0, 0);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLoader : MonoBehaviour
{
    /// <summary>
    /// We need to load settings data to input fields
    /// so we bing input fields to settings objects attributes
    /// </summary>
    [SerializeField]
    private NetworkSettings networkSettings;
    [SerializeField]
    private InputField networkSettings_GPS_IP;
    [SerializeField]
    private InputField networkSettings_MODULE_IP;

    public void LoadNetwork() {
        networkSettings.Read();
        networkSettings_GPS_IP.text = networkSettings.gpsIp;
        networkSettings_MODULE_IP.text = networkSettings.moduleIp;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        LoadNetwork();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

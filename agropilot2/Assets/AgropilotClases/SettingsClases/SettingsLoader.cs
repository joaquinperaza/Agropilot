using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLoader : MonoBehaviour
{
    /// <summary>
    /// We need to load settings data to input fields
    /// so we bind input fields to settings objects attributes
    /// </summary>
    private NetworkSettings networkSettings;
    [SerializeField]
    private InputField networkSettings_GPS_IP;
    [SerializeField]
    private InputField networkSettings_MODULE_IP;


    /// <summary>
    /// Load implements to tractor (and set implement type)
    /// </summary>
    private TractorStatus tractor;
    private TractorSettings tractorSettings;
    private ImplementSettings implemet;

    public void LoadNetwork() {
        networkSettings = GetComponent<NetworkSettings>();
        networkSettings.Read();
        networkSettings_GPS_IP.text = networkSettings.gpsIp;
        networkSettings_MODULE_IP.text = networkSettings.moduleIp;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        networkSettings = GetComponent<NetworkSettings>();
        tractorSettings = GetComponent<TractorSettings>();
        tractor = GetComponent<TractorStatus>();
        implemet = GetComponent<ImplementSettings>();
        LoadNetwork();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

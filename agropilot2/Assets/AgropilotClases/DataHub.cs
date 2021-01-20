using UnityEngine;
using System.Collections;
using System;
using SimpleTCP;

using System.Threading;

public class DataHub : MonoBehaviour
{
    private SimpleTcpClient gpsClient;
    private AgropilotIO api;
    private String IPWatchdog;

    // Use this for initialization
    void Start()
    {
        api = GetComponent<AgropilotIO>();
        IPWatchdog = null;
        Connect(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (api.networkSettings.gpsIp!= IPWatchdog) {
            IPWatchdog = api.networkSettings.gpsIp;
            Connect();
        };
       

    }
    public void Connect()
    {
        try {
            string[] ip = api.networkSettings.gpsIp.Split(':');

            gpsClient = new SimpleTcpClient().Connect(ip[0], int.Parse(ip[1]));
            gpsClient.Delimiter = 0x13;
            gpsClient.DataReceived += (sender, msg) => {

                Debug.Log("GPS NMEA: " + msg.MessageString);

            };
        }
        catch (Exception e){ Debug.Log(e); }
        

    }
    void OnApplicationQuit()
    {
        gpsClient.Disconnect();
    }
}

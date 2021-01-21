using UnityEngine;
using System.Collections;
using System;
using SimpleTCP;
using UnityEngine.UI;
using System.Threading;

public class GpsIO : MonoBehaviour
{
    private SimpleTcpClient gpsClient;
    [SerializeField] private Image gpsLed;
    private bool gpsLedOn = false;
    private AgropilotIO api;
    private String IPWatchdog;
    private int gpsExceptionCounter = 1;
    private TractorStatus tractorStatus;
    public NetworkSettings networkSettings; 
    private TractorSettings tractorSettings;
    private DateTime last;
    

    // Use this for initialization
    void Start()
    {
        api = GetComponent<AgropilotIO>();
        tractorStatus = GetComponent<TractorStatus>();
        networkSettings = GetComponent<NetworkSettings>();
        tractorSettings = GetComponent<TractorSettings>();
        IPWatchdog = null;
        Connect(); 
    }

    // Update is called once per frame
    void gpsLedOff() {
        gpsLedOn = false;
    }
    void Update()
    {
        if (networkSettings.gpsIp!= IPWatchdog) {
            IPWatchdog = networkSettings.gpsIp;
            Connect();
        };
        if (gpsLedOn)
        {
            gpsLed.color = Color.red;
            Invoke("gpsLedOff", 0.5f);
        }
        else
        {
            gpsLed.color = Color.gray;
        }
        if ((DateTime.Now - last).TotalSeconds>10) {
            Debug.Log("GPS is offline, reconnecting");
            gpsExceptionCounter++;
            gpsClient.Disconnect();
            gpsClient.Dispose();
            last = DateTime.Now;
            Invoke("Connect", 1f * gpsExceptionCounter);

        }
    }
    public void Connect()
    {
        last = DateTime.Now;
        try
        {
            gpsClient.Disconnect();
            gpsClient.Dispose();
        }
        catch { }
        try {
            string[] ip = networkSettings.gpsIp.Split(':');
            gpsClient = new SimpleTcpClient().Connect(ip[0], int.Parse(ip[1]));
            gpsClient.Delimiter = 0x13;
            gpsClient.DataReceived += (sender, msg) => {
                last = DateTime.Now;
                gpsLedOn = true;
                Debug.Log("GPS NMEA: " + msg.MessageString);
                gpsExceptionCounter = 0;
                api.Send(msg.Data);
            };

        }
        catch (Exception e){
            gpsExceptionCounter++;
            Debug.Log(e);
            Invoke("Connect", 1f * gpsExceptionCounter);
            try
            {
                gpsClient.Disconnect();
                gpsClient.Dispose();
            }
            catch { }
            
            
        }
        

    }
    void OnApplicationQuit()
    {
        gpsClient.Disconnect();
        gpsClient.Dispose();
    }
}

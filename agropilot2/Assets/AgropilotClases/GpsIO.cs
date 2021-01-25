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
    private int gpsLedOn = 2;
    private AgropilotIO api;
    private String IPWatchdog;
    private int gpsExceptionCounter = 1;
    private TractorStatus tractorStatus;
    public NetworkSettings networkSettings; 
    private TractorSettings tractorSettings;
    private DateTime last;
    private Thread TCPThread;
    

    // Use this for initialization
    void Start()
    {
        api = GetComponent<AgropilotIO>();
        tractorStatus = GetComponent<TractorStatus>();
        networkSettings = GetComponent<NetworkSettings>();
        tractorSettings = GetComponent<TractorSettings>();
        IPWatchdog = null;
        TCPThread = new Thread(Connect);
        TCPThread.Priority = System.Threading.ThreadPriority.Lowest;
        TCPThread.IsBackground = true;
        Debug.Log("TCP Starting");
        TCPThread.Start();
        Debug.Log("TCP Started");

    }

    // Update is called once per frame
    void gpsLedOff() {
        gpsLedOn = 0;
    }
    void Update()
    {
        if (networkSettings.gpsIp!= IPWatchdog) {
            IPWatchdog = networkSettings.gpsIp;
            TCPThread.Abort();
            TCPThread = new Thread(Connect);
            TCPThread.Priority = System.Threading.ThreadPriority.Lowest;
            TCPThread.IsBackground = true;
            Debug.Log("TCP Restarting");
            TCPThread.Start();
            Debug.Log("TCP Restarting");
        };
        if (gpsLedOn==1)
        {
            gpsLed.color = Color.green;
            Invoke("gpsLedOff", 0.5f);
        }
        else if(gpsLedOn == 2)
        {
            gpsLed.color = Color.red;
        }
        else
        {
            gpsLed.color = Color.gray;
        }
        if ((DateTime.Now - last).TotalSeconds>10) {
            Debug.Log("GPS is offline, reconnecting");
            gpsExceptionCounter++;
            last = DateTime.Now;

            TCPThread.Abort();
            TCPThread = new Thread(Connect);
            TCPThread.Priority = System.Threading.ThreadPriority.Lowest;
            TCPThread.IsBackground = true;
            Debug.Log("TCP Restarting");
            TCPThread.Start();
            Debug.Log("TCP Restarting");
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
            gpsLedOn = 0;
            gpsClient.DataReceived += (sender, msg) => {
                last = DateTime.Now;
                gpsLedOn = 1;
                Debug.Log("GPS NMEA: " + msg.MessageString);
                gpsExceptionCounter = 0;
                api.Send(msg.Data);
            };

        }
        catch (Exception e){
            try
            {
                gpsClient.Disconnect();
                gpsClient.Dispose();
            }
            catch { }
            gpsExceptionCounter++;
            gpsLedOn = 2;
            Debug.Log(e);
            Thread.Sleep(2000 + (1000 * gpsExceptionCounter));
            TCPThread = new Thread(Connect);
            TCPThread.Priority = System.Threading.ThreadPriority.Lowest;
            TCPThread.IsBackground = true;
            Debug.Log("TCP Restarting");
            TCPThread.Start();
            Debug.Log("TCP Restarting");

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
        TCPThread.Abort();
        gpsClient.Disconnect();
        gpsClient.Dispose();

    }
}

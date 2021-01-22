using System;
using UnityEngine;
using System.Collections;
using SimpleTCP;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
public class AgropilotIO : MonoBehaviour
{
    public NetworkSettings networkSettings;

    Thread UDPThread;
    private int port;
    private IPAddress group_address;
    IPEndPoint ip,ipGroup;
    private UdpClient client;
    private byte[] sendQueue;
    private String IPWatchdog;
    private int exceptionCounter=0;
    private bool run = true;


    [SerializeField] private Image rxLed;
    [SerializeField] private Image txLed;
    private int txLedOn=2;
    private int rxLedOn=2;
    void Start()
    {
        networkSettings = GetComponent<NetworkSettings>();
        IPWatchdog = networkSettings.moduleIp;
        sendQueue = new byte[0];
        

        UDPThread = new Thread(startUDP);
        UDPThread.Priority = System.Threading.ThreadPriority.Lowest;
        UDPThread.IsBackground = true;
        Debug.Log("UDP Starting");
        UDPThread.Start();
        Debug.Log("UDP Started");
        
    }

    void startUDP()
    {
        run = true;
        try
        {
            string[] moduleIP = networkSettings.moduleIp.Split(':');
            group_address = IPAddress.Parse(moduleIP[0]);
            port = int.Parse(moduleIP[1]);
            ip = new IPEndPoint(IPAddress.Any, port);
            ipGroup = new IPEndPoint(group_address, port);

            client = new UdpClient(ip);
            client.JoinMulticastGroup(group_address);
            client.BeginReceive(new AsyncCallback(ReceiveServerInfo), null);
        }
        catch (Exception e)
        {
            rxLedOn = 2;
            txLedOn = 1;
            RestartUDP(e);
        }

        while (run)
        {
            try
            {
                if (sendQueue.Length > 0)
                {
                    client.Send(sendQueue, sendQueue.Length, ipGroup);
                    Debug.Log("TX: " + String.Join(" ", sendQueue));
                    sendQueue = new byte[0];
                }
                Thread.Sleep(5);
            }
            catch (Exception e)
            {
                run = false;
                rxLedOn = 2;
                txLedOn = 1;
                RestartUDP(e);
            }
            
        }

        client.Close();
        client.Dispose();
    }

    void ReceiveServerInfo(IAsyncResult result)

    {
        byte[] receivedBytes = client.EndReceive(result, ref ip);
        if (receivedBytes.Length > 0)
        {
            rxLedOn = 1;
            Debug.Log("RX: " + String.Join(" ", receivedBytes));
            exceptionCounter = 0;
        }
        else
        {
            Debug.Log("No data received");
        }
        if (run) { client.BeginReceive(new AsyncCallback(ReceiveServerInfo), null); }
        else {
            client.Close();
            client.Dispose();
        } 

    }


    public void Send(byte[] packet) {
        txLedOn = 1;
        IEnumerable<byte> newBuffer = sendQueue.Concat(packet);
        sendQueue = newBuffer.ToArray();
    }

    private void RestartUDP(Exception e)
    {
        run = false;
        Debug.Log("Restarting UDP: "+ e);
        exceptionCounter++;
        Thread.Sleep(500+(500*exceptionCounter));

        try {
            startUDP();
        }
        catch (Exception e2)
        {
            Debug.Log("Restart Failed");
            exceptionCounter++;
            Thread.Sleep(2000 + (1000 * exceptionCounter));
            RestartUDP(e2);
        }
        


    }
    void LedOff()
    {
        rxLedOn = 0;
        txLedOn = 0;
    }
    void OnApplicationQuit()
    {
        run = false; //fix ghost thread running and address in use errors.
    }
    private void Update()
    {
        if (txLedOn==1)
        {
            txLed.color = Color.green;
            Invoke("LedOff", 0.2f);

        }
        else if (txLedOn == 2)
        {
            txLed.color = Color.red;
        }
        else
        {
            txLed.color = Color.gray;
        }
        if (rxLedOn==1)
        {
            rxLed.color = Color.green;
            Invoke("LedOff", 0.2f);

        }
        else if (rxLedOn == 2)
        {
            rxLed.color = Color.red;
        }
        else
        {
            rxLed.color = Color.gray;
        }
        if (networkSettings.moduleIp != IPWatchdog)
        {
            IPWatchdog = networkSettings.moduleIp;
            run = false;
            UDPThread = new Thread(startUDP);
            UDPThread.Priority = System.Threading.ThreadPriority.Lowest;
            UDPThread.IsBackground = true;
            Debug.Log("UDP Refreshing");
            UDPThread.Start();
            Debug.Log("UDP Refreshed");

        };
    }

    public AgropilotIO()
    {

    }
}

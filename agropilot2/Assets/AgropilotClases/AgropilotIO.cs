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
    private bool txLedOn=false;
    private bool rxLedOn=false;
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

            rxLedOn = true;
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
        txLedOn = true;
        IEnumerable<byte> newBuffer = sendQueue.Concat(packet);
        sendQueue = newBuffer.ToArray();
    }

    private void RestartUDP(Exception e)
    {
        run = false;
        Debug.Log("Restarting UDP: "+ e);
        exceptionCounter++;
        Thread.Sleep(500+(1000*exceptionCounter));

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
        rxLedOn = false;
        txLedOn = false;
    }
    void OnApplicationQuit()
    {
        run = false; //fix ghost thread running and address in use errors.
    }
    private void Update()
    {
        if (txLedOn)
        {
            txLed.color = Color.red;
            Invoke("LedOff", 0.2f);

        }
        else
        {
            txLed.color = Color.gray;
        }
        if (rxLedOn)
        {
            rxLed.color = Color.red;
            Invoke("LedOff", 0.2f);

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

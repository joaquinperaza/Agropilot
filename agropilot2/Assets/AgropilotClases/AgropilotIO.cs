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

public class AgropilotIO : MonoBehaviour
{
    private TractorStatus tractorStatus;
    public NetworkSettings networkSettings; //NEED IN DATAHUB FOR CONNECTING TO GPS
    private TractorSettings tractorSettings;
    private int port = 1892;
    private IPAddress group_address = IPAddress.Parse("224.5.6.7");
    IPEndPoint ip,ipGroup;
    private UdpClient client;
    private byte[] sendQueue;
    void Start()
    {
        tractorStatus = GetComponent<TractorStatus>();
        networkSettings = GetComponent<NetworkSettings>();
        tractorSettings = GetComponent<TractorSettings>();
        sendQueue = new byte[0];

        Thread thread = new Thread(startUDP);
        thread.Priority = System.Threading.ThreadPriority.Lowest;
        thread.IsBackground = true;
        Debug.Log("UDP STARTED");
        thread.Start();
        Debug.Log("UDP STARTED");


    }

    void startUDP()
    {
        ip = new IPEndPoint(IPAddress.Any, port);
        ipGroup = new IPEndPoint(group_address, port);

        client = new UdpClient(ip);
        client.JoinMulticastGroup(group_address);
        client.BeginReceive(new AsyncCallback(ReceiveServerInfo), null);

        while (true)
        {
            if (sendQueue.Length > 0) {
                client.Send(sendQueue, sendQueue.Length, ipGroup);
                Debug.Log("TX: " + String.Join(" ", sendQueue));
                sendQueue = new byte[0];
            }
            Thread.Sleep(5);
        }
    }

 

    void ReceiveServerInfo(IAsyncResult result)

    {
        byte[] receivedBytes = client.EndReceive(result, ref ip);
        if (receivedBytes.Length > 0)
        {
            Debug.Log("RX: " + String.Join(" ", receivedBytes));
        }
        else
        {
            Debug.Log("No data received");
        }
        client.BeginReceive(new AsyncCallback(ReceiveServerInfo), null);


    }



    public void Send(byte[] packet) {
        IEnumerable<byte> newBuffer = sendQueue.Concat(packet);
        sendQueue = newBuffer.ToArray();
    }
    void OnApplicationQuit()
    {
        client.Dispose();
    }

    public AgropilotIO()
    {

    }
}

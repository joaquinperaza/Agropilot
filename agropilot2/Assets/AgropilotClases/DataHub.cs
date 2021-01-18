using UnityEngine;
using System.Collections;
using SimpleTCP;
public class DataHub : MonoBehaviour
{
    private SimpleTcpServer server;
    [SerializeField] NetworkSettings networkSettings;
    [SerializeField] TractorSettings tractorSettings;
    // Use this for initialization
    void Start()
    {
        server = new SimpleTcpServer().Start(8921);
        server.Delimiter = 0x13;
        server.DelimiterDataReceived += (sender, msg) => {

            if (msg.MessageString == "N")
            {
                string json = JsonUtility.ToJson(networkSettings);
                Debug.Log(json);
                msg.ReplyLine(json);
            }
            if (msg.MessageString == "T")
            {
                string json = JsonUtility.ToJson(tractorSettings);
                msg.ReplyLine(json);
            }
            Debug.Log("Recibido: " + msg.MessageString + " connections: " + server.ConnectedClientsCount);
            msg.ReplyLine("0 200 OK");
        };

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnApplicationQuit()
    {
        server.Stop(); 
    }
}

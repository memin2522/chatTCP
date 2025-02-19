using UnityEngine;
using UnityEngine.UI;

public class ServerUI : MonoBehaviour
{
    public int serverPort = 5555;
    [SerializeField] private TCPServer _server;
    [SerializeField] private InputField messageInput;
    public void SendServerMessage()
    {
        if(!_server.isServerRunning){
            Debug.Log("The server is not running");
            return;
        }

        if(messageInput.text == ""){
            Debug.Log("The chat entry is empty");
            return;
        }

        string mensaje = messageInput.text; // Get the text from the message entry
        _server.SendData(mensaje); // Send message to the client
    }

    public void StartServer()
    {
        _server.StartServer(serverPort);
    }
}

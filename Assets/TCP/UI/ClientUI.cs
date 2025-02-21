using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClientUI : MonoBehaviour
{
    public int serverPort = 5555;
    public string serverAddress = "127.0.0.1";
    [SerializeField] private TCPClient _client;
    [SerializeField] private TMP_InputField messageInput;
    public void SendClientMessage()
    {
        if(!_client.isServerConnected)
        {
            Debug.Log("The client is not connected");
            return;
        }

        if(messageInput.text == ""){
            Debug.Log("The chat entry is empty");
            return;
        }

        string message = messageInput.text; // Get the text from the message entry
        _client.SendData(message); // Send message to the client
    }

    public void ConnectClient()
    {
        _client.ConnectToServer(serverAddress, serverPort);
    }
}

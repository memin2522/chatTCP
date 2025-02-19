using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class TCPClient : MonoBehaviour
{
    // Cliente TCP para conectarse al servidor
    private TcpClient tcpClient;
    // Flujo de datos de la red para enviar y recibir datos
    private NetworkStream networkStream;
    // Buffer para almacenar los datos recibidos del servidor
    private byte[] receiveBuffer;
    // Campo de texto para la entrada de mensajes en la UI de Unity
    public InputField chat;
    // Variable para almacenar el mensaje a enviar
    private string mensaje;

    private void Start()
    {
        // Conecta al cliente al servidor en la direccion IP y puerto especificados
        ConnectToServer("127.0.0.1", 5555);
    }

    private void ConnectToServer(string ipAddress, int port)
    {
        // Inicializa el cliente TCP
        tcpClient = new TcpClient();
        // Conecta el cliente al servidor utilizando la direccion IP y el puerto dados
        tcpClient.Connect(IPAddress.Parse(ipAddress), port);
        // Obtiene el flujo de datos de la red para la comunicacion con el servidor
        networkStream = tcpClient.GetStream();

        // Inicializa el buffer de recepcion con el tamaño del buffer del cliente
        receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
        // Comienza a leer datos del flujo de red de manera asincronica
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
    }

    private void ReceiveData(IAsyncResult result)
    {
        // Finaliza la lectura de datos del flujo de red y obtiene el numero de bytes leidos
        int bytesRead = networkStream.EndRead(result);
        // Copia los datos recibidos en un nuevo array de bytes
        byte[] receivedBytes = new byte[bytesRead];
        Array.Copy(receiveBuffer, receivedBytes, bytesRead);

        // Convierte los bytes recibidos en un mensaje de texto
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        // Muestra el mensaje recibido del servidor en la consola
        Debug.Log("Received from server: " + receivedMessage);
        // Continua leyendo datos del flujo de red de manera asincronica
        networkStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
    }

    public void EnviarMensaje()
    {
        // Obtiene el texto del campo de entrada de mensajes de la UI
        mensaje = chat.text;
        // Envia el mensaje al servidor
        SendData(mensaje);
    }

    private void SendData(string message)
    {
        // Convierte el mensaje en un array de bytes
        byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
        // Escribe los bytes en el flujo de red para enviarlos al servidor
        networkStream.Write(sendBytes, 0, sendBytes.Length);
        // Limpia el flujo de datos para asegurar que los datos se envian
        networkStream.Flush();
    }
}

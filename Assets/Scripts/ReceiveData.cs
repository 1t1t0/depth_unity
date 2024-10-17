using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ReceiveData : MonoBehaviour
{
    UdpClient udpClient;
    IPEndPoint remoteEndPoint;

    // getPointスクリプトへの参照
    public getPoint pointScript;

    void Start()
    {
        udpClient = new UdpClient(12345);  // Python側で指定したポート
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 12345);
    }

    void Update()
    {
        if (udpClient.Available > 0)
        {
            byte[] data = udpClient.Receive(ref remoteEndPoint);

            // 受信したデータを浮動小数点に変換 (x, y, z)
            float x = BitConverter.ToSingle(data, 0)/100;
            float y = BitConverter.ToSingle(data, 4)/100;
            float z = BitConverter.ToSingle(data, 8)/100;

            Debug.Log($"Received data - X: {x}, Y: {y}, Z: {z}");

            // getPointスクリプトに座標を渡す
            pointScript.UpdatePosition(x, y, z);
        }
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}

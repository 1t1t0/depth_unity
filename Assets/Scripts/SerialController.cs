using UnityEngine;
using System.IO.Ports;
using System;

public class SerialController : MonoBehaviour
{
    SerialPort serialPort = new SerialPort("COM4", 115200); // COMポートとボーレートは環境に合わせて変更
    public ReceiveData receiveData;

    private Vector3 newPosition; 

    private float pz=0,nz=0; //previous, now
    private float diffZ;

    private int pulse_width;
    public int MAX_PULSEWIDTH;
    public int MIN_PULSEWIDTH;
    public float a;
    private float interval=11f;
    public float zmove; //これ以下なら動かさない

    void Start()
    {
        serialPort.Open();
        Debug.Log("Serial Port Opened");
    }

    void Update()
    {
        newPosition=receiveData.sendPosition();

        pz=nz;
        nz=newPosition.z;
        //Debug.Log(pz);

        diffZ=nz-pz;
        //Debug.Log(diffZ);

        if(diffZ!=0){
            //pulse_width=(int)(a*interval/diffZ);
            //Debug.Log(pulse_width);
        
            if(serialPort.IsOpen){
                
                pulse_width=(int)(a*interval/diffZ);
            
                if(Math.Abs(diffZ)<=zmove || Math.Abs(pulse_width)>MAX_PULSEWIDTH || Math.Abs(pulse_width)<MIN_PULSEWIDTH) pulse_width=MAX_PULSEWIDTH;

                Debug.Log(pulse_width);

                if(Math.Abs(pulse_width)<2000&&Math.Abs(pulse_width)>10){
                    Debug.Log(pulse_width);
                    string data=diffZ.ToString(); 
                    serialPort.WriteLine(data);
                }

            /*テスト
            if(diffZ<0){
                string data=diffZ.ToString();
                //Debug.Log(data);
                serialPort.WriteLine(data);
            }else if(diffZ>0){
                string data=diffZ.ToString();
                serialPort.WriteLine(data);
            }*/
            }
        }

    }

    void OnApplicationQuit()
    {
        serialPort.Close();
        Debug.Log("Serial Port Closed");
    }
}

using UnityEngine;
using UnityEngine.UI;                       // These are the librarys being used
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Text;



public class ras_test : MonoBehaviour
{

    bool socketReady = false;                // global variables are setup here
    TcpClient mySocket;
    public NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;
    public String Host = "223.194.169.25";
    public Int32 Port = 9000;
    public bool lightStatus;

    public GameObject speed_txt;
    public GameObject distance_txt;
    String recData;

    public Thread clientReceiveThread;
    public TcpClient SendStatusThread;

    int i;
    string unity_message;

    void Start()
    {
        Debug.Log("이곳에 글자를 표시할 것입니다.");
        setupSocket();                        // setup the server connection when the program starts
        this.speed_txt = GameObject.Find("speed");
        this.distance_txt = GameObject.Find("distance");
        Debug.Log("소켓 연결 완료");


    }

    // Update is called once per frame
    void Update()
    {
            if(theStream.DataAvailable)
            {
                 readSocket();
            }

    }

    public void setupSocket()
    {                            // Socket setup here
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;
            

            
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);                // catch any exceptions
        }
    }

    public void writeSocket(String theLine)
    {            // function to write data out
        if (!socketReady)
            return;
        String tmpString = theLine.ToString();
        theWriter.Write(tmpString);
        theWriter.Flush();


    }

    public void readSocket()
    {                        // function to read data in

       int i = (this.unity_message = theReader.ReadLine().ToString()).LastIndexOf("\r\n");

        char sp = ' ';
        string[] cycle_inform = this.unity_message.Split(sp);
        if( i < 1)
        {
            this.speed_txt.GetComponent<Text>().text = "속도" + cycle_inform[0] + "km/h";
            this.distance_txt.GetComponent<Text>().text = "이동" + cycle_inform[1] + "km";
        }
            
        
    }

    public void closeSocket()
    {                            // function to close the socket
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    public void maintainConnection()
    {                    // function to maintain the connection (not sure why! but Im sure it will become a solution to a problem at somestage)
        if (!theStream.CanRead)
        {
            setupSocket();
        }
    }


} // end class ClientSocket
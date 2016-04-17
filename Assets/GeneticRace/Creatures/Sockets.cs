using UnityEngine;
using System.Collections;

public enum SocketLocation
{
    Top, Bottom, Left, Right, Front, Back
}

class Socket
{
    public SocketLocation location;
    public Transform socketTransform;
    public bool used;
}

public class Sockets : MonoBehaviour
{
    public GameObject socketPrefab;
    Socket[] sockets = new Socket[6];

	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < 6; i++)
        {
            sockets[i] = null;
        }

        CreateSocket(SocketLocation.Top);
        CreateSocket(SocketLocation.Bottom);
        CreateSocket(SocketLocation.Left);
        CreateSocket(SocketLocation.Right);
        CreateSocket(SocketLocation.Front);
        CreateSocket(SocketLocation.Back);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SetSocketObject(SocketLocation location, GameObject bodyPart)
    {
        Socket socket = null;
        foreach(Socket tmpSocket in sockets)
        {
            if(tmpSocket.location == location)
            {
                // Replace missing
                socket = tmpSocket;
                tmpSocket.used = true;
                break;
            }
        }

        bodyPart.transform.parent = socket.socketTransform;
        Vector3 localPosition = new Vector3();
        switch (location)
        {
            case SocketLocation.Top:
                {
                    localPosition.y = bodyPart.transform.localScale.y * 0.5f;
                    break;
                }
            case SocketLocation.Bottom:
                {
                    localPosition.y = bodyPart.transform.localScale.y * -0.5f;
                    break;
                }
            case SocketLocation.Left:
                {
                    localPosition.x = bodyPart.transform.localScale.x * -0.5f;
                    break;
                }
            case SocketLocation.Right:
                {
                    localPosition.x = bodyPart.transform.localScale.x * 0.5f;
                    break;
                }
            case SocketLocation.Front:
                {
                    localPosition.z = bodyPart.transform.localScale.z * 0.5f;
                    break;
                }
            case SocketLocation.Back:
                {
                    localPosition.z = bodyPart.transform.localScale.z * -0.5f;
                    break;
                }
        }

        bodyPart.transform.localPosition = localPosition;
    }

    void CreateSocket(SocketLocation location)
    {
        Socket socket = new Socket();
        socket.used = false;
        socket.location = location;

        int i = 0;
        foreach(Socket tmpSocket in sockets)
        {
            if(tmpSocket == null)
            {
                sockets[i] = socket;
                break;
            }
            i++;
        }

        if (i >= 6)
            return;

        GameObject socketGameObject = Instantiate(socketPrefab);
        socket.socketTransform = socketGameObject.transform;
        socketGameObject.transform.parent = transform;

        Vector3 currentScale = transform.lossyScale;
        Vector3 localScale = new Vector3(1 / currentScale.x, 1 / currentScale.y, 1 / currentScale.z);
        socketGameObject.transform.localScale = localScale;

        Vector3 localPosition = new Vector3(0, 0, 0);

        switch(location)
        {
            case SocketLocation.Top:
                {
                    localPosition.y = 0.5f;
                    socketGameObject.name = "TopSocket";
                    break;
                }
            case SocketLocation.Bottom:
                {
                    localPosition.y = -0.5f;
                    socketGameObject.name = "BottomSocket";
                    break;
                }
            case SocketLocation.Left:
                {
                    localPosition.x = -0.5f;
                    socketGameObject.name = "LeftSocket";
                    break;
                }
            case SocketLocation.Right:
                {
                    localPosition.x = 0.5f;
                    socketGameObject.name = "RightSocket";
                    break;
                }
            case SocketLocation.Front:
                {
                    localPosition.z = 0.5f;
                    socketGameObject.name = "FrontSocket";
                    break;
                }
            case SocketLocation.Back:
                {
                    localPosition.z = -0.5f;
                    socketGameObject.name = "BackSocket";
                    break;
                }
        }
        socketGameObject.transform.localPosition = localPosition;
    }
}

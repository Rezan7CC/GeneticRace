using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SocketLocation
{
    Top, Bottom, Left, Right, Front, Back
}

public class Socket
{
    public SocketLocation location;
    public Transform socketTransform;
    public SocketMovement socketMovement;
    public bool used;
}

public class Sockets : MonoBehaviour
{
    public GameObject socketPrefab;
    Socket[] sockets = new Socket[6];

	// Use this for initialization
	void Awake ()
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

    public List<Socket> GetUnusedSocketsInChildren()
    {
        List<Socket> unusedSockets = new List<Socket>();

        foreach(Socket socket in sockets)
        {
            if (socket == null)
                continue;
            if(socket.used == false)
            {
                unusedSockets.Add(socket);
                continue;
            }
            Sockets socketsComp = socket.socketTransform.GetComponentInChildren<Sockets>();
            if(socketsComp != null)
                unusedSockets.AddRange(socketsComp.GetUnusedSocketsInChildren());
        }
        return unusedSockets;
    }

    public void SetSocketObject(SocketLocation location, GameObject bodyPart)
    {
        Vector3 originalScale = bodyPart.transform.localScale;
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

        bodyPart.transform.parent = socket.socketMovement.transform;
        bodyPart.transform.localScale = originalScale;
        Vector3 localPosition = Vector3.zero;
        localPosition.z = bodyPart.transform.localScale.z * 0.5f;

        bodyPart.transform.localPosition = localPosition;
        bodyPart.transform.localRotation = Quaternion.identity;
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
        SocketMovement socketMovement = socketGameObject.GetComponentInChildren<SocketMovement>();
        socket.socketMovement = socketMovement;
        if(socketMovement != null)
            socketMovement.socket = socket;

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
                    Quaternion rotation = Quaternion.LookRotation(transform.up, -transform.forward);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
            case SocketLocation.Bottom:
                {
                    localPosition.y = -0.5f;
                    socketGameObject.name = "BottomSocket";
                    Quaternion rotation = Quaternion.LookRotation(-transform.up, transform.forward);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
            case SocketLocation.Left:
                {
                    localPosition.x = -0.5f;
                    socketGameObject.name = "LeftSocket";
                    Quaternion rotation = Quaternion.LookRotation(-transform.right, transform.up);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
            case SocketLocation.Right:
                {
                    localPosition.x = 0.5f;
                    socketGameObject.name = "RightSocket";
                    Quaternion rotation = Quaternion.LookRotation(transform.right, transform.up);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
            case SocketLocation.Front:
                {
                    localPosition.z = 0.5f;
                    socketGameObject.name = "FrontSocket";
                    Quaternion rotation = Quaternion.LookRotation(transform.forward, transform.up);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
            case SocketLocation.Back:
                {
                    localPosition.z = -0.5f;
                    socketGameObject.name = "BackSocket";
                    Quaternion rotation = Quaternion.LookRotation(-transform.forward, transform.up);
                    socket.socketTransform.rotation = rotation;
                    break;
                }
        }
        socketGameObject.transform.localPosition = localPosition;
    }
}

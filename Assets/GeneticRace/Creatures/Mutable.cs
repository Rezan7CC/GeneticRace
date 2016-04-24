using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mutable : MonoBehaviour
{
    public int startBodyPartCount = 5;
    public int maxBodyPartCount = 5;
    public float mutateNewPartChance = 0.5f;
    public GameObject bodyPartPrefab = null;
    public Sockets bodyBaseSockets = null;

    int currentBodyPartCount = 0;


	// Use this for initialization
	void Start ()
    {
        int i = 0;
        while (currentBodyPartCount < startBodyPartCount && i <= startBodyPartCount * 5)
        {
            MutateNewPart();
            i++;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void MutateRandomly()
    {
        if (currentBodyPartCount >= maxBodyPartCount || Random.value >= mutateNewPartChance)
            MutateExistingPart();
        else
            MutateNewPart();
    }

    void MutateNewPart()
    {
        if (bodyPartPrefab == null || bodyBaseSockets == null)
            return;

        if (currentBodyPartCount >= maxBodyPartCount)
            return;

        List<Socket> unusedSockets = bodyBaseSockets.GetUnusedSocketsInChildren();
        if (unusedSockets.Count <= 0)
            return;

        int randSocketIndex = Random.Range(0, unusedSockets.Count);
        Socket selectedSocket = unusedSockets[randSocketIndex];
        Sockets sockets = selectedSocket.socketTransform.parent.GetComponent<Sockets>();
        if (sockets == null)
            return;

        sockets.SetSocketObject(selectedSocket.location, Instantiate(bodyPartPrefab));

        currentBodyPartCount++;
    }

    void MutateExistingPart()
    {

    }
}

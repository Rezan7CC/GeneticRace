using UnityEngine;
using System.Collections;

public class RandomSize : MonoBehaviour
{
    public float minSize = 3;
    public float maxSize = 6;

	// Use this for initialization
	void Awake ()
    {
        GenerateSize();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void GenerateSize()
    {
        transform.localScale = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), Random.Range(minSize, maxSize));
    }
}

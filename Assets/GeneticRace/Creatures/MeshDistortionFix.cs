using UnityEngine;
using System.Collections;

public class MeshDistortionFix : MonoBehaviour
{
    public MeshRenderer meshRenderer;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        meshRenderer.enabled = false;
        meshRenderer.enabled = true;
    }
}

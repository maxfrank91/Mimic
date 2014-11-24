using UnityEngine;
using System.Collections;

public class Crackle : MonoBehaviour 
{
    public GameObject crackle;
    Vector3 center;
    float range = 0.5f;

	// Use this for initialization
	void Awake () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        center = transform.position;
	}
}

using UnityEngine;
using System.Collections;

public class Trail : MonoBehaviour 
{
    Vector3 lastPos, actualPos;

	// Use this for initialization
	void Awake () 
    {
        lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (lastPos != actualPos)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + (Vector3.Angle(lastPos, actualPos))));
        lastPos = actualPos;
        if (Input.GetMouseButton(0)) OnMouse_Drag();

        if (Input.GetMouseButtonDown(0))
        {
            GetComponentInChildren<TrailRenderer>().enabled = true;
            GetComponentInChildren<ParticleSystem>().emissionRate = 50;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponentInChildren<TrailRenderer>().enabled = false;
            GetComponentInChildren<ParticleSystem>().emissionRate = 0;
        }
	}

	public void newPos(Vector3 newPos )
    {

		newPos.z = +10;
		transform.position = actualPos;
	}

	void OnMouse_Drag()
    {

		//Debug.Log("drag");
		actualPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		actualPos.z += 10;
		transform.position = actualPos;
	}
}

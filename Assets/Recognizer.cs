using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recognizer : MonoBehaviour 
{
    List<Vector2> lineList;
    Vector2 tmp, vCenter;
    bool done;
    float timer;
    public GameObject sphere;

	// Use this for initialization
	void Start () 
    {
        lineList = new List<Vector2>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;
        #if UNITY_EDITOR
        tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            lineList.Clear();
            done = false;
            lineList.Add(tmp);
            timer = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineList.Add(tmp);
            done = true;
            ComputeStats();
        }
        if (Input.GetMouseButton(0) && timer > 0.2)
        {
            if (lineList[lineList.Count - 1] != tmp)
                lineList.Add(tmp);
        }
        #endif
	}

    void ComputeStats()
    {
        int angles = 0;
        float angle = 0f;
        if (lineList.Count > 1)
        {
            vCenter = Vector2.zero;
            for (int i = 0; i < lineList.Count; i++)
            {
                vCenter += lineList[i];
            }
            vCenter /= lineList.Count;    
        }
        if (lineList.Count > 3)
        {
            for (int i = 2; i < lineList.Count; i++)
            {
                angle = Vector2.Angle(lineList[i - 2] - lineList[i - 1], lineList[i - 1] - lineList[i]);
                Debug.Log(angle);
                if (angle > 70)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(lineList[i - 1].x, lineList[i - 1].y, 0);
                    cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    Debug.Log("test");
                }
            }
        }
    }
}

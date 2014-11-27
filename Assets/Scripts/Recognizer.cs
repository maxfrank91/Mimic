using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recognizer : MonoBehaviour 
{
    List<Vector2> lineList;
    Vector2 tmp, vCenter;
    bool done;
    float timer;

    static float ANGLE = 45;

	// Use this for initialization
	void Start () 
    {
        lineList = new List<Vector2>();
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        #if UNITY_EDITOR
        tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            lineList.Clear();
            done = false;
            lineList.Add(tmp);
            DeleteDebugCube();
        }
        if (Input.GetMouseButtonUp(0))
        {
            lineList.Add(tmp);
            done = true;
            ComputeStats();
        }
        if (Input.GetMouseButton(0))
        {
            if ((lineList[lineList.Count - 1] - tmp).magnitude > 0.1f)
                lineList.Add(tmp);
        }
        #endif
	}

    void DeleteDebugCube()
    {
        foreach (GameObject cube in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            if (cube.name == "Cube") GameObject.Destroy(cube);
    }
    void CreateDebugCube(int index)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(lineList[index - 1].x, lineList[index - 1].y, 0);
        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void ComputeStats()
    {
        List<Vector2> angles = new List<Vector2>();
        float angle = 0f;

        //find center of drawn symbol
        if (lineList.Count > 1)
        {
            vCenter = Vector2.zero;
            for (int i = 0; i < lineList.Count; i++)
            {
                vCenter += lineList[i];
            }
            vCenter /= lineList.Count;    
        }
        // find angles of drawn symbol
        if (lineList.Count > 3)
        {
            CreateDebugCube(1);
            angles.Add(lineList[0]);
            
            for (int i = 2; i < lineList.Count - 2; i++)
            {
                angle = Vector2.Angle(lineList[i - 2] - lineList[i - 1], lineList[i - 1] - lineList[i]);
                //Debug.Log(angle);
                if (angle > ANGLE)
                {
                    CreateDebugCube(i);
                    angles.Add(lineList[i - 1]);
                    //Debug.Log("index: " + i);
                    i += 2;
                }
            }
            //Debug.Log("Count: " + lineList.Count);

            //check the distance between the first and the last point (closed symbol)
            float distance1 = Vector2.Distance(lineList[0], lineList[lineList.Count-1]);
            float distance2 = 0;
            if (angles.Count > 1)
                distance2 = Vector2.Distance(lineList[0], angles[1]) / 2;
            else
                distance2 = Vector2.Distance(lineList[0], vCenter);
            if (distance1 < distance2)
            {
                if (angles.Count == 4)
                {
                    // SQUARE
                    if ((angles[0].y - angles[3].y < angles[0].y - angles[1].y &&
                        angles[1].y - angles[2].y < angles[0].y - angles[1].y) ||
                        (angles[0].x - angles[3].x < angles[0].x - angles[1].x &&
                        angles[1].x - angles[2].x < angles[0].x - angles[1].x))
                    {
                        InputManager.SetInput(GameManager.Symbol.SQUARE);
                        Debug.Log("Viereck");
                    }
                }
                if (angles.Count == 3)
                {
                    InputManager.SetInput(GameManager.Symbol.TRIANGLE);
                    Debug.Log("Dreieck");
                }
                if (angles.Count == 1)
                {
                    InputManager.SetInput(GameManager.Symbol.CIRCLE);
                    Debug.Log("Circle");
                }
            }
        }
    }
}

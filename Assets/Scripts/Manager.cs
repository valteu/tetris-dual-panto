using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DualPantoFramework;
using System;
using System.Linq;

public class Manage : MonoBehaviour
{
    // Start is called before the first frame update
    PantoHandle upperHandle;

    GameObject UpperForceField;
    GameObject LowerForceField;
    Vector3[] borderResetPosition = new Vector3[2];

    public GameObject cubePrefab;
    public Vector3 spawnPosition;
    GameObject fallingCube;
    List<GameObject> cubes = new List<GameObject>();

    bool[,] matrix = new bool[5, 11];

    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        

        UpperForceField = GameObject.Find("UpperForceField");
        LowerForceField = GameObject.Find("LowerForceField");
        borderResetPosition[0] = UpperForceField.transform.position;
        borderResetPosition[1] = LowerForceField.transform.position;

        spawnCube();

        InvokeRepeating("FallDown", 2.0f, 2f);
    }

    void spawnCube()
    {
        fallingCube = Instantiate(cubePrefab, spawnPosition, cubePrefab.transform.rotation);
    }
    void addCube()
    {
        var pos = getCubeMatrixPos();
        matrix[(int)pos.x,(int)pos.z] = true;
    }

    void FallDown()
    {
        Console.WriteLine("Block below" + isBlockBelow());
        if (isBlockBelow())
        {
            addCube();
            cubes.Add(fallingCube);
            spawnCube();
            resetForceFields();
        }
        fallingCube.transform.position -= new Vector3(0, 0, 1);
        BorderDown();
    }

    Vector3 getCubeMatrixPos()
    {
        var pos = fallingCube.transform.position;
        pos -= spawnPosition;
        pos.x += 2;
        pos.z = (int)Mathf.Abs(pos.z);
        return pos;
    }

    bool isBlockBelow()
    {
        var pos = getCubeMatrixPos();
        if (pos.z == 10) return true;
        return (matrix[(int)pos.x, (int)pos.z+1]);
    }
    bool isBlockLeft()
    {
        var pos = getCubeMatrixPos();

        if (pos.x <= 0) return true;

        return (matrix[(int)pos.x - 1, (int)pos.z]);
    }
    bool isBlockRight()
    {
        var pos = getCubeMatrixPos();
        Debug.Log(pos.x+":"+ pos.z);

        if (pos.x >= 4) return true;

        return (matrix[(int)pos.x + 1, (int)pos.z]);
    }

    void BorderDown()
    {
        UpperForceField.transform.position -= new Vector3(0, 0, 1f);
        LowerForceField.transform.position -= new Vector3(0, 0, 1f);
    }

    void resetForceFields()
    {
        UpperForceField.transform.position = borderResetPosition[0];
        LowerForceField.transform.position = borderResetPosition[1];
    }

    private void FixedUpdate()
    {
        var distance = fallingCube.transform.position.x - upperHandle.HandlePosition(fallingCube.transform.position).x;
        Debug.Log(fallingCube.transform.position.x+"#"+ upperHandle.HandlePosition(fallingCube.transform.position).x);
        if (Mathf.Abs(distance) > 0.5)
        {
            if (distance < 0 && !isBlockRight())
            {
                fallingCube.transform.position += new Vector3(1, 0, 0);
            }
            else if (distance > 0 && !isBlockLeft())
            {
                fallingCube.transform.position -= new Vector3(1, 0, 0);
            }
        }
    }
}
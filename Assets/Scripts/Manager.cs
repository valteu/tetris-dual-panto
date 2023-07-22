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

    PantoCollider[] pantoColliders;

    public GameObject cubePrefab;
    public Vector3 spawnPosition;
    GameObject fallingCube;
    List<GameObject> cubes = new List<GameObject>();
    private FallingStoneSoundEffect soundEffects;



    GameObject[,] matrix = new GameObject[5, 11];

    async void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        

        UpperForceField = GameObject.Find("UpperForceField");
        LowerForceField = GameObject.Find("LowerForceField");
        borderResetPosition[0] = UpperForceField.transform.position;
        borderResetPosition[1] = LowerForceField.transform.position;

        spawnCube();
        soundEffects = GetComponent<FallingStoneSoundEffect>();

        InvokeRepeating("FallDown", 2.0f, 1f);
        
    }

    void spawnCube()
    {
        fallingCube = Instantiate(cubePrefab, spawnPosition, cubePrefab.transform.rotation);
    }

    void addCube()
    {
        var pos = getCubeMatrixPos();
        matrix[(int)pos.x,(int)pos.z] = fallingCube;
        deleteFullLine();
    }

    void deleteFullLine()
    {
        bool isFull = true;
        var pos = getCubeMatrixPos();

        for (int i = 0; i < 5; i++)
        {
            if (matrix[i, (int)pos.z] == null)
                isFull = false;
        }

        if (isFull)
        {
            soundEffects.playLineCleared();
            for (int i = 0; i < 5; i++)
            {
                var cube = matrix[i, (int)pos.z];
                if (cube != null)
                {
                    cube.GetComponent<PantoCollider>()?.Disable();
                    Destroy(cube);
                }
            }

            for (int i = (int)pos.z; i > 0; i--)
            {
                for (int ii = 0; ii < 5; ii++)
                {
                    var cubeAbove = matrix[ii, i - 1];
                    if (cubeAbove != null)
                    {
                        cubeAbove.GetComponent<PantoCollider>()?.Disable();
                        GameObject newCube = Instantiate(cubePrefab, cubeAbove.transform.position - new Vector3(0, 0, 1), cubePrefab.transform.rotation);
                        
                        Destroy(cubeAbove);
                        matrix[ii, i] = newCube;
                        matrix[ii, i].GetComponent<PantoCollider>()?.CreateObstacle();
                        matrix[ii, i].GetComponent<PantoCollider>()?.Enable();
                    }
                    else
                    {
                        matrix[ii, i] = null;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                matrix[i, 0] = null;
            }
        }
    }


    void FallDown()
    {
        Console.WriteLine("Block below" + isBlockBelow());
        if (isBlockBelow())
        {
            fallingCube.GetComponent<PantoCollider>()?.CreateObstacle();
            fallingCube.GetComponent<PantoCollider>()?.Enable();
            addCube();
            cubes.Add(fallingCube);
            spawnCube();
            resetForceFields();
            soundEffects.playNewBrick();
        }
        fallingCube.transform.position -= new Vector3(0, 0, 1);
        soundEffects.playMove();
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
        return (matrix[(int)pos.x, (int)pos.z+1] != null);
    }
    bool isBlockLeft()
    {
        var pos = getCubeMatrixPos();

        if (pos.x <= 0) return true;

        return (matrix[(int)pos.x - 1, (int)pos.z] != null);
    }
    bool isBlockRight()
    {
        var pos = getCubeMatrixPos();
        Debug.Log(pos.x+":"+ pos.z);

        if (pos.x >= 4) return true;

        return (matrix[(int)pos.x + 1, (int)pos.z] != null);
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
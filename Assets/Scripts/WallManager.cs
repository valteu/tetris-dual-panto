using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using DualPantoFramework;

public class WallManager : MonoBehaviour
{
    PantoCollider[] pantoColliders;
    void Start()
    {
        activateColliders();
    }

    void activateColliders(){
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            if(collider.gameObject.tag == "Wall"){
                collider.CreateObstacle();
                collider.Enable();
            }
        }
    }

    void Update()
    {
        pantoColliders = GameObject.FindObjectsOfType<PantoCollider>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Enable();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (PantoCollider collider in pantoColliders)
            {
                collider.Disable();
            }
        }
    }

}

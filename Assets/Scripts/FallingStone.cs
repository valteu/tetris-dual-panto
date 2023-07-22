using UnityEngine;
using DualPantoFramework;

public class FallingStone : MonoBehaviour
{
    PantoHandle upperHandle;
    public bool isfalling = true;

    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    void createCollider()
    {
        /*FindObjectsOfType<PantoCollider>();
        foreach (PantoCollider collider in pantoColliders)
        {
            collider.CreateObstacle();
            collider.Enable();
            GetComponent<PantoCollider>
        }*/
    }
    void updateCollider()
    {

    }

    async void FixedUpdate()
    {
        
    }
}
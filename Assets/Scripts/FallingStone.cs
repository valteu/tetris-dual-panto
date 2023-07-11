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

    async void FixedUpdate()
    {
        var distance = transform.position.x - upperHandle.HandlePosition(transform.position).x;

        if (Mathf.Abs(distance) > 0.5)
        {
            if (distance < 0 && transform.position.x < 2)
            {
                transform.position += new Vector3(1, 0, 0);
            }
            else if (distance > 0 && transform.position.x > -2)
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
    }
}
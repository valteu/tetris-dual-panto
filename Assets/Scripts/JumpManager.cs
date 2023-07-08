using UnityEngine;
using SpeechIO;
using System.Threading;

public class JumpManager : MonoBehaviour
{
    public GameObject cubeA;
    public GameObject cubeB;
    public GameObject cubeC;
    public GameObject cubeD;
    public float jumpForce = 10f;
    SpeechIn speechIn;

    private void Start()
    {
        speechIn = new SpeechIn(onRecognized);
        speechIn.StartListening(new string[] { "Alpha", "Bravo", "Charlie", "Delta" });
    }

    private async void Listen()
    {
        string command = await speechIn.Listen(new string[] { "Alpha", "Bravo", "Charlie", "Delta" });
        switch (command)
        {
            case "Alpha":
                JumpCube(cubeA);
                break;
            case "Bravo":
                JumpCube(cubeB);
                break;
            case "Charlie":
                JumpCube(cubeC);
                break;
            case "Delta":
                JumpCube(cubeD);
                break;
        }
    }

    private void OnDestroy()
    {
        speechIn.StopListening();
    }


    void onRecognized(string message)
    {
        Debug.Log("recognized " + message);
    }

    private void Update()
    {
        Listen();
        if (Input.GetKeyDown(KeyCode.A))
        {
            JumpCube(cubeA);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            JumpCube(cubeB);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            JumpCube(cubeC);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            JumpCube(cubeD);
        }
    }

    private void JumpCube(GameObject cube)
    {
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

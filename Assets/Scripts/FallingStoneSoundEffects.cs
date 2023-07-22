using UnityEngine;
using SpeechIO;
public class FallingStoneSoundEffect : MonoBehaviour
{
    public AudioClip MoveClip;
    public AudioClip LineCleared;
    public float maxPitch = 1.2f;
    public float minPitch = 0.8f;
    private AudioSource audioSource;
    public SpeechOut speechOut;

    async void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speechOut = new SpeechOut();
        await speechOut.Speak("Tetris: Control the falling brick with the upper handle and Solve the puzzle");
        
    }
    
    public void playMove()
    {
        PlayClipPitched(MoveClip, minPitch, maxPitch);
    }

    public void playLineCleared()
    {
        PlayClipPitched(LineCleared, minPitch, maxPitch);
    }

    public void playNewBrick()
    {
        speechOut.Speak("New brick");
        speechOut.Stop();
    }

    public void StopPlayback()
    {
        audioSource.Stop();
    }

    public void PlayClipPitched(AudioClip clip, float minPitch, float maxPitch)
    {
        // little trick to make clip sound less redundant
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        // plays same clip only once, this way no overlapping
        audioSource.PlayOneShot(clip);
        audioSource.pitch = 1f;
    }

    void OnApplicationQuit()
    {
        speechOut.Stop();
    }

}

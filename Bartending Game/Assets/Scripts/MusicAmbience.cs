using UnityEngine;

public class MusicAmbience : MonoBehaviour
{
    public double musicDuration;
    public double goalTime;

    public AudioClip currentClip;
    public AudioSource audioSource;

    private void OnPlayMusic()
    {
        goalTime = AudioSettings.dspTime + 0.5;

        audioSource.clip = currentClip;
        audioSource.PlayScheduled(goalTime);

        musicDuration = (double)currentClip.samples / currentClip.frequency;
        goalTime = goalTime + musicDuration;
    }
}

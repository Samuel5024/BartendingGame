using UnityEngine;

public class MusicAmbience : MonoBehaviour
{
    public double musicDuration;
    public double goalTime;
    public AudioSource[] _audioSources;
    public int audioToggle;
    public AudioClip currentClip;
    
    private void Update()
    {
        if (AudioSettings.dspTime > goalTime - 1)
        {
            PlayScheduledClip();
        }
    }

    private void PlayScheduledClip()
    {
        _audioSources[audioToggle].clip = currentClip;
        _audioSources[audioToggle].PlayScheduled(goalTime);
        
        musicDuration = (double)currentClip.samples / currentClip.frequency;
        goalTime = goalTime + musicDuration;
        
        audioToggle = 1 - audioToggle;
    }
}

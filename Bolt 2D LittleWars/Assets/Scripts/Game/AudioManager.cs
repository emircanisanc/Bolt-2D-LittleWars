using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] private AudioClip VictoryClip;
    [SerializeField] private AudioClip DefeatClip;

    [SerializeField] private AudioSource audioSource;

    public void PlayVictory()
    {

    }
    public void PlayDefeat()
    {

    }

    public void PlayMusic()
    {

    }
    public void PauseMusic()
    {

    }
    public void UnPauseMusic()
    {

    }
}

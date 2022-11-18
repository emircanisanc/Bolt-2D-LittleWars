using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] private AudioClip VictoryClip;
    [SerializeField] private AudioClip DefeatClip;
    [SerializeField] private AudioClip BuyClip;
    [SerializeField] private AudioClip UpgradeClip;

    [SerializeField] private AudioSource audioSource;

    public void PlayVictory()
    {
        audioSource.PlayOneShot(VictoryClip);
    }
    public void PlayDefeat()
    {
        audioSource.PlayOneShot(DefeatClip);
    }
    public void PlayBuy()
    {
        audioSource.PlayOneShot(BuyClip);
    }
    public void PlayUpgrade()
    {
        audioSource.PlayOneShot(UpgradeClip);
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

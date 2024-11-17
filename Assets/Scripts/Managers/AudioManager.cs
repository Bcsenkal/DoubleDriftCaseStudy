using UnityEngine;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource extraSfxSource;
        [SerializeField] private AudioSource engineSfxSource;
        
        [Space(10)]
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioClip engineSfx;
        [SerializeField] private AudioClip collectSfx;
        [SerializeField] private AudioClip crashSfx;
        [SerializeField] private AudioClip buttonClickSfx;
        [SerializeField] private AudioClip purchaseSuccessSfx;
        [SerializeField] private AudioClip purchaseFailSfx;


#region Music

        public void PlayMusic(bool isPlaying)
        {
            if (isPlaying)
            {
                musicSource.clip = musicClip;
                musicSource.loop = true;
                musicSource.Play();
            }
            else
            {
                musicSource.Stop();
                musicSource.loop = false;
            }
        }
#endregion

#region SFX

        public void PlayEngineSfx(bool isPlaying)
        {
            if (isPlaying)
            {
                engineSfxSource.clip = engineSfx;
                engineSfxSource.loop = true;
                engineSfxSource.Play();
            }
            else
            {
                engineSfxSource.Stop();
                engineSfxSource.loop = false;
            }
        }

        public void PlayButtonClick()
        {
            sfxSource.PlayOneShot(buttonClickSfx);
        }

        public void PlayCollectSFX()
        {
            sfxSource.PlayOneShot(collectSfx);
        }

        public void PlayCrashSFX()
        {
            extraSfxSource.PlayOneShot(crashSfx);
        }

        public void PlayUnlockSfx(bool canUnlock)
        {
            if (canUnlock)
            {
                sfxSource.PlayOneShot(purchaseSuccessSfx);
            }
            else
            {
                sfxSource.PlayOneShot(purchaseFailSfx);
            }
        }

#endregion
    }
}

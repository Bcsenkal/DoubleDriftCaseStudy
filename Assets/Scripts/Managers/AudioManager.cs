using UnityEngine;

namespace Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource extraSfxSource;
        
        [Space(10)]
        [Header("Audio Clips")] 
        [SerializeField] private AudioClip musicClip;
        [SerializeField] private AudioClip collectSfx;
        [SerializeField] private AudioClip crashSfx;
        [SerializeField] private AudioClip buttonClickSfx;


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

#endregion
    }
}

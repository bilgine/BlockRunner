using UnityEngine;

namespace SoundSystem
{
    public class SoundManager : MonoBehaviour, ISound
    {
        [SerializeField] private AudioSource audioSource;
        public void PlaySound()
        {
            audioSource.volume = 1f;
            audioSource.Play();
        }

        public void ResetSound()
        {
            audioSource.pitch = 1f;
        }
    
        public void PitchSound()
        {
            audioSource.pitch += 0.05f;
        }
    }
}
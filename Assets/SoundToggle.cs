using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    private bool isMuted = false;

    public void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
    }
}

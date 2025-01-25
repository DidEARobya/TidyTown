using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager
{
    public AudioManager()
    {

    }

    public void PlaySound(AudioSource source, AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
    public void PlaySound(Vector3 position, AudioClip sound)
    {
        AudioSource.PlayClipAtPoint(sound, position);
    }
}

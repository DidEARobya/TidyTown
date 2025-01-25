using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectManager
{
    public VisualEffectManager()
    {

    }
    public void PlayVisualEffectAtLocation(VisualEffectAsset effect, Vector3 position, float lifetime)
    {
        if (effect == null)
        {
            Debug.LogError("Tried to play null effect");
            return;
        }

        GameObject effectObject = new GameObject();
        effectObject.transform.position = position;

        VisualEffect player = effectObject.AddComponent<VisualEffect>();
        player.visualEffectAsset = effect;

        player.Play();

        GameObject.Destroy(effectObject, lifetime);
    }
}

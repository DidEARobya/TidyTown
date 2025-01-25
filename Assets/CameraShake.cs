using System.Collections;
using UnityEngine;

// Ashkan Soroor (HS)
public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f; 
    public float shakeMagnitude = 0.2f; 

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        Shake();

    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            print("Shake called");

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
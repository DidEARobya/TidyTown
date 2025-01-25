using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform location1;
    [SerializeField] private Transform location2;
    [SerializeField] private Camera cameraMoved;
    [SerializeField] private float duration = 2.0f; // Duration of the camera movement
    [SerializeField] private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to move the camera smoothly from location1 to location2
        StartCoroutine(MoveCamera(location1, location2, duration));

    }

    // Coroutine to move the camera smoothly
    private IEnumerator MoveCamera(Transform start, Transform end, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            cameraMoved.transform.position = Vector3.Lerp(start.position, end.position, elapsedTime / duration);
            cameraMoved.transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera is exactly at the end position and rotation
        cameraMoved.transform.position = end.position;
        cameraMoved.transform.rotation = end.rotation;
        // Enable the camera controller after the camera has moved
        cameraController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

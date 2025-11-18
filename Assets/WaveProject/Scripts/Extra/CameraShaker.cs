using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{



    //private static CameraShaker instance;
    //public static CameraShaker Instance { get { return instance; } }

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private bool isShaking = false;

    void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
        cameraTransform = transform;
    }

    // Call this from other scripts to start the shake
    public void Shake(float duration, float magnitude)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine(duration, magnitude));
        }
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        isShaking = true;
        originalPosition = cameraTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPosition.y + Random.Range(-1f, 1f) * magnitude;
            cameraTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
        isShaking = false;
    }

}

using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 initialPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.7f;
    
    void Start()
    {
        cameraTransform = transform;
        initialPosition = cameraTransform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        
        cameraTransform.DOShakePosition(duration, magnitude).OnComplete(()=>
        {
            transform.position = initialPosition;
        });
    }
}
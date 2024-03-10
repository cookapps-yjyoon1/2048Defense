using System;
using DG.Tweening;
using UnityEngine;

public class CameraManager : SingletonBehaviour<CameraManager>
{
    [SerializeField] private CameraShake cameraShake;
    
    public void Shake(float duration, float magnitude)
    {
        cameraShake.Shake(duration,magnitude);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private Camera mainCamera;
    void Awake() 
    {
        mainCamera = Camera.main;
    }

    //1.相机size放大/缩小
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(14.5f, 0.5f);
    }
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(20, 0.5f);
        mainCamera.transform.DOMove(new Vector3(4.5f,9,-10), 1);
    }
}

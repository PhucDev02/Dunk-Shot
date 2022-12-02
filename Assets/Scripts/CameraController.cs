using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public static CameraController Instance;

    [SerializeField] Transform firstHoop;
    [SerializeField] CinemachineVirtualCamera vCamera;
    [SerializeField] Transform ball;
    CinemachineComponentBase componentBase;
    public float screenWidth, screenHeight;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnSecondChange, (param) => Enable());
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;
        setCamera();
    }
    private void setCamera()
    {
        componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_ScreenX = (HoopsPooler.Instance.GetLastHoop().position.x + screenWidth / 2) / screenWidth;
            (componentBase as CinemachineFramingTransposer).m_ScreenY = ((screenHeight / 2 - firstHoop.position.y) / screenHeight);
            (componentBase as CinemachineFramingTransposer).m_BiasX = (componentBase as CinemachineFramingTransposer).m_ScreenX;
        }
    }
    private void Update()
    {
        if (GameController.Instance.IsGameOver == false)
            if (ball.position.y < HoopsPooler.Instance.GetLowestPositionHoop() - 1.5f)
            {
                vCamera.enabled = false;
                this.PostEvent(EventID.OnGameOver);
            }
    }
    public void Enable()
    {
        vCamera.enabled = true;
        setCamera();
    }
}

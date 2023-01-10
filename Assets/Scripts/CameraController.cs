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
    [SerializeField] GameObject vCamPrefab;
    [SerializeField] Transform ball;
    [SerializeField] SpriteRenderer bound;
    CinemachineComponentBase componentBase;
    public float screenWidth, screenHeight;
    [SerializeField] private Vector3 initCameraPosition;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnSecondChange, (param) => Enable());
        this.RegisterListener(EventID.OnContactHoop, (param) => Follow());
        Camera.main.orthographicSize = vCamera.m_Lens.OrthographicSize = bound.bounds.size.x * Screen.height / Screen.width * 0.5f;
        screenHeight = Camera.main.orthographicSize * 2;
        screenWidth = screenHeight * Camera.main.aspect;


        setCamera();
      // DragPanel.maxMagnitude = Camera.main.orthographicSize / 2;
      //  DragPanel.minMagnitude = DragPanel.maxMagnitude / 2.5f;
    }
    private void Start()
    {
        initCameraPosition = vCamera.transform.position;
    }
    private void setCamera()
    {
        componentBase = vCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_ScreenX = (GameManager.initPositionCamera.x + screenWidth / 2) / screenWidth;
            (componentBase as CinemachineFramingTransposer).m_ScreenY = ((screenHeight / 2 - GameManager.initPositionCamera.y) / screenHeight);
            //(componentBase as CinemachineFramingTransposer).m_BiasX = (componentBase as CinemachineFramingTransposer).m_ScreenX;
        }
    }
    private void Update()
    {
        if (GameController.Instance.IsGameOver == false)
            if (ball.position.y < HoopsPooler.Instance.GetLowestPositionHoop() - 1f)
            {
                vCamera.m_Follow = null;
            }
    }
    public void Enable()
    {
        //vCamera.enabled = true;
        vCamera.m_Follow = ball;
        setCamera();
    }
    public void Follow()
    {
        vCamera.m_Follow = ball;
    }
    public void NewGame()
    {
        if (vCamera != null)
            Destroy(vCamera.gameObject);
        GameObject tmp = Instantiate(vCamPrefab);
        vCamera = tmp.GetComponent<CinemachineVirtualCamera>();
        vCamera.transform.position = initCameraPosition;
        vCamera.m_Follow = firstHoop;
        //setCamera();
        Camera.main.orthographicSize = vCamera.m_Lens.OrthographicSize = bound.bounds.size.x * Screen.height / Screen.width * 0.5f;

    }
}
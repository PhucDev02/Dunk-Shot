using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class Projection : MonoBehaviour
{
    public static Projection Instance;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyTheme());
    }


    private Scene simulatorScene;
    private PhysicsScene2D physicScene;

    [SerializeField] private Transform obstacles;
    private List<GameObject> simulationObstacles;
    private BallController ghostObj = null;

    [SerializeField] private int maxTrajectoryPoint;
    [SerializeField] GameObject pointPrefab;
    [SerializeField] private GameObject[] points;
    private SpriteRenderer[] pointsRenderer;

    private bool isVisible;
    private float alpha;
    private Color color;
    private void ApplyTheme()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (PlayerPrefs.GetInt("Darkmode") == 0)
                pointsRenderer[i].color = GameManager.Instance.GetTheme().trajectoryColor;
            else
                pointsRenderer[i].color = GameManager.Instance.GetTheme().trajectoryDarkColor;
        }
        color = pointsRenderer[0].color;
    }
    void Start()
    {
        CreatePhysicScene();
        isVisible = false;
    }
    void CreatePhysicScene()
    {
        simulatorScene = SceneManager.CreateScene("Simulator", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicScene = simulatorScene.GetPhysicsScene2D();

        simulationObstacles = new List<GameObject>();
        foreach (Transform obj in obstacles)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.tag = "Untagged";
            SceneManager.MoveGameObjectToScene(ghostObj, simulatorScene);
            simulationObstacles.Add(ghostObj);

        }
        points = new GameObject[maxTrajectoryPoint];
        pointsRenderer = new SpriteRenderer[maxTrajectoryPoint];
        points[0] = Instantiate(pointPrefab, transform);
        pointsRenderer[0] = points[0].GetComponent<SpriteRenderer>();
        for (int i = 1; i < maxTrajectoryPoint; i++)
        {
            points[i] = Instantiate(pointPrefab, transform);
            points[i].transform.localScale = points[i - 1].transform.lossyScale * 0.98f;
            pointsRenderer[i] = points[i].GetComponent<SpriteRenderer>();
        }
        ghostObj = Instantiate(GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>());
        ghostObj.GetComponent<SpriteRenderer>().enabled = false;
        ghostObj.GetComponent<BallTrail>().enabled = false;
        ghostObj.transform.GetChild(0).gameObject.SetActive(false);
        ghostObj.transform.GetChild(1).gameObject.SetActive(false);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulatorScene);
        
        ApplyTheme();
    }


    public void SimulateTrajectory(BallController ball, Vector2 pos)
    {
        //update obstacles
        for (int i = 0; i < obstacles.childCount; i++)
        {
            simulationObstacles[i].transform.position = obstacles.GetChild(i).transform.position;
        }

        ghostObj.transform.position = pos;
        ghostObj.gameObject.transform.localScale = ball.transform.lossyScale;
        ghostObj.ContactHoop();
        setAlphaPoint();
        ghostObj.Shoot();
        for (int i = 0; i < maxTrajectoryPoint * 3; i++)
        {
            physicScene.Simulate(Time.fixedDeltaTime);
            if (i % 3 == 0)
            {
                points[i / 3].transform.position = ghostObj.transform.position;
            }
        }
        //Destroy(ghostObj.gameObject);
    }
    private void setAlphaPoint()
    {
        alpha = (DragPanel.force.magnitude - DragPanel.minMagnitude) / 0.1f;
        color.a = alpha;
        for (int i = 0; i < maxTrajectoryPoint; i++)
        {
            pointsRenderer[i].color = color;
        }
    }
    public void TurnOffTrajectory()
    {
        if (isVisible == true)
        {
            isVisible = false;
            foreach (GameObject obj in points)
                obj.SetActive(false);
        }
    }
    public void TurnOnTrajectory()
    {
        if (isVisible == false)
        {
            isVisible = true;
            foreach (GameObject obj in points)
                obj.SetActive(true);
        }
    }
    public void ToggleCheatMode()
    {
        maxTrajectoryPoint = 30;
    }
}

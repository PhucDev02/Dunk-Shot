using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Projection : MonoBehaviour
{
    public static Projection Instance;
    private void Awake()
    {
        Instance = this;
    }


    private Scene simulatorScene;
    private PhysicsScene2D physicScene;

    [SerializeField] private Transform obstacles;


    [SerializeField] private int maxTrajectoryPoint;
    [SerializeField] GameObject pointPrefab;
    [SerializeField] private GameObject[] points;
    private SpriteRenderer[] pointsRenderer;

    private bool isVisible;
    private float alpha;
    private Color color;
    private float highestVerticlePoint;
    void Start()
    {
        CreatePhysicScene();
        isVisible = false;
    }
    void CreatePhysicScene()
    {
        simulatorScene = SceneManager.CreateScene("Simulator", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicScene = simulatorScene.GetPhysicsScene2D();
        foreach (Transform obj in obstacles)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            SceneManager.MoveGameObjectToScene(ghostObj, simulatorScene);
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
        color = pointsRenderer[0].color;
    }


    public void SimulateTrajectory(BallController ball, Vector2 pos)
    {
        var ghostObj = Instantiate(ball, pos, Quaternion.identity);
        ghostObj.gameObject.transform.localScale = ball.transform.lossyScale;
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulatorScene);
        setAlphaPoint();
        ghostObj.Shoot();
        for (int i = 0; i < maxTrajectoryPoint * 3; i++)
        {
            physicScene.Simulate(Time.fixedDeltaTime);
            if (i % 3 == 0)
            {
                points[i / 3].transform.position = ghostObj.transform.position;
                highestVerticlePoint = Mathf.Max(highestVerticlePoint, ghostObj.transform.position.y);
            }
        }
        Destroy(ghostObj.gameObject);
    }
    private void setAlphaPoint()
    {
        alpha = (DragPanel.force.magnitude - 120) / 100 ;
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
    public float GetHighestVerticlePoint()
    {
        return highestVerticlePoint;
    }
}

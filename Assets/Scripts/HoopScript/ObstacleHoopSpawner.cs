using DG.Tweening;
using UnityEngine;

public class ObstacleHoopSpawner : MonoBehaviour
{
    public static ObstacleHoopSpawner Instance { get; private set; }

    private HoopController hoop;
    private bool atRight;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn(HoopController hoop)
    {
        this.hoop = hoop;
        atRight = hoop.transform.position.x > 0f;

        int id = Random.Range(1, 16);

        switch (id)
        {
            case 1:
                SpawnBesideBar();
                break;
            case 2:
                SpawnHorizontalTopBar();
                break;
            case 8:
                SpawnVerticalTopBar();
                break;
            case 3:
                SpawnHorizontalBar();
                break;
            case 4:
                SpawnRotateBar();
                break;
            case 5:
                SpawnShield();
                break;
            case 6:
                SpawnTopBouncer();
                break;
            case 7:
                SpawnSingleBesideBouncer();
                break;
            default:
                break;
        }
    }

    private void SpawnBesideBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_3).GetComponent<Obstacle>();

        float x = 1f;
        float y = 0.5f;
        float dir = atRight ? 1 : -1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, y);
        obstacle.Appear();

        hoop.hoopObstacles.Add(obstacle);
        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnHorizontalTopBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_2).GetComponent<Obstacle>();

        float y = 1.5f;
        obstacle.transform.position = hoop.transform.position + new Vector3(0, y);
        obstacle.transform.DORotate(Vector3.forward * 90, 0);
        obstacle.Appear();
        hoop.hoopObstacles.Add(obstacle);

        hoop.transform.rotation = Quaternion.identity;
    }
    private void SpawnVerticalTopBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_2).GetComponent<Obstacle>();

        float y = 1.5f;
        obstacle.transform.position = hoop.transform.position + new Vector3(0, y);
        obstacle.Appear();
        hoop.hoopObstacles.Add(obstacle);

        hoop.transform.rotation = Quaternion.identity;
    }
    private void SpawnHorizontalBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_3).GetComponent<Obstacle>();

        float x = 1;
        float dir = atRight ? -1 : 1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        hoop.hoopObstacles.Add(obstacle);
        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnRotateBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_2).GetComponent<Obstacle>();

        float x = Random.Range(1.5f, 2.0f);
        float y = -0.5f;
        float duration = Random.Range(2.6f, 3f);
        float dir = atRight ? -1 : 1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, y);
        obstacle.transform.DORotate(Vector3.forward * 360, duration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        obstacle.Appear();

        hoop.hoopObstacles.Add(obstacle);
        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnShield()
    {
        // random shield
        string obstacleTag = "Shield" + Random.Range(1, 5);
        Debug.Log("SPAWN " + obstacleTag);
        Obstacle obstacle = ObjectPool.Instance.Spawn(obstacleTag).GetComponent<Obstacle>();

        float duration = Random.Range(0.2f, 0.4f);

        obstacle.transform.position = hoop.transform.position;
        obstacle.transform.DORotate(Vector3.forward * 360, duration, RotateMode.Fast).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        //obstacle.transform.DORotate(Vector3.forward * 360f, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        hoop.hoopObstacles.Add(obstacle); 
        obstacle.Appear();

    }

    private void SpawnTopBouncer()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BOUNCER).GetComponent<Obstacle>();

        float y = 1.5f;

        hoop.hoopObstacles.Add(obstacle);
        obstacle.transform.position = hoop.transform.position + new Vector3(0, y);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnSingleBesideBouncer()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BOUNCER).GetComponent<Obstacle>();

        float x = 1.3f;
        float dir = atRight ? -1 : 1;

        hoop.hoopObstacles.Add(obstacle);
        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }
}
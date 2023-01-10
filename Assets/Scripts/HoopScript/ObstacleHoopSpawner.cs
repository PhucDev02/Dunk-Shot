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

        int id = Random.Range(1, 10);

        switch (id)
        {
            case 1:
                SpawnBesideBar();
                break;
            case 2:
                SpawnTopBar();
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
                SpawnTopBackboard();
                break;
            case 7:
                SpawnSingleBesideBackboard();
                break;
            default:
                break;
        }
    }

    private void SpawnBesideBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_4).GetComponent<Obstacle>();

        float x = Random.Range(1.35f, 1.55f);
        float y = Random.Range(0.6f, 0.85f);
        float dir = atRight ? 1 : -1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, y);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnTopBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_3).GetComponent<Obstacle>();

        float y = Random.Range(2.4f, 3f);

        obstacle.transform.position = hoop.transform.position + new Vector3(0, y);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnHorizontalBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_1).GetComponent<Obstacle>();

        float x = Random.Range(3f, 4f);
        float dir = atRight ? -1 : 1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnRotateBar()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BAR_2).GetComponent<Obstacle>();

        float x = Random.Range(3.2f, 3.5f);
        float y = Random.Range(-0.5f, -0.2f);
        float duration = Random.Range(2.6f, 3f);
        float dir = atRight ? -1 : 1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, y);
        obstacle.transform.DORotate(Vector3.forward * 360, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnShield()
    {
        // random shield
        string obstacleTag = "Shield" + Random.Range(1, 5);
        Debug.Log("SPAWN " + obstacleTag);
        Obstacle obstacle = ObjectPool.Instance.Spawn(obstacleTag).GetComponent<Obstacle>();

        float duration = Random.Range(2.8f, 4f);

        obstacle.transform.position = hoop.transform.position;
        obstacle.transform.DORotate(Vector3.forward * 360f, duration, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        obstacle.Appear();

    }

    private void SpawnTopBackboard()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BACK_BOARD).GetComponent<Obstacle>();

        float y = Random.Range(2.6f, 3f);

        obstacle.transform.position = hoop.transform.position + new Vector3(0, y);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }

    private void SpawnSingleBesideBackboard()
    {
        Obstacle obstacle = ObjectPool.Instance.Spawn(PoolTag.BACK_BOARD).GetComponent<Obstacle>();

        float x = Random.Range(1.85f, 2.1f);
        float dir = atRight ? -1 : 1;

        obstacle.transform.position = hoop.transform.position + new Vector3(dir * x, 0f);
        obstacle.Appear();

        hoop.transform.rotation = Quaternion.identity;
    }
}
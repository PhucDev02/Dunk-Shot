using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopObstacles : MonoBehaviour
{
    private List<Obstacle> obstacles;

    private void Awake()
    {
        obstacles = new List<Obstacle>();
    }

    public void Renew()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.Disappear();
            ObjectPool.Instance.Recall(obstacle.gameObject);
        }

        obstacles.Clear();
    }

    public void Add(Obstacle obstacle)
    {
        obstacles.Add(obstacle);
    }

    public void Release()
    {
        foreach (var obstacle in obstacles)
        {
            obstacle.Disappear();
        }

        obstacles.Clear();
    }
}

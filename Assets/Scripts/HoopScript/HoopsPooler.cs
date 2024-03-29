using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsPooler : MonoBehaviour
{
    public static HoopsPooler Instance;
    // Start is called before the first frame update
    [SerializeField] List<GameObject> hoops;
    [SerializeField] public int idLastHoop, idLowestHoop;
    [SerializeField] private bool isValidShot;
    private int[] rotation = { 0, 0, 0, 15, 30, 45 };
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnSecondChange, (param) => OnSecondChange());
    }
    void Start()
    {
        Reset();
        hoops = new List<GameObject>();
        LoadHoop();
    }
    public void Reset()
    {
        idLastHoop = 0;
        idLowestHoop = 0;
        isValidShot = false;
    }
    public void LoadHoop()
    {
        Reset();
        GameObject tmp = null;
        ObjectPool.Instance.RecallAll();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        hoops.Clear();
        if (!GameController.Instance.challengeMode)
        {
            tmp = Resources.Load("Prefabs/Levels/EndlessMode") as GameObject;
        }
        else
            tmp = Resources.Load(ChallengeManager.Instance.path) as GameObject;
        int boundIndex = tmp.transform.childCount - 2;
        for (int i = 0; i < boundIndex; i++) // -2 to except victoryhoop,-1 is obstacle
        {
            hoops.Add(Instantiate(tmp.transform.GetChild(i).gameObject, transform));
            hoops[i].GetComponent<HoopController>().id = i;
        }

        hoops.Add(Instantiate(tmp.transform.GetChild(boundIndex).gameObject, transform));
        if (!GameController.Instance.challengeMode)
            hoops[boundIndex].GetComponent<HoopController>().id = boundIndex;
        else
        {
            hoops[boundIndex].GetComponent<VictoryHoop>().id = boundIndex;
            hoops[0].GetComponent<HoopController>().SetFirstHoopInChallenge();
        }
        //spawn Obstacle or token
        for(int i=0;i<tmp.transform.GetChild(boundIndex+1).childCount;i++)
        {
            Instantiate(tmp.transform.GetChild(boundIndex + 1).GetChild(i).gameObject,transform);
        }

        GameController.Instance.RespawnBall();
    }

    public void SetIdLastHoop(int id)
    {
        if (id != idLastHoop)
        {
            idLastHoop = id;
            isValidShot = true;
            disableLowerHoops();
            if (!GameController.Instance.challengeMode)
                SpawnNewHoop();
        }
        else isValidShot = false;
    }
    public bool IsValidShot()
    {
        return isValidShot;
    }
    public float GetLowestPositionHoop()
    {
        return hoops[idLowestHoop].transform.position.y;
    }
    private void SpawnNewHoop()
    {
        Logger.Log("spawn");
        for (int i = 0; i < hoops.Count; i++)
        {
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].transform.position = randomNewPosition();
                hoops[i].SetActive(true);
                hoops[i].transform.eulerAngles = randomNewRotate();
                ObstacleHoopSpawner.Instance.Spawn(hoops[i].GetComponent<HoopController>());
                return;
            }
        }
    }
    private void disableLowerHoops()
    {
        for (int i = 0; i < hoops.Count; i++)
        {
            if (hoops[i].activeInHierarchy)
            {
                if (hoops[i].transform.position.y < hoops[idLastHoop].transform.position.y)
                    if (!GameController.Instance.challengeMode)
                        hoops[i].GetComponent<HoopController>().Disappear();
                    else
                        hoops[i].GetComponent<HoopController>().EffectContact();
                if (hoops[i].transform.position.y < hoops[idLowestHoop].transform.position.y)
                    idLowestHoop = i;
            }
        }
    }
    private Vector2 randomNewPosition()
    {
        if (hoops[idLastHoop].transform.position.x > 0)
            return new Vector2(Random.Range(-(CameraController.Instance.screenWidth / 2 - 1f), -1.0f), hoops[idLastHoop].transform.position.y + Random.Range(2.0f, 3f));
        else
            return new Vector2(Random.Range(1.0f, CameraController.Instance.screenWidth / 2 - 1f), hoops[idLastHoop].transform.position.y + Random.Range(2.0f, 3f));

    }
    private Vector3 randomNewRotate()
    {
        if (hoops[idLastHoop].transform.position.x > 0)
            return -Vector3.forward * rotation[Random.Range(0, 6)];
        else
            return Vector3.forward * rotation[Random.Range(0, 6)];


    }
    public Transform GetLastHoop()
    {
        return hoops[idLastHoop].transform;
    }
    public void OnSecondChange()
    {
        hoops[idLastHoop].GetComponent<HoopController>().ResetRotation();
    }
    private GameObject getHoop()
    {
        for (int i = 0; i < hoops.Count; i++)
        {
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].SetActive(true);
                return hoops[i];
            }
        }
        return null;
    }
    public void NewEndlessGame()
    {
        idLastHoop = 0;
        idLowestHoop = 0;
        isValidShot = false;
        //for (int i = 0; i < transform.childCount; i++)
        //    if (hoops[i].activeInHierarchy)
        //    {
        //        if (hoops[i].GetComponent<MonoBehaviour>() is HoopController)
        //            hoops[i].GetComponent<HoopController>().Reset();
        //        hoops[i].SetActive(false);
        //    }
        LoadHoop();
        int dem = 0;
        for (int i = 0; i < hoops.Count; i++)
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].SetActive(true);
                hoops[i].GetComponent<HoopController>().Reset();
                if (dem == 0)
                {
                    idLastHoop = hoops[i].GetComponent<HoopController>().id;
                    hoops[i].transform.position = GameManager.initPosFirstHoop;
                    hoops[i].GetComponent<HoopController>().SetFirstHoop();
                }
                else
                    hoops[i].transform.position = GameManager.initPosSecondHoop;
                dem++;
                if (dem == 2) return;
            }
    }
    public void NewChallengeGame()
    {
        LoadHoop();
    }
}

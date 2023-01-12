using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsPooler : MonoBehaviour
{
    public static HoopsPooler Instance;
    // Start is called before the first frame update
    [SerializeField] List<GameObject> hoops;
    [SerializeField] private int idLastHoop, idLowestHoop;
    [SerializeField] private bool isValidShot;
<<<<<<< HEAD
    [SerializeField] Transform endlessMode, challengeMode;
    [SerializeField] GameObject challengeTest,endlessPrefab;

    private int[] rotation = { 0, 0, 0, 15, 30, 45 };
=======
    private int[] rotation = { 0,0,0, 15, 30, 45 };
>>>>>>> parent of 14d93d4 (backup for stupid challenge)
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnSecondChange, (param) => OnSecondChange());
    }
    void Start()
    {
        idLastHoop = 0;
        idLowestHoop = 0;
        isValidShot = false;
        hoops = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
<<<<<<< HEAD
            //release bong
            GameController.Instance.RespawnBall();
            if (endlessMode != null) Destroy(endlessMode.gameObject);
            challengeMode = Instantiate(challengeTest, transform).transform;
            for (int i = 0; i < challengeMode.childCount - 1; i++)
            {
                hoops.Add(challengeMode.GetChild(i).gameObject);
                hoops[i].GetComponent<HoopController>().id = i;
                //hoops[i].SetActive(true);
            }
        }
        else
        {
            endlessMode = Instantiate(endlessPrefab, transform).transform;
            if (challengeMode != null) Destroy(challengeMode.gameObject);
            for (int i = 0; i < endlessMode.childCount; i++)
            {
                hoops.Add(endlessMode.GetChild(i).gameObject);
                hoops[i].GetComponent<HoopController>().id = i;
            }
=======
            hoops.Add(transform.GetChild(i).gameObject);
            hoops[i].GetComponent<HoopController>().id = i;
>>>>>>> parent of 14d93d4 (backup for stupid challenge)
        }
        this.RegisterListener(EventID.OnContactHoop, (param) => disableLowerHoops());
    }
    private void disableLowerHoops()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (hoops[i].activeInHierarchy)
            {
                if (hoops[i].transform.position.y < hoops[idLastHoop].transform.position.y)
                    hoops[i].GetComponent<HoopController>().Disappear();
                if (hoops[i].transform.position.y < hoops[idLowestHoop].transform.position.y)
                    idLowestHoop = i;
            }
        }
    }
    public void SetIdLastHoop(int id)
    {
        if (id != idLastHoop)
        {
            idLastHoop = id;
            isValidShot = true;
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
        for (int i = 0; i < transform.childCount; i++)
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].SetActive(true);
                return hoops[i];
            }
        }
        return null;
    }
    public void NewGame()
    {
        idLastHoop = 0;
        idLowestHoop = 0;
        isValidShot = false;
        for (int i = 0; i < transform.childCount; i++)
            if (hoops[i].activeInHierarchy)
            {
                hoops[i].GetComponent<HoopController>().reset();
                hoops[i].SetActive(false);
            }
        int dem = 0;
        for (int i = 0; i < transform.childCount; i++)
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].SetActive(true);
                hoops[i].GetComponent<HoopController>().reset();
                if (dem == 0)
                {
                    idLastHoop = hoops[i].GetComponent<HoopController>().id;
                    hoops[i].transform.position = GameManager.initPosFirstHoop;
                }
                else
                    hoops[i].transform.position = GameManager.initPosSecondHoop;
                dem++;
                if (dem == 2) return;
            }
    }
}

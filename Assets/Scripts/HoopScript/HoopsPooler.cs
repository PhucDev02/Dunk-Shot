using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsPooler : MonoBehaviour
{
    public static HoopsPooler Instance;
    // Start is called before the first frame update
    [SerializeField] List<GameObject> hoops;
    private int idLastHoop, idLowestHoop;
    private bool isValidShot;
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
            hoops.Add(transform.GetChild(i).gameObject);
            hoops[i].GetComponent<HoopController>().id = i;
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
                hoops[i].transform.rotation = Quaternion.identity;
                hoops[i].SetActive(true);
                return;
            }
        }
    }
    private Vector2 randomNewPosition()
    {
        if (hoops[idLastHoop].transform.position.x > 0)
            return new Vector2(Random.Range(-(CameraController.Instance.screenWidth / 2 - 0.8f), 0), hoops[idLastHoop].transform.position.y + Random.Range(2.0f, 3f));
        else
            return new Vector2(Random.Range(0, CameraController.Instance.screenWidth / 2 - 0.8f), hoops[idLastHoop].transform.position.y + Random.Range(2.0f, 3f));

    }
    public Transform GetLastHoop()
    {
        return hoops[idLastHoop].transform;
    }
    public void OnSecondChange()
    {
        hoops[idLastHoop].GetComponent<HoopController>().ResetRotation();
    }
}

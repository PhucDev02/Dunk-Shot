using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopsPooler : MonoBehaviour
{
    public static HoopsPooler Instance;
    // Start is called before the first frame update
    [SerializeField] List<GameObject> hoops;
    private int idLastHoop;
    private bool isValidShot;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        idLastHoop = 0;
        isValidShot = false;
        hoops = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            hoops.Add(transform.GetChild(i).gameObject);
            hoops[i].GetComponent<HoopController>().id = i;
            hoops[i].GetComponent<HoopController>().id = i;
        }
        this.RegisterListener(EventID.OnContactHoop, (param) => disableLowerHoops());
    }
    private void disableLowerHoops()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (hoops[i].activeInHierarchy)
                if (hoops[i].transform.position.y < hoops[idLastHoop].transform.position.y)
                    hoops[i].GetComponent<HoopController>().Disappear();
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
        return isValidShot ;
    }
    private void SpawnNewHoop()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!hoops[i].activeInHierarchy)
            {
                hoops[i].transform.position = new Vector2(Random.Range(-2.5f, 2.5f), hoops[idLastHoop].transform.position.y + 2.0f);
                hoops[i].SetActive(true);
                return;
            }
        }
    }
}

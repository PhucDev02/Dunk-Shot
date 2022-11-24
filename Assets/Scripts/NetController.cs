using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] HoopController hoopController;
    [SerializeField] Transform anchor, bottom;
    [SerializeField] EdgeCollider2D sensor;
    private void Start()
    {
        this.RegisterListener(EventID.OnShoot, (param) => { StartCoroutine(WaitToEnableSensor()); });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         hoopController.ContactBall();
        Logger.Log("contact");
        sensor.enabled = false;
    }
    IEnumerator WaitToEnableSensor()
    {
        yield return new WaitForSeconds(0.1f);
        sensor.enabled = true;
    }
}

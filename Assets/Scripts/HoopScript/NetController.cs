using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NetController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] HoopController hoopController;
    [SerializeField] Transform anchor, bottom;
    [SerializeField] EdgeCollider2D sensor;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hoopController.ContactBall();
        sensor.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        OnCollisionWithBall();
    }
    public void EnableSensor()
    {
        StartCoroutine(WaitToEnableSensor());
    }
    IEnumerator WaitToEnableSensor()
    {
        yield return new WaitForSeconds(0.1f);
        sensor.enabled = true;
    }
    public void OnLaunchFailed()
    {
        transform.DOScaleY(1.0f, 0.5f).SetEase(Ease.OutElastic);
    }
    public void OnCollisionWithBall()
    {
        transform.DOScaleY(0.8f, 0.05f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOScaleY(1.0f, 0.1f).SetEase(Ease.InQuad);
        });
    }
    public void OnContactHoop()
    {
        transform.DOScaleY(1.2f, 0.05f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOScaleY(1.0f, 0.1f).SetEase(Ease.InQuad);
        });
    }

    public void OnLaunch()
    {
        transform.DOScaleY(1.0f, 0.5f).SetEase(Ease.OutElastic).SetDelay(0.02f);
    }

}

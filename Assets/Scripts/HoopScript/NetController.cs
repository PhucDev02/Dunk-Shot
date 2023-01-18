using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NetController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] HoopController hoopController;
    [SerializeField] public EdgeCollider2D sensor;
    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hoopController.ContactBall();
        AudioManager.Instance.Play("CollideVsNet");
        sensor.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 1)
        {
            OnCollisionWithBall();
        }
    }
    public void EnableSensor()
    {
        this.PostEvent(EventID.OnLaunchBall);
        StartCoroutine(WaitToEnableSensor());
    }
    IEnumerator WaitToEnableSensor()
    {
        yield return new WaitForSeconds(0.1f);
        sensor.enabled = true;
    }
    public void OnLaunchFailed()
    {
        transform.DOScaleY(1.0f, 0.5f).SetUpdate(true).SetEase(Ease.OutElastic).SetDelay(0.05f);
    }
    public void OnLaunch()
    {
        transform.DOScaleY(1.0f, 0.6f).SetUpdate(true).SetEase(Ease.OutElastic).SetDelay(Time.fixedDeltaTime + 0.01f);
    }
    public void OnCollisionWithBall()
    {
        transform.DOScaleY(0.8f, 0.05f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() =>
        {
            transform.DOScaleY(1.0f, 0.1f).SetUpdate(true).SetEase(Ease.OutBack);
        });
    }
    public void OnContactHoop()
    {
        transform.DOScaleY(1.2f, 0.05f).SetUpdate(true).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOScaleY(1.0f, 0.1f).SetUpdate(true).SetEase(Ease.InQuad);
        });
    }


}

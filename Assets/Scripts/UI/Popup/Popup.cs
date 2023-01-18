using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Popup : MonoBehaviour
{
    private void Start()
    {
        preview.transform.DOScale(0.94f, 0.45f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }
    public virtual void ShowPopup()
    {
        AudioManager.Instance.Play("BallPopup");
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
    }
    public void HidePopup()
    {
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
    [SerializeField] protected Image preview;
    [SerializeField] protected TextMeshProUGUI description;
    public virtual void AssignPopup(Ball ball)
    {
        description.text = ball.description;
    }
}

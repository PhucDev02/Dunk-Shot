using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Popup : MonoBehaviour
{
    public void ShowPopup()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo).SetUpdate(true);
    }
    public void HidePopup()
    {
        gameObject.SetActive(false);
    }
    [SerializeField] protected Image preview;
    [SerializeField] protected TextMeshProUGUI description;
    public virtual void AssignPopup(Ball ball)
    {
        description.text = ball.description;
    }
}

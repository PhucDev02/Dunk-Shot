using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StarObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    private void Reset()
    {
        sprite.DOKill();
        sprite.DOFade(1, 0);
        transform.DOKill();
        transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        GetComponent<CircleCollider2D>().enabled = true;

    }
    private void Start()
    {
        Reset();
    }
    private void OnEnable()
    {
        Reset();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.Play("GainStar");
        transform.DOKill();
        transform.DOMoveY(transform.position.y + 0.5f, 0.4f);
        transform.DOScale(1.5f, 0.4f);
        sprite.DOFade(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
        PlayerPrefs.SetInt("Stars", PlayerPrefs.GetInt("Stars") + 5);
        UI_Controller.Instance.UpdateCurrency();
        GetComponent<CircleCollider2D>().enabled = false;
    }
    private void OnDisable()
    {
        transform.DOScale(1, 0);
    }
}

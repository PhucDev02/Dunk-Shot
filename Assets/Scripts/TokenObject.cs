using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TokenObject : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    private void Start()
    {
        transform.DOScale(1.1f, 0.4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        GetComponent<CircleCollider2D>().enabled = true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject != null && sprite != null)
        {
            AudioManager.Instance.Play("GainToken");
            GameplayChallengeManager.Instance.UpdateTokenCount();
            transform.DOKill();
            transform.DOMoveY(transform.position.y + 0.5f, 0.4f);
            transform.DOScale(1.5f, 0.4f);
            sprite.DOFade(0, 0.4f);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HoopEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public SpriteRenderer ring1, ring2, ring3;
    private Vector3 v1, v2, v3;
    private void Awake()
    {
        v1 = new Vector3(0.35f, 0.35f, 0.35f);
        v2 = new Vector3(0.5f, 0.5f, 0.5f);
        v3 = new Vector3(0.7f, 0.7f, 0.7f);
        Reset();
    }
    // 0.3 0.4 0.55
    public void ShootEffect()
    {
        ring1.transform.DOScale(0.25f, 0.5f).SetDelay(0.05f);
        ring1.DOFade(1, 0.25f).SetDelay(0.1f).OnComplete(() =>
        {
            ring1.DOFade(0, 0.25f).OnComplete(() =>
            {
                ring1.transform.localScale = v1;
            });
        });
        ring2.transform.DOScale(0.25f, 0.5f).SetDelay(0.15f);
        ring2.DOFade(1, 0.25f).SetDelay(0.2f).OnComplete(() =>
        {
            ring2.DOFade(0, 0.25f).OnComplete(() =>
            {
                ring2.transform.localScale = v2;
            });
        });
        ring3.transform.DOScale(0.25f, 0.5f).SetDelay(0.25f);
        ring3.DOFade(1, 0.25f).SetDelay(0.3f).OnComplete(() =>
        {
            ring3.DOFade(0, 0.25f).OnComplete(() =>
            {
                ring3.transform.localScale = v3;
            });
        });

    }
    public void Reset()
    {
        ring1.transform.localScale = v1;
        ring2.transform.localScale = v2;
        ring3.transform.localScale = v3;
        ring1.DOFade(0f, 0f);
        ring2.DOFade(0f, 0f);
        ring3.DOFade(0f, 0f);
    }

}

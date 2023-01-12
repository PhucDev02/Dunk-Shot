using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScrollForeground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> foreground, starsDecor;
    float foregroundDistance, starsDistance, velocity;
    [SerializeField] new Transform camera;
    [SerializeField] Vector3 lastPos;
    private void Awake()
    {
        foreground = new List<Transform>();
        for (int i = 0; i < 2; i++)
        {
            foreground.Add(transform.GetChild(i));
        }
        foregroundDistance = foreground[1].transform.position.y - foreground[0].transform.position.y;
        starsDistance = starsDecor[1].transform.position.y - starsDecor[0].transform.position.y;
        this.RegisterListener(EventID.OnChangeTheme, (param) => ApplyThemeAndDarkmode());
        this.RegisterListener(EventID.OnSwitchDarkmode, (param) => ApplyThemeAndDarkmode());

    }
    private void Start()
    {
        ApplyThemeAndDarkmode();
    }
    void ApplyThemeAndDarkmode()
    {
        if (GameManager.Instance.GetTheme().theme == Themes.Cloudy_Sky && PlayerPrefs.GetInt("Darkmode") == 1)
        {
            starsDecor[0].gameObject.SetActive(true);
            starsDecor[1].gameObject.SetActive(true);
        }
        else
        {
            starsDecor[0].gameObject.SetActive(false);
            starsDecor[1].gameObject.SetActive(false);
        }
        for (int i = 0; i < foreground.Count; i++)
        {
            if (PlayerPrefs.GetInt("Darkmode") == 0)
            {
                foreground[i].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetTheme().foreground;
                foreground[i].GetComponent<SpriteRenderer>().DOFade(GameManager.Instance.GetTheme().alphaLight, 0);
            }
            else
            {
                foreground[i].GetComponent<SpriteRenderer>().sprite = GameManager.Instance.GetTheme().darkForeground;
                foreground[i].GetComponent<SpriteRenderer>().DOFade(GameManager.Instance.GetTheme().alphaDark, 0);

            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        velocity = (camera.position.y - lastPos.y) / Time.fixedDeltaTime;
        lastPos = camera.position;

        for (int i = 0; i < foreground.Count; i++)
        {
            foreground[i].position = Vector3.Lerp(foreground[i].position, foreground[i].position - Vector3.up * velocity * 0.25f, Time.fixedDeltaTime);
            if (camera.position.y >= foreground[i].position.y)
            {
                foreground[1 - i].position = foreground[i].position + Vector3.up * foregroundDistance;
            }
            else
                foreground[1 - i].position = foreground[i].position - Vector3.up * foregroundDistance;
        }
        for (int i = 0; i < starsDecor.Count; i++)
        {
            starsDecor[i].position = Vector3.Lerp(starsDecor[i].position, starsDecor[i].position - Vector3.up * velocity * 0.15f, Time.fixedDeltaTime);
            if (camera.position.y >= starsDecor[i].position.y)
            {
                starsDecor[1 - i].position = starsDecor[i].position + Vector3.up * starsDistance;
            }
            else
                starsDecor[1 - i].position = starsDecor[i].position - Vector3.up * starsDistance;
        }
    }
}

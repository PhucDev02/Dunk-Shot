using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollForeground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> foreground;
    float distance;
    [SerializeField] new Transform camera;
    private void Awake()
    {
        foreground = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            foreground.Add(transform.GetChild(i));
        }
        distance = foreground[1].transform.position.y - foreground[0].transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < foreground.Count; i++)
            if (camera.position.y >= foreground[i].position.y)
            {
                foreground[1 - i].position = foreground[i].position + Vector3.up * distance;
            }
            else
                foreground[1 - i].position = foreground[i].position - Vector3.up * distance;
    }
}

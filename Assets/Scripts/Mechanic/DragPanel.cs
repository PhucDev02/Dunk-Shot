using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startPosition, endPosition;
    public static Vector2 force;
    public static float maxNetScale = 1.8f, maxMagnitude = 3, minMagnitude = 1.5f, forceCoef = 290; //275
    private bool isValid;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI() && !BallController.isOnAir)
        {
            isValid = true;
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UI_Menu.Instance.Hide();
        }

        if (Input.GetMouseButtonUp(0) && isValid==true)
        {
            this.PostEvent(EventID.OnShoot);
            force = Vector2.zero;
            isValid = false;
        }
        if (isValid)
        {
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            force = startPosition - endPosition;
            this.PostEvent(EventID.OnDrag);
        }
    }
    public static float GetAngle()
    {
        return force.x < 0 ? Vector3.Angle(Vector3.up, force) : -Vector3.Angle(Vector3.up, force);
    }
    public static float GetScale()
    {
        return force.magnitude < maxMagnitude ? 1 + (force.magnitude / maxMagnitude) * 0.8f : maxNetScale;
    }
    public static Vector2 getForce()
    {
        return (force.magnitude >= maxMagnitude ? force.normalized * maxMagnitude : force) * forceCoef;
    }
    public bool IsMouseOverUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

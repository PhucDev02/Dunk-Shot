using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] new BoxCollider2D collider;
    Vector2 startPosition, endPosition;
    public static Vector2 force;
    private static float maxNetScale = 1.8f, maxMagnitude = 370, forceCoef = 2f;
    private bool isValid;
    private void OnMouseDown()
    {
        if (!IsMouseOverUI())
        {
            isValid = true;
            startPosition = Input.mousePosition;
            UI_Menu.Instance.Hide();
        }
        else isValid = false;

    }
    private void OnMouseDrag()
    {
        {
            if (!IsMouseOverUI() && isValid)
            {
                endPosition = Input.mousePosition;
                force = startPosition - endPosition;
                this.PostEvent(EventID.OnDrag);
            }
        }
    }
    private void OnMouseUp()
    {
        //if (!IsMouseOverUI())
        {
            this.PostEvent(EventID.OnShoot);
            force = Vector2.zero;
        }
    }
    public static float GetAngle()
    {
        if (force.x < 0)
            return Vector3.Angle(Vector3.up, force);
        else
            return -Vector3.Angle(Vector3.up, force);
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

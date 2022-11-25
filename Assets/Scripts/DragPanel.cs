using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BoxCollider2D collider;
    Vector2 startPosition, endPosition;
    public static Vector2 force;
    private static float maxNetScale = 1.8f,maxMagnitude=370,forceCoef=2f;
   
    private void OnMouseDown()
    {
        startPosition = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        endPosition = Input.mousePosition;
        force = startPosition - endPosition;
        this.PostEvent(EventID.OnDrag);
    }
    private void OnMouseUp()
    {
        this.PostEvent(EventID.OnShoot);
        force = Vector2.zero;
    }
    public static float GetAngle()
    {
        if (force.x < 0)
            return Vector3.Angle(Vector3.up, force) ;
        else 
            return -Vector3.Angle(Vector3.up, force);
    }
    public static float GetScale()
    {
        return force.magnitude<maxMagnitude?1+(force.magnitude/maxMagnitude)*0.8f:maxNetScale;
    }
    public static Vector2 getForce()
    {
        return (force.magnitude >= maxMagnitude ? force.normalized*maxMagnitude : force)*forceCoef;
    }
}

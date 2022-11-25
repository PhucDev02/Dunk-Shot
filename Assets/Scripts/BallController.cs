using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] CircleCollider2D collider;
    public void Shoot()
    {
        rigidBody.simulated = true;
        rigidBody.AddForce(DragPanel.getForce());
        transform.SetParent(null);
        rigidBody.angularVelocity = Random.Range(300, 1200);
    }
    public void ContactHoop()
    {
        rigidBody.simulated = false;
        rigidBody.velocity = Vector2.zero;
    }
    public float CalculateBallSpeed()
    {
        return (DragPanel.getForce().magnitude / rigidBody.mass) * Time.fixedDeltaTime;
    }
    public float CalculateTimeGetHighest()
    {
        return Mathf.Abs(CalculateBallSpeed() * Mathf.Sin((90 - DragPanel.GetAngle()) * Mathf.Deg2Rad) / Physics2D.gravity.magnitude);
    }
}

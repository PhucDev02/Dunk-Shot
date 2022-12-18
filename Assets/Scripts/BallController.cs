using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] new CircleCollider2D collider;
   public static bool isOnAir=true;
    private void Awake()
    {
        this.RegisterListener(EventID.OnSecondChange, (param) => Respawn());

    }
    public void Shoot()
    {
        rigidBody.simulated = true;
        rigidBody.AddForce(DragPanel.getForce());
        transform.SetParent(null);
        rigidBody.angularVelocity = Random.Range(300, 1200);
        GameController.Instance.isPerfect = true;
    }
    public void ContactHoop()
    {
        isOnAir = false;
        rigidBody.simulated = false;
        rigidBody.velocity = Vector2.zero;
    }
    public void Respawn()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.zero;
        gameObject.SetActive(true);
        transform.position = HoopsPooler.Instance.GetLastHoop().position + Vector3.up * 1f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            this.PostEvent(EventID.OnBounceWall);
        }
        if (collision.gameObject.CompareTag("HoopSide"))
        {
            this.PostEvent(EventID.OnBounceSide);
        }
        if (collision.gameObject.CompareTag("DeadBar"))
        {
            rigidBody.velocity = Vector2.zero;
            //gameObject.SetActive(false);
            this.PostEvent(EventID.OnGameOver);
        }
    }
    public void NewGame()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.simulated = true;
        gameObject.transform.SetParent(null);
        gameObject.transform.position = GameManager.initPosBall;
    }
}

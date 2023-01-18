using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] new CircleCollider2D collider;
    [SerializeField] BallTrail trail;
    public static bool isOnAir = true;
    private Vector3 startScale;
    private void Awake()
    {
        this.RegisterListener(EventID.OnSecondChange, (param) => Respawn());
        startScale = transform.lossyScale;

    }
    public void Shoot()
    {
        rigidBody.simulated = true;
        rigidBody.AddForce(DragPanel.getForce());
        transform.SetParent(null);
        rigidBody.angularVelocity = Random.Range(300, 1200);
        GameController.Instance.bounceCnt = 0;
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
       if(this!=null)
        {
            if (GameController.Instance.challengeMode && ChallengeManager.Instance.type == 3)
            {
                TimeGameplay.Instance.Reset();
            }
            Reset();
            transform.position = HoopsPooler.Instance.GetLastHoop().position + Vector3.up * 1.2f;
        }
        //catch { }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            AudioManager.Instance.Play("BounceWall");
            this.PostEvent(EventID.OnBounceWall);
        }
        if (collision.gameObject.CompareTag("HoopSide"))
        {
            AudioManager.Instance.Play("CollideVsNet");
            this.PostEvent(EventID.OnBounceSide);
        }
        if (collision.gameObject.CompareTag("DeadBar"))
        {
            rigidBody.velocity = Vector2.zero;
            AudioManager.Instance.Play("GameOver");
            this.PostEvent(EventID.OnGameOver);
        }
    }
    public void NewGame()
    {
        Reset();
        gameObject.transform.position = GameManager.initPosBall;
    }
    public void Reset()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider.enabled = true;
        rigidBody.velocity = Vector2.zero;
        rigidBody.simulated = true;
        rigidBody.angularVelocity = 0;
        transform.SetParent(null);
        trail.DeactiveEffect();
    }
}

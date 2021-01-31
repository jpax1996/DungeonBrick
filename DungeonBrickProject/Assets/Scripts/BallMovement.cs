using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
   public float mMovementSpeed = 2;
    public TrajectoryDisplay mTrajectorySystem;
    public delegate void StartThrow();
    public static event StartThrow OnStartThrow;
    public delegate void SpellHit();
    public static event SpellHit OnSpellHit;
    private Vector3 mBallVelocity;
    private bool mIsBallRolling;
    private int mBallDamage = 50;

    // Use this for initialization
    void Start()
    {
        LevelManager.OnLevelOver += ResetBallState;
        ResetBallState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mIsBallRolling)
        {
            this.GetComponent<Rigidbody2D>().MovePosition( this.transform.position + mBallVelocity * mMovementSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Handle finger movements based on touch phase.
                switch (touch.phase)
                {
                    // Record initial touch position.
                    case TouchPhase.Began:
                        SetupBallLaunch(touch);
                        break;
                    case TouchPhase.Moved:
                        SetupBallLaunch(touch);
                        break;
                    // Report that a direction has been chosen when the finger is lifted.
                    case TouchPhase.Ended:
                        mIsBallRolling = true;
                        mTrajectorySystem.SetActive(false);
                        this.GetComponent<TrailRenderer>().enabled = true;
                        //OnStartThrow();
                        break;
                }
            }
            if (Input.GetButton("Fire1"))
            {
                mTrajectorySystem.SetActive(true);
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                mBallVelocity = (mousePosition - transform.position).normalized;
                mTrajectorySystem.RefreshTajectory();
            } else if (Input.GetButtonUp("Fire1"))
            {
                mIsBallRolling = true;
                mTrajectorySystem.SetActive(false);
                this.GetComponent<TrailRenderer>().enabled = true;
                //OnStartThrow();   
            }
        }
    }

    void SetupBallLaunch(Touch currTouch)
    {
        mTrajectorySystem.SetActive(true);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(currTouch.position);
        touchPosition.z = 0;
        mBallVelocity = (touchPosition - transform.position).normalized;
        mTrajectorySystem.RefreshTajectory();
    }

    public Vector3 GetVelocity()
    {
        return mBallVelocity;
    }
  
    private void ResetBallState()
    {
        mBallVelocity = new Vector3(0, 0, 0);
        mIsBallRolling = false;
        this.GetComponent<TrailRenderer>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mIsBallRolling)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float distanceX = Mathf.Abs(Mathf.Abs(contact.point.x) - Mathf.Abs(this.transform.position.x));
                float distanceY = Mathf.Abs(Mathf.Abs(contact.point.y) - Mathf.Abs(this.transform.position.y));
                if (distanceX < distanceY)
                {
                    mBallVelocity.y = -mBallVelocity.y;
                }
                else
                {
                    mBallVelocity.x = -mBallVelocity.x;
                }
                break;
            }
            if (collision.gameObject.CompareTag("Enemy_Tag"))
            {
                collision.gameObject.GetComponent<Enemy>().OnHit(mBallDamage);
            }
            else if (collision.gameObject.name == "BotWallCollider")
            {
                ResetBallState();
            }
        }
    }
}
    

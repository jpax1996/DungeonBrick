using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float mMovementSpeed = 2;
    public int MAX_AIMING_ANGLE = 80;
    public TrajectoryDisplay mTrajectorySystem;

    private Vector3 mBallVelocity;
    private bool mIsBallRolling;
    private bool mAimingEnbaled;
    private bool mIsInputInAngle;
    private PlayerManager mPlayerManager;

    // Use this for initialization
    void Start()
    {
        mPlayerManager = this.GetComponent<PlayerManager>();

        StopBallMovement();
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
            if (mAimingEnbaled)
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
                            if (mIsInputInAngle)
                            {
                                mIsBallRolling = true;
                                mTrajectorySystem.SetActive(false);
                                this.GetComponent<TrailRenderer>().enabled = true;
                                GameEvents.current.ThrowStart();
                            }
                            break;
                    }
                }
                if (Input.GetButton("Fire1"))
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    mIsInputInAngle = IsAngleAllowed(mousePosition);
                    mTrajectorySystem.SetActive(mIsInputInAngle);
                    mBallVelocity = (mousePosition - transform.position).normalized;
                    mTrajectorySystem.RefreshTajectory();
                } else if (Input.GetButtonUp("Fire1"))
                {
                    if (mIsInputInAngle)
                    {
                        mIsBallRolling = true;
                        mTrajectorySystem.SetActive(false);
                        this.GetComponent<TrailRenderer>().enabled = true;
                        GameEvents.current.ThrowStart();
                    }
                }
            }
        }
    }

    private bool IsAngleAllowed(Vector3 targetPosition)
    {
        Vector3 fromDir = transform.up;
        Vector3 targetDir = targetPosition - transform.position;
        float angle = Vector2.Angle(fromDir, targetDir);
        return angle > MAX_AIMING_ANGLE ? false : true;
    }

    void SetupBallLaunch(Touch currTouch)
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(currTouch.position);
        touchPosition.z = 0;
        mIsInputInAngle = IsAngleAllowed(touchPosition);
        mTrajectorySystem.SetActive(mIsInputInAngle);
        mBallVelocity = (touchPosition - transform.position).normalized;
        mTrajectorySystem.RefreshTajectory();
    }

    public Vector3 GetVelocity()
    {
        return mBallVelocity;
    }
  
    public void StopBallMovement()
    {
        mBallVelocity = new Vector3(0, 0, 0);
        mIsBallRolling = false;
        this.GetComponent<TrailRenderer>().enabled = false;
    }
    
    public bool IsBallRolling()
    {
        return mIsBallRolling;
    }

    public void SetEnableThrow(bool isThrowEnabled)
    {
        mAimingEnbaled = isThrowEnabled;
    }

    private void SetIsInputInAngle(bool isInAngle)
    {
        mIsInputInAngle = isInAngle;
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
                mPlayerManager.OnHitEnemy(collision.gameObject);
            }
            else if (collision.gameObject.name == "BotWallCollider")
            {
                GameEvents.current.ThrowOver();
            }
        }
    }
}
    

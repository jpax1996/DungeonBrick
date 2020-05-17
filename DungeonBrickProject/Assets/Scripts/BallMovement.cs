using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Transform mSpawnTransform;
    public float mMovementSpeed = 2;
    public TrajectoryDisplay mTrajectorySystem;
    public delegate void StartThrow();
    public static event StartThrow OnStartThrow;
    private Vector3 mBallVelocity;
    private bool mIsBallRolling;
    private int mBallDamage = 1;

    // Use this for initialization
    void Start()
    {
        SpellTimer.OnSpellTimeOver += ResetBallState;
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
            SetupBallLaunch();
            if (Input.GetButtonDown("Fire1"))
            {
                mIsBallRolling = true;
                mTrajectorySystem.SetActive(false);
                this.GetComponent<TrailRenderer>().enabled = true;
                OnStartThrow();
            }
        }
    }

    void SetupBallLaunch()
    {
        mTrajectorySystem.SetActive(true);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        mBallVelocity = (mousePosition - transform.position).normalized;
        mTrajectorySystem.RefreshTajectory();
    }

    public Vector3 GetVelocity()
    {
        return mBallVelocity;
    }
  
    private void ResetBallState()
    {
        transform.position = mSpawnTransform.position;
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
        }    
    }
}
    

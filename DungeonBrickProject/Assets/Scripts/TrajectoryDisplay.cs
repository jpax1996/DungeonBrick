using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDisplay : MonoBehaviour {

    public GameObject mTrajectoryDotPrefab;
    private GameObject mTrajectorySegmentsParent;
    private GameObject[] mTrajectorySegments;
    private Vector3 mTrajectoryVelocity;
    public int mTrajectorySize = 10;
    public float mDotDistance = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetActive(bool active)
    {
        if(mTrajectorySegmentsParent)
            mTrajectorySegmentsParent.SetActive(active);
    }

    public void RefreshTajectory()
    {
        mTrajectoryVelocity = this.GetComponent<BallMovement>().GetVelocity();
        if (mTrajectorySegments == null)
        {
            CreateTrajectorySegments();
        }
        else
        {
            UpdateSegmentsPosition();
        }
    }

    private void CreateTrajectorySegments()
    {
        mTrajectorySegmentsParent = new GameObject("TrajectorySegments");
        mTrajectorySegments = new GameObject[mTrajectorySize];
        for (int i = 0; i < mTrajectorySize; i++)
        {
            if (i == 0)
            {
                mTrajectorySegments[i] = PlaceTrajectoryPoint(transform.position);
            }
            else
            {
                mTrajectorySegments[i] = PlaceTrajectoryPoint(mTrajectorySegments[i - 1].transform.position);
            }
        }
    }

    private Vector3 CalculateTrajectory(Vector3 initialPosition)
    {
        return mTrajectoryVelocity * mDotDistance + initialPosition;
    }

    private GameObject PlaceTrajectoryPoint(Vector3 initialPosition)
    {
        CheckSegmentCollision(initialPosition);
        GameObject trajectoryDot = Instantiate(mTrajectoryDotPrefab);
        trajectoryDot.transform.position = CalculateTrajectory(initialPosition);
        trajectoryDot.transform.parent = mTrajectorySegmentsParent.transform;
        return trajectoryDot;
    }

    private bool CheckSegmentCollision(Vector3 segmentPosition)
    {
        RaycastHit2D contact;
        contact = Physics2D.CircleCast((Vector2)segmentPosition, this.GetComponent<CircleCollider2D>().radius, new Vector2(0, 0), 1, LayerMask.GetMask("Obstacle"));
        if (contact)
        {
            if (contact.point.x == segmentPosition.x)
            {
                mTrajectoryVelocity.y = -mTrajectoryVelocity.y;
            }
            if (contact.point.y == segmentPosition.y)
            {
                mTrajectoryVelocity.x = -mTrajectoryVelocity.x;
            }
            return true;
        }
        return false;
    }

    private void UpdateSegmentsPosition()
    {
        for (int i = 0; i < mTrajectorySize; i++)
        {
            if (i == 0)
            {
                mTrajectorySegments[i].transform.position = CalculateTrajectory(this.transform.position);
            }
            else
            {
                //CheckSegmentCollision(mTrajectorySegments[i-1].transform.position);
                mTrajectorySegments[i].transform.position = CalculateTrajectory(mTrajectorySegments[i - 1].transform.position);
                if (CheckSegmentCollision(mTrajectorySegments[i].transform.position))
                {
                    mTrajectorySegments[i].transform.position = CalculateTrajectory(mTrajectorySegments[i].transform.position);
                }
            }
        }
    }
}

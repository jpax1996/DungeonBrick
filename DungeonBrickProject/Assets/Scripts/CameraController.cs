using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform CameraStartPosition;

    public float mDefaulAspect = 1.5f;
    public float mDefaultOrthoSize = 9f;
    private Resolution mCurrResolution;
    // Transform of the GameObject you want to shake
    private Transform mTransform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.1f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 2f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    // Use this for initialization
    void Start () {
        
        mTransform = this.transform;
        initialPosition = mTransform.position;
        Enemy.OnEnemyDead += CameraShake;
	}
	
	// Update is called once per frame
	void Update () {
        if(mCurrResolution.width != Screen.currentResolution.width || mCurrResolution.height != Screen.currentResolution.height)
        {
            SetUpCameraOrthoSize();
        }
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    private void CameraShake()
    {
        shakeDuration = 0.08f;
    }

    private void SetUpCameraOrthoSize()
    {
        mCurrResolution = Screen.currentResolution;
        float deviceApect = Screen.height / Screen.width;
        Camera.main.orthographicSize = (deviceApect / mDefaulAspect) * mDefaultOrthoSize;

        this.transform.position = CameraStartPosition.position;
    }
}

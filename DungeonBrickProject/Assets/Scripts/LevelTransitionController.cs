using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionController : MonoBehaviour
{
    public Sprite mLoadInSprite;
    public Sprite mLoadOutSprite;
    public float transitionSpeed;
    private Image mLoadImage;
    private bool mStartLoadingIn;
    private bool mStartLoadingOut;

    public delegate void LoadingInEnded();
    public static event LoadingInEnded OnLoadingInOver;
    
    public delegate void LoadingOutEnded();
    public static event LoadingOutEnded OnLoadingOutOver;

    const string TEXTURE_SHADER_REFERENCE = "_TransitionEffect";
    const string CUTOFF_SHADER_REFERENCE = "_CutOff";
    const string EDGE_SMOOTHING_SHADER_REFERENCE = "_EdgeSmoothing";

    // Start is called before the first frame update
    void Start()
    {
        mLoadImage = this.GetComponent<Image>();
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, 1.1f);
        LevelManager.OnLevelOver += StartLoadingOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!mStartLoadingIn)
            {
                StartLoadingIn();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(!mStartLoadingOut)
            {
                StartLoadingOut();
            }
        }
        if (mStartLoadingIn)
        {
            LoadeIn();
        }
        else if(mStartLoadingOut)
        {
            LoadgOut();
        }
    }
    
    public void StartLoadingIn()
    {
        mStartLoadingIn = true;
        mStartLoadingOut = false;
        mLoadImage.material.SetTexture(TEXTURE_SHADER_REFERENCE, mLoadInSprite.texture);
    }

    private void LoadeIn()
    {
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, Mathf.MoveTowards(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE), 1.1f, transitionSpeed * Time.deltaTime));
        if(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE) == 1.1f)
        {
            mStartLoadingIn = false;
            OnLoadingInOver();
        }
    }

    public void StartLoadingOut()
    {
        mStartLoadingOut = true;
        mStartLoadingIn = false;
        mLoadImage.material.SetTexture(TEXTURE_SHADER_REFERENCE, mLoadOutSprite.texture);
    }

    private void LoadgOut()
    {
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, Mathf.MoveTowards(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE), -0.1f - mLoadImage.material.GetFloat(EDGE_SMOOTHING_SHADER_REFERENCE), transitionSpeed * Time.deltaTime));
        if (mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE) == -0.1f - mLoadImage.material.GetFloat(EDGE_SMOOTHING_SHADER_REFERENCE))
        {
            mStartLoadingOut = false;
            OnLoadingOutOver();
            StartLoadingIn();
        }
    }
}

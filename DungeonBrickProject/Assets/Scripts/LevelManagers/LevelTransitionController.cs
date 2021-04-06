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
    private GameObject mCanvasObject;
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
        mCanvasObject = this.transform.parent.gameObject;
        mCanvasObject.SetActive(false);
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, 1.1f);
        LevelManager.OnLevelOver += StartLoadingOut;
    }

    // Update is called once per frame
    void Update()
    {
        if (mStartLoadingIn)
        {
            LoadIn();
        }
        else if(mStartLoadingOut)
        {
            LoadOut();
        }
    }
    
    public void StartLoadingIn()
    {
        mStartLoadingIn = true;
        mStartLoadingOut = false;
        mLoadImage.material.SetTexture(TEXTURE_SHADER_REFERENCE, mLoadInSprite.texture);
        mCanvasObject.SetActive(true);
    }

    private void LoadIn()
    {
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, Mathf.MoveTowards(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE), 1.1f, transitionSpeed * Time.deltaTime));
        if(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE) == 1.1f)
        {
            mCanvasObject.SetActive(false);
            mStartLoadingIn = false;
            OnLoadingInOver();
        }
    }

    public void StartLoadingOut()
    {
        mStartLoadingOut = true;
        mStartLoadingIn = false;
        mLoadImage.material.SetTexture(TEXTURE_SHADER_REFERENCE, mLoadOutSprite.texture);
        mCanvasObject.SetActive(true);
    }

    private void LoadOut()
    {
        mLoadImage.material.SetFloat(CUTOFF_SHADER_REFERENCE, Mathf.MoveTowards(mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE), -0.1f - mLoadImage.material.GetFloat(EDGE_SMOOTHING_SHADER_REFERENCE), transitionSpeed * Time.deltaTime));
        if (mLoadImage.material.GetFloat(CUTOFF_SHADER_REFERENCE) == -0.1f - mLoadImage.material.GetFloat(EDGE_SMOOTHING_SHADER_REFERENCE))
        {
            mStartLoadingOut = false;
            mCanvasObject.SetActive(false);
            OnLoadingOutOver();
            StartLoadingIn();
        }
    }
}

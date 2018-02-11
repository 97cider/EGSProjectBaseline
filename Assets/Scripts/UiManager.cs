using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiManager : MonoBehaviour {
    public Image progressBar, progressBarBG;
    public GameObject worldSpaceText;
    public GameObject InteractIcon;
    public Sprite searchIcon, talkIcon, openIcon, travelIcon;
    public string BounceAnim, idleAnim;

    private Animator interactAnim;
    private Image interactSprite;
    void Start()
    {
        progressBar.enabled = false;
        interactAnim = InteractIcon.GetComponent<Animator>();
        interactSprite = InteractIcon.GetComponent<Image>();
    }
    public void OverrideText(string text, InteractType inttype, InteractProcess intproc)
    {
        worldSpaceText.GetComponent<Text>().text = text;
        if(inttype != InteractType.Travel)
        {
            interactAnim.Play(idleAnim);
            if(inttype == InteractType.Search)
            {
                interactSprite.sprite = searchIcon;
            }
            if(intproc == InteractProcess.Duration)
            {
                progressBar.enabled = true;
                progressBarBG.enabled = true;
            }
            else
            {
                progressBar.enabled = false;
                progressBarBG.enabled = false;
            }
        }
        else
        {
            interactAnim.Play(BounceAnim);
        }
    }
    public void RestartProgressBar()
    {
        progressBar.enabled = false;
        progressBarBG.enabled = false;
        progressBar.fillAmount = 0;
   
    }
    public void UpdateProgessBar(float current, float goal)
    {
        if(current > 0)
        {
            progressBar.enabled = true;
            progressBarBG.enabled = true;

        }
        progressBar.fillAmount = current / goal;
    }
   public void InteractUIMover(Vector3 loc)
    {
        worldSpaceText.transform.position = Camera.main.WorldToScreenPoint(loc);
    }
	// Update is called once per frame
	void Update () {
	}
}

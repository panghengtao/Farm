using UnityEngine;
using System.Collections;

public class PopupAchievement : MonoBehaviour {

    UILabel title, detail;
	void Awake () {
        title = transform.FindChild("Title").GetComponent<UILabel>();
        detail = transform.FindChild("Detail").GetComponent<UILabel>();
    }
	
	void Update () {
	    
	}

    public void ShowPopup(string s_title, string s_detail)
    {
        AudioControl.DPlaySound("Danh hieu moi");
        title.text = s_title;
        detail.text = s_detail;
        LeanTween.moveLocalY(this.gameObject, 260, 0.5f).setEase(LeanTweenType.easeOutCubic).setUseEstimatedTime(true).setOnComplete(() =>
        {
            LeanTween.moveLocalY(this.gameObject, 450, 0.5f).setOnComplete(() =>
            {
                Destroy(gameObject);
            }).setEase(LeanTweenType.easeInCubic).setUseEstimatedTime(true).setDelay(2.5f);
        });
    }
}

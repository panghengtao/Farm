using UnityEngine;
using System.Collections;

public class DialogAskResult : DialogAbs
{
    Transform bgBlack, bgMain;

    void Start()
    {
        bgMain = transform.FindChild("Main");
        bgBlack = transform.FindChild("Black");
    }

    void Update()
    {
        if (Show)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideDialog();
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShowDialog();
        }
    }

    public override void ShowDialog(DialogAbs.CallBackShowDialog callback = null)
    {
        Show = true;
        CommonObjectScript.isViewComplete = true;
        bgMain.FindChild("Logo").FindChild("Title").GetComponent<UILabel>().text = MissionControl.Language["COMPLETE"];
        bgMain.FindChild("Cancel").FindChild("LabelCancel").GetComponent<UILabel>().text = MissionControl.Language["Cancel"];
        bgMain.FindChild("Ok").FindChild("LabelOK").GetComponent<UILabel>().text = MissionControl.Language["Ok"];
        bgMain.FindChild("TextShow").GetComponent<UILabel>().text = MissionControl.Language["Quest_finish_detail"];
        int addGold = (int)(CommonObjectScript.maxTimeOfMission * 200);
        bgMain.FindChild("TextShow1").GetComponent<UILabel>().text = CommonObjectScript.maxTimeOfMission + " " + MissionControl.Language["day_reamain_equal"] + " = ";
        bgMain.FindChild("TextShow2").GetComponent<UILabel>().text = "" + addGold;
        //An bang task khi hien thi result
        CommonObjectScript.isViewPoppup = true;
        GameObject task = GameObject.Find("DialogTask").gameObject;
        if (task != null)
        {
            task.GetComponent<DialogTask>().HideButton();
        }
        bgMain.gameObject.SetActive(true);
        bgBlack.gameObject.SetActive(true);
        LeanTween.scale(bgMain.gameObject, new Vector3(1, 1, 1), 0.4f).setUseEstimatedTime(true).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
        {
            Time.timeScale = 0;
        });
        //Hide loading scene if it is showing
        LoadingScene.HideLoadingScene();
    }

    public override void HideDialog(DialogAbs.CallBackHideDialog callback = null)
    {
        CommonObjectScript.isViewComplete = false;
        LeanTween.scale(bgMain.gameObject, new Vector3(0.0f, 0.0f, 0), 0.4f).setEase(LeanTweenType.easeInBack).setUseEstimatedTime(true).setOnComplete(() =>
       {
           CommonObjectScript.isViewPoppup = false;
           Time.timeScale = 1;
           bgMain.gameObject.SetActive(false);
           bgBlack.gameObject.SetActive(false);
           Show = false;
           print("aaaaaaaaaaa");
       });
    }

    public void ButtonOk()
    {
        HideDialog();
        //CommonObjectScript.dollar += (int)(200 * CommonObjectScript.maxTimeOfMission);
        CommonObjectScript.AddDollarStatic((int)(200 * CommonObjectScript.maxTimeOfMission));
        DialogResult.ShowResult();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && Show)
        {
            HideDialog();
        }
    }
}

using UnityEngine;
using System.Collections;

public class DialogInfo : DialogAbs
{
    public Transform DialogConfirm;
    Transform dialogMain, bgBlack;
    Transform left, right;

    void Start()
    {
        dialogMain = transform.FindChild("Main");
        bgBlack = transform.FindChild("BgBlack");
        left = transform.FindChild("Panel").FindChild("Left");
        right = transform.FindChild("Panel").FindChild("Right");
    }

    void Update()
    {

    }

    public override void ShowDialog(DialogAbs.CallBackShowDialog callback = null)
    {
        if (CommonObjectScript.isViewPoppup)
        {
            return;
        }
        CommonObjectScript.isViewPoppup = true;
        dialogMain.FindChild("Panel").FindChild("Logo").FindChild("Title").GetComponent<UILabel>().text = MissionControl.Language["INFO"];
        if ("Vietnamese".Equals(VariableSystem.language))
        {
            dialogMain.FindChild("En").gameObject.SetActive(false);
            dialogMain.FindChild("Vi").gameObject.SetActive(true);
        }
        else
        {
            dialogMain.FindChild("En").gameObject.SetActive(true);
            dialogMain.FindChild("Vi").gameObject.SetActive(false);
        }
        Show = true;
        bgBlack.gameObject.SetActive(true);
        dialogMain.gameObject.SetActive(true);
        left.parent.gameObject.SetActive(true);
        LeanTween.scale(dialogMain.gameObject, new Vector3(1, 1, 1), 0.4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(left.gameObject, -130, 0.4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(right.gameObject, -130, 0.4f).setEase(LeanTweenType.easeOutBack);
        dialogMain.FindChild("En").FindChild("Texture").GetComponent<TweenPosition>().ResetToBeginning();
        dialogMain.FindChild("Vi").FindChild("Texture").GetComponent<TweenPosition>().ResetToBeginning();

    }

    public override void HideDialog(DialogAbs.CallBackHideDialog callback = null)
    {
        LeanTween.moveLocalY(left.gameObject, -645, 0.4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveLocalY(right.gameObject, -650, 0.4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(dialogMain.gameObject, new Vector3(0, 0, 0), 0.3f).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            Show = false;
            bgBlack.gameObject.SetActive(false);
            left.parent.gameObject.SetActive(false);
            CommonObjectScript.isViewPoppup = false;
            dialogMain.gameObject.SetActive(false);
        });
    }
}

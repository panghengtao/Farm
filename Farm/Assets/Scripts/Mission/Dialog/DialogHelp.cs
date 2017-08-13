using UnityEngine;
using System.Collections;
using System;

public class DialogHelp : DialogAbs
{

    UIGrid grid;
    Transform scroll;
    Transform[] pages;
    Transform dialogMain, bgBlack;

    void Start()
    {
        dialogMain = transform.FindChild("Main");
        bgBlack = transform.FindChild("BgBlack");
        grid = transform.FindChild("Main").FindChild("Scroll View").FindChild("Grid").GetComponent<UIGrid>();
        scroll = transform.FindChild("Main").FindChild("Scroll View");
        pages = new Transform[16];
        for (int i = 1; i <= 16; i++)
        {
            pages[i - 1] = transform.FindChild("Main").FindChild("Page").FindChild("" + i).FindChild("On");
            if ("Vietnamese".Equals(VariableSystem.language))
            {
                scroll.FindChild(i + "").FindChild("Vi").gameObject.SetActive(true);
                scroll.FindChild(i + "").FindChild("En").gameObject.SetActive(false);
            }
            else
            {
                scroll.FindChild(i + "").FindChild("Vi").gameObject.SetActive(false);
                scroll.FindChild(i + "").FindChild("En").gameObject.SetActive(true);
            }
        }
        //grid.transform.parent.GetComponent<UIScrollView>().onDragFinished
    }

    // Update is called once per frame
    void Update() {
        if(Show)
        {
            for(int i = 0; i < pages.Length; i++)
            {
                if (scroll.GetComponent<UICenterOnChild>().centeredObject != null && scroll.GetComponent<UICenterOnChild>().centeredObject.name.Equals(pages[i].parent.name))
                {
                    pages[i].gameObject.SetActive(true);
                }
                else
                {
                    pages[i].gameObject.SetActive(false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideDialog();
            }
        }
	}

    public override void ShowDialog(DialogAbs.CallBackShowDialog callback = null)
    {
        if (CommonObjectScript.isViewPoppup)
        {
            return;
        }
        CommonObjectScript.isViewPoppup = true;
        grid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2();
        grid.transform.parent.localPosition = new Vector2();
        scroll.GetComponent<UICenterOnChild>().CenterOn( scroll.FindChild(0 + ""));
        scroll.GetComponent<UICenterOnChild>().Recenter();
        dialogMain.FindChild("Bg1").FindChild("Texture").FindChild("Title").GetComponent<UILabel>().text = MissionControl.Language["HELP"];
        Show = true;
        bgBlack.gameObject.SetActive(true);
        dialogMain.gameObject.SetActive(true);
        LeanTween.scale(dialogMain.gameObject, new Vector3(1, 1, 1), 0.4f).setEase(LeanTweenType.easeOutBack).setUseEstimatedTime(true);
        for (int i = 1; i <= 16; i++)
        {
            if ("Vietnamese".Equals(VariableSystem.language))
            {
                scroll.FindChild(i + "").FindChild("Vi").gameObject.SetActive(true);
                scroll.FindChild(i + "").FindChild("En").gameObject.SetActive(false);
            }
            else
            {
                scroll.FindChild(i + "").FindChild("Vi").gameObject.SetActive(false);
                scroll.FindChild(i + "").FindChild("En").gameObject.SetActive(true);
            }
        }
    }

    public override void HideDialog(DialogAbs.CallBackHideDialog callback = null)
    {
        LeanTween.scale(dialogMain.gameObject, new Vector3(0, 0, 0), 0.4f).setUseEstimatedTime(true).setEase(LeanTweenType.easeInBack).setOnComplete(() =>
        {
            Time.timeScale = 1;
            print("aaaaaaaaaaa");
            Show = false;
            //dialogMain.gameObject.SetActive(false);
            bgBlack.gameObject.SetActive(false);
            CommonObjectScript.isViewPoppup = false;
        });
    }
}

using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.Common;

public class ItemMission : MonoBehaviour
{

    public int Level = 1;
    public int Star = 1;
    public bool IsOpen = true;
    public bool IsCurrentLevel = false;
    Transform gridAvatar;
    long score;
    void Awake()
    {
        gridAvatar = this.transform.FindChild("Grid");
        Level = Convert.ToInt32(this.gameObject.name);
        this.transform.FindChild("Button").FindChild("Level").GetComponent<UILabel>().text = "" + Level;
        transform.FindChild("Button").FindChild("BgBlue").gameObject.SetActive(false);
        transform.FindChild("Button").gameObject.GetComponent<UIButton>().enabled = false;
        transform.FindChild("Button").gameObject.GetComponent<UIButtonScale>().enabled = false;
        transform.FindChild("Button").FindChild("BgRed").gameObject.SetActive(false);
        //Debug.Log("START " + this.gameObject.name);
    }

    //update this mission when it is unlock 
    public void SetData(MissionDataSave data)
    {
        Transform Star1, Star2, Star3;
        Star1 = transform.FindChild("Star1");
        Star2 = transform.FindChild("Star2");
        Star3 = transform.FindChild("Star3");
        Star1.gameObject.SetActive(false);
        Star2.gameObject.SetActive(false);
        Star3.gameObject.SetActive(false);
        // Debug.Log(" ---------- Set open level " + this.gameObject.name);
        this.IsCurrentLevel = false;
        this.IsOpen = true;
        this.score = data.Score;
        this.Star = data.Star;
        this.Level = data.Mission;
        transform.FindChild("Button").FindChild("BgBlue").gameObject.SetActive(true);
        transform.FindChild("Button").FindChild("BgRed").gameObject.SetActive(false);
        transform.FindChild("Button").FindChild("BgCurrent").gameObject.SetActive(false);
        transform.FindChild("Button").GetComponent<UIButton>().enabled = true;
        transform.FindChild("Button").GetComponent<UIButtonScale>().enabled = true;
        if (Star == 1)
        {
            Star1.gameObject.SetActive(true);
        }
        else if (Star == 2)
        {
            Star2.gameObject.SetActive(true);
        }
        else if (Star == 3)
        {
            Star3.gameObject.SetActive(true);
        }
        // Debug.Log(" -----mission " + data.Mission + " star " + Star + " isOpend " + IsOpen);

    }

    public void SetCurrentMission()
    {
        this.IsCurrentLevel = true;
        if (Star == 1)
        {
            transform.FindChild("Star1").gameObject.SetActive(true);
        }
        else if (Star == 2)
        {
            transform.FindChild("Star2").gameObject.SetActive(true);
        }
        else if (Star == 3)
        {
            transform.FindChild("Star3").gameObject.SetActive(true);
        }
        transform.FindChild("Button").FindChild("BgBlue").gameObject.SetActive(false);
        transform.FindChild("Button").FindChild("BgRed").gameObject.SetActive(true);
        transform.FindChild("Button").FindChild("BgCurrent").gameObject.SetActive(true);
    }

    public void ItemMissionButton()
    {
        DialogTask.complete = false;
        AudioControl.DPlaySound("Click 1");
        //Remove all item on Task Dialog
        Transform dialogTask = GameObject.Find("DialogTask").transform;
        dialogTask.GetComponent<DialogTask>().RemoveAllItem();

        Debug.Log("READ LEVEL " + Level);
        MissionData.READ_XML(Level);
        GameObject.Find("UI Root").transform.FindChild("Mission").FindChild("Dialog").FindChild("DialogMission").gameObject.GetComponent<DialogMission>().ShowDialogMision(Level);
    }

    public void AvatarMissionButton()
    {
        if (gridAvatar.gameObject.GetComponent<UIGrid>().cellHeight == 15)
        {
            gridAvatar.gameObject.GetComponent<UIGrid>().cellHeight = 64;
            gridAvatar.gameObject.GetComponent<UIGrid>().Reposition();
        }
        else
        {
            gridAvatar.gameObject.GetComponent<UIGrid>().cellHeight = 15;
            gridAvatar.gameObject.GetComponent<UIGrid>().Reposition();
        }
        gridAvatar.gameObject.GetComponent<UIGrid>().repositionNow = true;
    }

    public void AddAvatar(Transform mAvatar, string mUserId)
    {
        Transform avatar = Instantiate(mAvatar) as Transform;
        avatar.GetComponent<Avatar>().SetData(this, mUserId);
        UIGrid grid = transform.FindChild("Grid").GetComponent<UIGrid>();
        grid.gameObject.SetActive(true);
        grid.AddChild(avatar);
        //Xet depth thu cong cho avatar => vl
        avatar.GetComponent<UIWidget>().depth = 3 +  (grid.GetChildList().Count - 1) * 2;
        avatar.FindChild("Border").GetComponent<UIWidget>().depth = 3 + (grid.GetChildList().Count - 1) * 2 + 1;
        avatar.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //avatar.GetComponent<UITexture>().depth = 4;
        //avatar.FindChild("Border").GetComponent<UITexture>().depth = 5;
    }

    public void RemoveAllAvatar()
    {
        if (gridAvatar == null)
        {
            gridAvatar = this.transform.FindChild("Grid");
        }
        for (int i = 0; i < gridAvatar.childCount; i++)
        {
            Transform item = gridAvatar.GetChild(i);
            gridAvatar.gameObject.GetComponent<UIGrid>().RemoveChild(item);
            Destroy(item.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : PanelBase<GameOverPanel>
{
    public UILabel timeLabel;
    public UIInput nameInput;
    public UIButton enterBtn;

    private int endTime;


    public override void Init()
    {
        enterBtn.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.AddRankData(nameInput.value,endTime);
            SceneManager.LoadScene("StartScene");
        }));

        HideSelf();
    }

    public override void ShowSelf()
    {
        base.ShowSelf();

        endTime = (int)GamePanel.Instance.nowTime;
        timeLabel.text = GamePanel.Instance.timeLabel.text;
    }

}

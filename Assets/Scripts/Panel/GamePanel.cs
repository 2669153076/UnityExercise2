using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : PanelBase<GamePanel>
{
    public UILabel timeLabel;
    public UIButton exitBtn;

    public List<GameObject> hpObjs;

    public float nowTime;


    public override void Init()
    {
        exitBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示确定退出面板
            Time.timeScale = 0;
            QuitPanel.Instance.ShowSelf();
        }));

        UpdateHP(GameDataMgr.Instance.GetNowSelAirplaneInfo().hp);

        //Invoke("ShowGameOver", 3);
    }

    private void ShowGameOver()
    {
        GameOverPanel.Instance.ShowSelf();
    }

    private void Update()
    {
        nowTime += Time.deltaTime;
        timeLabel.text = "";
        if ((int)nowTime / 3600 > 0)
            timeLabel.text += (int)nowTime / 3600 + "h";
        //分
        if ((int)nowTime % 3600 / 60 > 0 || timeLabel.text != "")
            timeLabel.text += (int)nowTime % 3600 / 60 + "m";
        //秒
        timeLabel.text+= (int)nowTime % 60 + "s";
    }

    public void UpdateHP(int hp)
    {
        for (int i = 0; i < hpObjs.Count; i++)
        {
            hpObjs[i].SetActive(i < hp);
        }
    }

}

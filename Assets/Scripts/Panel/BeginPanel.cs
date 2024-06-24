using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : PanelBase<BeginPanel>
{
    public UIButton startBtn;
    public UIButton rankBtn;
    public UIButton settingBtn;
    public UIButton quitBtn;


    public override void Init()
    {
        startBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示选角面板
            RoleSelPanel.Instance.ShowSelf();

            HideSelf();
        }));

        rankBtn.onClick.Add(new EventDelegate(() =>
        {
            //显示排行榜面板
            RankPanel.Instance.ShowSelf();
            
        }));

        settingBtn.onClick.Add(new EventDelegate(() =>
        {
            SettingPanel.Instance.ShowSelf();
        }));

        quitBtn.onClick.Add(new EventDelegate(() =>
        {
            Application.Quit();
        }));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : PanelBase<SettingPanel>
{
    public UIButton closeBtn;
    public UISlider musicSlider;
    public UISlider soundSlider;
    public UIToggle musicToggle;
    public UIToggle soundToggle;


    public override void Init()
    {
        closeBtn.onClick.Add(new EventDelegate(() =>
        {
            HideSelf();
        }));

        musicSlider.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetMusicVolumn(musicSlider.value);
        }));

        soundSlider.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetSoundVolumn(soundSlider.value);
        }));

        musicToggle.onChange.Add(new EventDelegate(()=>{
            GameDataMgr.Instance.SetMusicIsOpen(musicToggle.value);
        }));

        soundToggle.onChange.Add(new EventDelegate(() => {
            GameDataMgr.Instance.SetSoundIsOpen(soundToggle.value);
        }));

        HideSelf();
    }

    public override void ShowSelf()
    {
        base.ShowSelf();
        //显示自己时 更新面板内容
        SettingData settingData = GameDataMgr.Instance.settingData;
        musicSlider.value = settingData.musicVolumn;
        soundSlider.value = settingData.soundVolumn;
        musicToggle.value = settingData.isOpenMusic;
        soundToggle.value = settingData.isOpenSound;
    }

    public override void HideSelf()
    {
        base.HideSelf();
        //隐藏自己时 保存设置数据
        GameDataMgr.Instance.SavaSettingData();
    }


}

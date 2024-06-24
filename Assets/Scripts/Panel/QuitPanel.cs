using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : PanelBase<QuitPanel>
{

    public UIButton enterBtn;
    public UIButton closeBtn;

    public override void Init()
    {
        enterBtn.onClick.Add(new EventDelegate(()=>{
            SceneManager.LoadScene("StartScene");
        }));

        closeBtn.onClick.Add(new EventDelegate(() => {
            Time.timeScale = 1;
            HideSelf();
        }));

        HideSelf();
    }

    
}

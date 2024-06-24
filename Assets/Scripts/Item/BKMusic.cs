using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource bkAudio;

    private void Awake()
    {
        instance = this;
        bkAudio = GetComponent<AudioSource>();
        SetBKMusicIsOpen(GameDataMgr.Instance.settingData.isOpenMusic);
        SetBKMusicVolumn(GameDataMgr.Instance.settingData.musicVolumn);
    }

    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }

    public void SetBKMusicVolumn(float value)
    {
        bkAudio.volume = value;
    }
}

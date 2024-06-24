using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class PanelBase<T> : MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化方法
    /// </summary>
    public abstract void Init();

    public virtual void ShowSelf()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideSelf()
    {
        gameObject.SetActive(false);
    }
}

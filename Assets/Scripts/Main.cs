using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //根据开始场景中的选择 动态创建飞机
        RoleInfo roleInfo = GameDataMgr.Instance.GetNowSelAirplaneInfo();
        
        GameObject obj = Instantiate(Resources.Load<GameObject>(roleInfo.resPath));
        Player player = obj.AddComponent<Player>();

        player.moveSpeed = roleInfo.speed * 20;
        player.maxHP = 10;
        player.curHP = roleInfo.hp;
        player.roundSpeed = 20;
        player.playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();

        GamePanel.Instance.UpdateHP(roleInfo.hp);
    }

    
}

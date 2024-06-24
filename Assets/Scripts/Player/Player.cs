using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance=>instance;

    public int maxHP;
    public int curHP;

    public int moveSpeed = 20;
    public int roundSpeed = 20;
    //最终旋转目标角度
    private Quaternion targetQ;

    public bool isDeath = false;

    private float hValue;
    private float vValue;

    public Camera playerCamera;
    private Vector3 nowScreenPos; //当前世界坐标转屏幕上的位置
    private Vector3 frontPos; //玩家上一次的位置

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
            return;
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");
        if(hValue ==0)
        {
            targetQ = Quaternion.identity;
        }
        else
        {
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        }
        //让飞机朝着 目标角度旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, roundSpeed * Time.deltaTime);

        //位移之前记录位置
        frontPos = transform.position;

        transform.Translate(Vector3.forward * vValue * moveSpeed * Time.deltaTime,Space.World);
        transform.Translate(Vector3.right * hValue * moveSpeed * Time.deltaTime,Space.World);

        //坐标判断
        nowScreenPos = playerCamera.WorldToScreenPoint(transform.position);
        
        if (nowScreenPos.x <= 0 || nowScreenPos.x >= Screen.width)
        {
            transform.position = new Vector3(frontPos.x,transform.position.y,transform.position.z);
        }
        if(nowScreenPos.y<=0||nowScreenPos.y >= Screen.height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, frontPos.z);
        }

        //射线检测 用于销毁子弹
        if(Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000,1 << LayerMask.NameToLayer("Bullet")))
            {
                Bullet obj = hitInfo.transform.GetComponent<Bullet>();
                obj.Dead();
            }
        }
    
    }

    public void Dead()
    {
        isDeath = true;
        Time.timeScale = 0;
        BKMusic.Instance.SetBKMusicIsOpen(false);
        GameOverPanel.Instance.ShowSelf();
    }

    public void Wound()
    {
        if (isDeath)
            return;
        curHP -= 1;
        //更新游戏面板血量
        GamePanel.Instance.UpdateHP(curHP);
        if (curHP <= 0)
        {
            Dead();
        }
    }
}

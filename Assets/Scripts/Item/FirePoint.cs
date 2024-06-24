using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum E_PosType
{
    TopLeft,
    TopCenter,
    TopRight,

    CenterLeft,
    CenterRight,

    BottomLeft,
    BottomCenter,
    BottomRight

}

public class FirePoint : MonoBehaviour
{
    public E_PosType posType;
    public Camera playerCamera;

    private Vector3 screenPos;  //表示屏幕上的点
    private Vector3 initDir;    //子弹发射时初始方向
    private FirePointInfo firePointInfo;
    private int nowNum;
    private float nowDelay;
    private float nowCD;
    private BulletInfo nowBulletInfo;   //当前组的子弹信息
    private float spacingAngle; //散弹间隔角度
    private Vector3 nowDir; //发射散弹时 记录上一次方向
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
        ResetFirePointInfo();
        UpdateFirePoint();
    }

    private void UpdatePos()
    {
        //使开火点的z轴与角色所在的z轴相同，使其在同一水平面上
        screenPos.z = playerCamera.WorldToScreenPoint(Player.Instance.transform.position).z;
        switch (posType)
        {
            case E_PosType.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_PosType.TopCenter:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;

                initDir = Vector3.right;
                break;
            case E_PosType.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;

                initDir = Vector3.left;
                break;
            case E_PosType.CenterLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.up;
                break;
            case E_PosType.CenterRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;

                initDir = Vector3.up;
                break;
            case E_PosType.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_PosType.BottomCenter:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;

                initDir = Vector3.right;
                break;
            case E_PosType.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;

                initDir = Vector3.left;
                break;
        }
        //将屏幕上的点转为世界坐标系的点
        transform.position = playerCamera.ScreenToWorldPoint(screenPos);
    }

    /// <summary>
    /// 重置当前发射点数据
    /// </summary>
    private void ResetFirePointInfo()
    {
        //当子弹间隔时间和数量都为0时 才需要重新获取开火点数据
        if (nowDelay != 0 && nowNum != 0)
        {
            return;
        }
        if(firePointInfo != null)
        {
            nowCD -= Time.deltaTime;
            //下一轮子弹还在冷却
            if(nowCD>0)
            {
                return;
            }
        }

        List<FirePointInfo> firePointInfos = GameDataMgr.Instance.firePointData.firePoints;
        firePointInfo = firePointInfos[Random.Range(0, firePointInfos.Count)];
        nowNum = firePointInfo.num;
        nowDelay = firePointInfo.delay;
        nowCD = firePointInfo.cd;

        string[] strs = firePointInfo.ids.Split(',');
        int beginId = int.Parse(strs[0]);
        int endId = int.Parse(strs[1]);
        //因为id是从1开始的，而列表中的数据是从0开始的
        int randomBulletId = Random.Range(beginId, endId+1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletList[randomBulletId-1];

        if (firePointInfo.type == 2)
        {
            switch (posType)
            {
                case E_PosType.TopLeft:
                case E_PosType.TopRight:
                case E_PosType.BottomLeft:
                case E_PosType.BottomRight:
                    spacingAngle = 90f/(nowNum);
                    break;
                case E_PosType.CenterLeft:
                case E_PosType.TopCenter:
                case E_PosType.BottomCenter:
                case E_PosType.CenterRight:
                    spacingAngle = 180f / (nowNum );
                    break;
            }
        }
    }

    private void UpdateFirePoint()
    {
        if (nowDelay == 0 && nowNum == 0)
        {
            return;
        }

        nowDelay -= Time.deltaTime;
        if (nowDelay>0)
        {
            return;
        }
        GameObject bullet;
        Bullet bulletObj;
        switch (firePointInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                bulletObj = bullet.AddComponent<Bullet>();
                //传入当前子弹数据，进行初始化
                bulletObj.InitInfo(nowBulletInfo);
                //设置子弹位置和朝向
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(Player.Instance.transform.position - transform.position);
                //表示已经发射过一个子弹
                nowNum--;
                nowDelay = nowNum==0?0:firePointInfo.delay;
                break;
            case 2:
                if(nowDelay == 0)   //一瞬间发射所有散弹
                {
                    for (int i = 1; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                        bulletObj = bullet.AddComponent<Bullet>();
                        //传入当前子弹数据，进行初始化
                        bulletObj.InitInfo(nowBulletInfo);

                        bullet.transform.position = transform.position;
                        nowDir = Quaternion.AngleAxis(spacingAngle * i , Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    nowDelay = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resPath));
                    bulletObj = bullet.AddComponent<Bullet>();
                    //传入当前子弹数据，进行初始化
                    bulletObj.InitInfo(nowBulletInfo);

                    bullet.transform.position = transform.position;
                    nowDir = Quaternion.AngleAxis(spacingAngle * (firePointInfo.num-nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    nowNum--;
                    nowDelay = nowNum==0 ? 0 : firePointInfo.delay;
                }
                break;
        }
    }
}

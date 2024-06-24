using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //子弹使用的数据
    private BulletInfo info;

    private float time; //用于曲线移动的计时器

    // Start is called before the first frame update
    //void Start()
    //{
    //    InitInfo(GameDataMgr.Instance.bulletData.bulletList[0]);
    //}

    // Update is called once per frame
    void Update()
    {
        //共同点：朝自己面朝向移动
        transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);

        //1 直线运动
        //2 曲线运动
        //3 右抛物线运动
        //4 做抛物线运动
        //5 跟踪
        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                //sin值变化的快慢 决定了左右变化的频率
                //乘以速度 决定了变化的距离
                //roundSpeed 控制变化的频率
                transform.Translate(Vector3.right*info.rightSpeed* Mathf.Sin(time*info.roundSpeed) * Time.deltaTime);
                break;
            case 3:
                transform.rotation *= Quaternion.AngleAxis(info.roundSpeed*Time.deltaTime, Vector3.up);
                break;
            case 4:
                transform.rotation*=Quaternion.AngleAxis(-info.roundSpeed* Time.deltaTime, Vector3.up);
                break;
            case 5:
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(Player.Instance.transform.position-transform.position),info.roundSpeed*Time.deltaTime);
                break;
        }
    }

    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        //根据生命周期函数 延迟移除
        Invoke("DelayDestory", info.lifeTime);
    }

    private void DelayDestory()
    {
        Dead();
    }

    //销毁
    public void Dead()
    {
        //创建死亡特效
        GameObject eff = Instantiate(Resources.Load<GameObject>(info.deathEffResPath));
        eff.transform.position = transform.position;
        Destroy(eff, 1);

        //销毁子弹对象
        Destroy(gameObject);
    }

    //对角色造成伤害
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Wound();
            Dead();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoleSelPanel : PanelBase<RoleSelPanel>
{
    public UIButton leftBtn;
    public UIButton rightBtn;
    public UIButton startBtn;
    public UIButton exitBtn;

    //模型父对象
    public Transform airplanePos;

    //属性
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumnObjs;

    //飞机模型
    private GameObject airPlaneObj;

    private float time = 0;
    private bool isSel;     //是否选中飞机
    public Camera uiCamera;

    private void Update()
    {
        //飞机上下浮动
        time += Time.deltaTime;
        airplanePos.Translate(Vector3.up * Mathf.Sin(time) * 0.0001f, Space.World);

        //射线检测 让飞机可以旋转
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(uiCamera.ScreenPointToRay(Input.mousePosition),
                                1000,
                                1 << LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isSel=false;
        }
        if (Input.GetMouseButton(0) && isSel)
        {
            airplanePos.rotation *= Quaternion.AngleAxis(-Input.GetAxis("Mouse X") * 20, Vector3.up);
        }
    }

    public override void Init()
    {
        leftBtn.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowSelAirplaneId--;
            if (GameDataMgr.Instance.nowSelAirplaneId < 0)
            {
                GameDataMgr.Instance.nowSelAirplaneId = GameDataMgr.Instance.roleData.roleList.Count - 1;
            }
            ChangeNowAirplane();
        }));

        rightBtn.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowSelAirplaneId++;
            if (GameDataMgr.Instance.nowSelAirplaneId > GameDataMgr.Instance.roleData.roleList.Count - 1)
            {
                GameDataMgr.Instance.nowSelAirplaneId = 0;
            }
            ChangeNowAirplane();

        }));

        startBtn.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));

        exitBtn.onClick.Add(new EventDelegate(() =>
        {
            BeginPanel.Instance.ShowSelf();
            HideSelf();
        }));

        HideSelf();

    }

    public override void ShowSelf()
    {
        base.ShowSelf();
        GameDataMgr.Instance.nowSelAirplaneId = 0;
        ChangeNowAirplane();
    }

    public override void HideSelf()
    {
        base.HideSelf();
        //删除当前模型
        DestoryObj();
    }

    private void ChangeNowAirplane()
    {
        RoleInfo roleInfo = GameDataMgr.Instance.GetNowSelAirplaneInfo();
        //更新模型
        DestoryObj();

        airPlaneObj = Instantiate(Resources.Load<GameObject>(roleInfo.resPath));

        //更新角度、位置、缩放
        airPlaneObj.transform.SetParent(airplanePos);
        airPlaneObj.transform.localPosition = Vector3.zero;
        airPlaneObj.transform.localRotation = Quaternion.identity;
        airPlaneObj.transform.localScale = Vector3.one * roleInfo.scale;
        airPlaneObj.layer = LayerMask.NameToLayer("UI");

        //更新属性
        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < roleInfo.hp);
            speedObjs[i].SetActive(i < roleInfo.speed);
            volumnObjs[i].SetActive(i < roleInfo.volume);
        }

    }

    private void DestoryObj()
    {
        if(airPlaneObj!=null)
        {
            Destroy(airPlaneObj);
            airPlaneObj = null;
        }
    }

    
}

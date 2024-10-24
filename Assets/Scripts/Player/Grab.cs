using System.Collections;
using UnityEngine;

public class SpiderGrab : MonoBehaviour
{
    public Camera mainCamara;
    public LineRenderer lineRenderer;          //直线绘制组件
    public DistanceJoint2D distanceJoint;      //模拟物理连接组件，可以模拟绳锁，弹簧等效果
    public float intervalTime;

    private bool CanGrab;

    void Start()
    {
        distanceJoint.enabled = false;    //先禁用组件
        CanGrab = true;                   //能抓取
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanGrab)
        {   //将点击屏幕坐标转换为目标坐标
            Vector2 targetPosition = (Vector2)mainCamara.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, targetPosition);         //绘制蜘蛛线起始目标位置
            lineRenderer.SetPosition(1, transform.position);     //绘制蜘蛛线末尾位置
            distanceJoint.connectedAnchor = targetPosition;      //连接到锚点
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
            CanGrab = false;
            StartCoroutine(GrabInterval());     //让他等intervalTime个时间才能继续钩锁
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }

        if (distanceJoint.enabled)            //因为Input.GetKeyDown(KeyCode.Mouse0)只会进去一次
        {                                     //蜘蛛网要持续不断一直刷新。
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    IEnumerator GrabInterval()                //等待intervalTime，才能再一次钩锁
    {
        yield return new WaitForSeconds(intervalTime);
        CanGrab = true;
    }

}
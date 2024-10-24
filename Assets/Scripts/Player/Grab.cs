using System.Collections;
using UnityEngine;

public class SpiderGrab : MonoBehaviour
{
    public Camera mainCamara;
    public LineRenderer lineRenderer;          //ֱ�߻������
    public DistanceJoint2D distanceJoint;      //ģ�������������������ģ�����������ɵ�Ч��
    public float intervalTime;

    private bool CanGrab;

    void Start()
    {
        distanceJoint.enabled = false;    //�Ƚ������
        CanGrab = true;                   //��ץȡ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && CanGrab)
        {   //�������Ļ����ת��ΪĿ������
            Vector2 targetPosition = (Vector2)mainCamara.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, targetPosition);         //����֩������ʼĿ��λ��
            lineRenderer.SetPosition(1, transform.position);     //����֩����ĩβλ��
            distanceJoint.connectedAnchor = targetPosition;      //���ӵ�ê��
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
            CanGrab = false;
            StartCoroutine(GrabInterval());     //������intervalTime��ʱ����ܼ�������
        }

        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }

        if (distanceJoint.enabled)            //��ΪInput.GetKeyDown(KeyCode.Mouse0)ֻ���ȥһ��
        {                                     //֩����Ҫ��������һֱˢ�¡�
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    IEnumerator GrabInterval()                //�ȴ�intervalTime��������һ�ι���
    {
        yield return new WaitForSeconds(intervalTime);
        CanGrab = true;
    }

}
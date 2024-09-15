using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public float viewRange = 15.0f; //��ĳ������ ���� ���� �Ÿ� 
    [Range(0, 360)]
    public float viewAngle = 120f; //��ĳ������ �þ߰�
    [SerializeField] private Transform enemyTr;
    [SerializeField] private Transform playerTr;
    [SerializeField] private int playerLayer;
    [SerializeField] private int boxLayer;
    [SerializeField] private int layerMask;
    [SerializeField] private int barrelLayer;
    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        playerLayer = LayerMask.NameToLayer("PLAYER");
        boxLayer = LayerMask.NameToLayer("BOXES");
        barrelLayer = LayerMask.NameToLayer("BARREL");
        layerMask = 1 << playerLayer | 1 << boxLayer | 1 << barrelLayer;
    }
    public Vector3 CirclePoint(float angle) //������ �������� �˱����� 
    { 
        //���� ��ǥ�� �������� �����ϱ� ���ؼ� ��ĳ���� Yȸ������ ���� 
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f
            , Mathf.Cos(angle * Mathf.Deg2Rad));
        // �������� 1�� ���������� �����ϸ� X�� sin =x/1 �̶� ���� �����ϰ�
        //  ���� x =sin �̵ǰ� ���������� z�� cos  =z/1 �̱⿡ z= cos ����
        //Mathf.Deg2Rad�� ��ȯ��� �μ� (PI*2)/360 �� ����.
        //�Ϲ� ������ �������� ��ȯ
        //�׹ݴ뵵 �ִ�.Mathf.Rad2Deg ������ �Ϲݰ����� ��ȯ
    }
    public bool isTracePlayer() //�÷��̾ �����ؾ� �ϴ� �� �Ǵ� �Լ�
    {
        bool isTrace = false;
         // 15 �ݰ� �ȿ��� �� �����ǿ��� �÷��̾  �ִ����� ���� 
        Collider[] colls = Physics.OverlapSphere(enemyTr.position,
            viewRange, 1 << playerLayer);
        if(colls.Length ==1) //�÷��̾ ���� �ȿ� �ִٰ� �Ǵ�
        {
            // ��ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ��� �� 
            Vector3 dir= (playerTr.position - enemyTr.position).normalized;
            // �� ĳ������ �þ߰��� ���Դٰ� �Ǵ�
            if(Vector3.Angle(enemyTr.forward,dir) <viewAngle *0.5f)
            {
                isTrace = true;
            }
        }
        return isTrace;
    }
    public bool isViewPlayer() //�÷��̾ ���Ҵٸ� ���� �ϱ� ���� �Ǵ� �Լ� 
    {
        bool isView = false;
        RaycastHit hit;

        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        //����ĳ��Ʈ�� ��Ƽ� ��ֹ��� �ִ� �� �Ǵ�
        if(Physics.Raycast(enemyTr.position, dir, out hit,viewRange ,layerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }
        return isView;


    }
}

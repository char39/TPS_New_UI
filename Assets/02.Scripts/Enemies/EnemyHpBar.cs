using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*월드 좌표: 가상세계에 있는 캐릭터의 좌표를 받아와
 * -> 스크린좌표:
 * -> canvas좌표: 캔버스의 좌표로 변경*/

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] Camera uiCam;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectParent;  //UI_Canvas
    [SerializeField] RectTransform rectHP;
    public Vector3 offset = Vector3.zero;    //HPBar의 위치를 Enemy 위치에서 벗어나게 하기 위해 추가
    public Transform targetTr;

    void Start()
    {
        canvas = GameObject.Find("Canvas_UI_new").GetComponent<Canvas>();
        uiCam = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHP = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset); //월드좌표(x, y, z)에서 스크린좌표(x, y)로 변환할 때 z값도 같이 옴.

        if (screenPos.z < 0f)   //player가 카메라 뒤에 있다면
            screenPos.z *= -1.0f;

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCam, out localPos);
        rectHP.localPosition = localPos;    //HPBar 이미지의 위치를 변경
    }
}

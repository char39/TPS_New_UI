using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBarNew : MonoBehaviour
{
    [SerializeField] Camera uiCam;
    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform rectParent;
    [SerializeField] RectTransform rectHP;
    public Vector3 offset = Vector3.zero;
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
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        if (screenPos.z < 0f)
            screenPos.z *= -1.0f;

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCam, out localPos);
        rectHP.localPosition = localPos;
    }
}

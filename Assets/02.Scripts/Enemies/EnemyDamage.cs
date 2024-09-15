using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private readonly string bulletTag = "BULLET";
    [SerializeField] private GameObject bloodEffect;
    public float hp = 100f;

    public Vector3 hpbarOffset = new Vector3(0f, 2.2f, 0f);
    public Image hpbarImg;
    public GameObject hpbar;

    public Vector3 hpbarOffsetNew = new(0f, 2.2f, 0f);
    public Image hpbarImgNew;
    public GameObject hpbarNew;

    void OnEnable()
    {
        //StartCoroutine(SetHPBar());
        StartCoroutine(SetHPBarNew());
    }

    IEnumerator SetHPBar()
    {
        yield return new WaitForSeconds(1f);
        hpbar = ObjectPoolingManager.poolingManager.GetEnemyHPBar();

        hpbarImg = hpbar.GetComponentsInChildren<Image>()[1];
        var _hpBar = hpbar.GetComponent<EnemyHPBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpbarOffset;
        hpbar.gameObject.SetActive(true);
    }

    IEnumerator SetHPBarNew()
    {
        yield return new WaitForSeconds(1f);
        hpbarNew = ObjectPoolingManager.poolingManager.GetEnemyHPBarNew();

        hpbarImgNew = hpbarNew.GetComponentsInChildren<Image>()[1];
        var _hpBarNew = hpbarNew.GetComponent<EnemyHPBarNew>();
        _hpBarNew.targetTr = this.gameObject.transform;
        _hpBarNew.offset = hpbarOffsetNew;
        hpbarNew.SetActive(true);
    }

    void Start()
    {
        bloodEffect = Resources.Load("Effects/BulletImpactFleshBigEffect") as GameObject;
    }

    void OnDamage(object[] _params)
    {
        ShowBloodEffect((Vector3)_params[0]);
        hp -= (float)_params[1];
        hp = Mathf.Clamp(hp, 0f, 100f);
        hpbarImg.fillAmount = (float)hp / 100f;

        if (hp <= 0f)
        {
            Die();
            //hpbarImg.GetComponentsInChildren<Image>()[0].color = Color.clear;
            hpbarImgNew.GetComponentsInChildren<Image>()[0].color = Color.clear;
        }
    }

    void Die()
    {
        // Debug.Log("!");
        GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
        GameManager.G_instance.KillScore();
    }

    private void ShowBloodEffect(Vector3 col)
    {
        Vector3 pos = col;
        Vector3 _normal = col.normalized;
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, _normal);
        GameObject blood = Instantiate(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
}

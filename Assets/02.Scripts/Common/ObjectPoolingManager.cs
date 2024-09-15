using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager poolingManager;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject E_bulletPrefab;
    public int maxPool = 10;
    public List<GameObject> bulletPoolList;
    public List<GameObject> E_bulletPoolList;

    [Header("EnemyObjectPool")]
    public GameObject EnemyPrefab;
    public List<GameObject> EnemyPoolList;
    public List<Transform> SpawnPointList;

    [Header("EnemyHPBar")]
    public List<GameObject> EnemyHPBarList;
    [SerializeField] GameObject E_HPBar;

    public List<GameObject> EnemyHPBarNewList;
    [SerializeField] GameObject E_HPBarNew;

    void Awake() //Awake-> OnEnable-> Start
    {
        if (poolingManager == null)
            poolingManager = this;
        else if (poolingManager != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        bulletPrefab = Resources.Load("Bullet") as GameObject;
        E_bulletPrefab = Resources.Load("E_Bullet") as GameObject;
        EnemyPrefab = Resources.Load<GameObject>("Enemy");
        E_HPBar = Resources.Load<GameObject>("EnemyHpBar");
        E_HPBarNew = Resources.Load<GameObject>("EnemyHpBar_New");

        CreateBulletPool();
        CreateE_BulletPool();
        CreateEnemyPool();
        CreateE_HPBarPool();
        CreateE_HPBarNewPool();
    }
    private void Start()
    {
        var spawnPoint = GameObject.Find("SpawnPoints");
        if (spawnPoint != null)
            spawnPoint.GetComponentsInChildren<Transform>(SpawnPointList);

        SpawnPointList.RemoveAt(0);
        if (SpawnPointList.Count > 0)
            StartCoroutine(CreateEnemy());
    }


    private void CreateEnemyPool()
    {
        GameObject EnemyGroup = new GameObject("EnemyGroup");
        for (int i = 0; i < maxPool; i++)
        {
            var enemyObj = Instantiate(EnemyPrefab, EnemyGroup.transform);
            enemyObj.name = $"{(i + 1).ToString()} ";
            enemyObj.SetActive(false);
            EnemyPoolList.Add(enemyObj);
        }
    }

    void CreateBulletPool()
    {
        GameObject playerBulletGroup = new GameObject("PlayerBulletGroup");
        for (int i = 0; i < 10; i++)
        {
            var _bullet = Instantiate(bulletPrefab, playerBulletGroup.transform);
            _bullet.name = $"{(i + 1).ToString()} ";
            _bullet.SetActive(false);
            bulletPoolList.Add(_bullet);
        }

    }
    void CreateE_BulletPool()
    {
        GameObject EnemyBulletGroup = new GameObject("Enemy_BulletGroup");
        for (int i = 0; i < 20; i++)
        {
            var E_bullet = Instantiate(E_bulletPrefab, EnemyBulletGroup.transform);
            E_bullet.name = $"{(i + 1).ToString()} ";
            E_bullet.SetActive(false);
            E_bulletPoolList.Add(E_bullet);
        }

    }
    public GameObject GetBulletPool()
    {
        for (int i = 0; i < bulletPoolList.Count; i++)
        {
            //Ȱ Ǿٸ activeSelf Ȱȭ Ȱ θ ˷
            if (bulletPoolList[i].activeSelf == false)
            {
                return bulletPoolList[i];
            }
        }
        return null;
    }

    public GameObject E_GetBulletPool()
    {
        for (int i = 0; i < E_bulletPoolList.Count; i++)
        {
            if (E_bulletPoolList[i].activeSelf == false)
            {
                return E_bulletPoolList[i];
            }
        }
        return null;
    }

    void CreateE_HPBarPool()
    {
        Transform uiCanvas = GameObject.Find("Canvas_UI_new").transform;
        for (int i = 0; i < 10; i++)
        {
            var e_HPBar = Instantiate(E_HPBar, uiCanvas.transform);
            e_HPBar.name = $"{(i + 1).ToString()} 번째 HPBar";
            e_HPBar.SetActive(false);
            EnemyHPBarList.Add(e_HPBar);
        }
    }

    void CreateE_HPBarNewPool()
    {
        Transform uiCanvas = GameObject.Find("Canvas_UI_new").transform;
        for (int i = 0; i < 10; i++)
        {
            var e_HPBarNew = Instantiate(E_HPBarNew, uiCanvas.transform);
            e_HPBarNew.name = $"{(i + 1).ToString()} 번째 HPBarNew";
            e_HPBarNew.SetActive(false);
            EnemyHPBarNewList.Add(e_HPBarNew);
        }
    }

    public GameObject GetEnemyHPBar()
    {
        for (int i = 0; i < E_bulletPoolList.Count; i++)
        {
            if (EnemyHPBarList[i].activeSelf == false)
                return EnemyHPBarList[i];
        }
        return null;
    }

    public GameObject GetEnemyHPBarNew()
    {
        for (int i = 0; i < E_bulletPoolList.Count; i++)
        {
            if (EnemyHPBarNewList[i].activeSelf == false)
                return EnemyHPBarNewList[i];
        }
        return null;
    }

    IEnumerator CreateEnemy()
    {
        while (!GameManager.G_instance.isGameOver)
        {
            yield return new WaitForSeconds(3f);
            if (GameManager.G_instance.isGameOver) yield break;
            foreach (GameObject _enemy in EnemyPoolList)
            {
                if (_enemy.activeSelf == false)
                {
                    int idx = Random.Range(0, EnemyPoolList.Count - 1);
                    _enemy.transform.position = SpawnPointList[idx].position;
                    _enemy.transform.rotation = SpawnPointList[idx].rotation;
                    _enemy.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
}

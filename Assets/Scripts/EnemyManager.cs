using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    public float m_CreateDis = 8f;
    public float m_CreateQueueWidth = 4f;
    public float m_CreateQueueHeight = 1f;
    public float m_CreateInteStartVal = 2f;
    public float m_CreateInteGrowVal = 0.1f;
    public float m_MinCreateInte = 1f;
    public float m_CreateNumStartVal = 1f;
    public float m_CreateNumGrow = 0.5f;
    public int m_MaxCreateNum = 5;
    public List<GameObject> m_EmyPrefabs = new List<GameObject>();

    private List<Enemy> m_Enemies = new List<Enemy>();
    private float m_NextCreateTime;
    private float m_NextCreateInte;
    private float m_NextCreateNum;
    private const int MAXLISTCOUNT = 100;

    private void Start()
    {
        this.Restart();
        m_NextCreateTime = Global.instance.m_Time + m_NextCreateInte;
    }

    protected void OnEnable()
    {
        if (EnemyManager.instance == null)
        {
            EnemyManager.instance = this;
        }
        else if (EnemyManager.instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Restart()
    {
        m_NextCreateInte = m_CreateInteStartVal;
        m_NextCreateNum = m_CreateNumStartVal;

        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            this.RemoveEnemy(m_Enemies[i]);
        }
    }

    public void CreateEnemy()
    {
        // Debug.Log("CreateEnemy");
        m_NextCreateTime = Global.instance.m_Time + m_NextCreateInte;

        GameObject prefab = m_EmyPrefabs[
            Random.Range(0, m_EmyPrefabs.Count)
        ];
        DirType dir = (DirType)Random.Range(0, (int)DirType.SIZE);
        Vector3 pos = Global.instance.m_Role.transform.position;
        switch (dir)
        {
            case DirType.Up:
                {
                    pos += new Vector3(
                        Random.Range(-m_CreateDis, m_CreateDis),
                        -m_CreateDis,
                        0
                    );
                    pos += new Vector3(
                        Random.Range(-m_CreateQueueHeight / 2f, m_CreateQueueHeight / 2f),
                        Random.Range(-m_CreateQueueWidth / 2f, m_CreateQueueWidth / 2f),
                        0
                    );
                }
                break;
            case DirType.Down:
                {
                    pos += new Vector3(
                        Random.Range(-m_CreateDis, m_CreateDis),
                        m_CreateDis,
                        0
                    );
                    pos += new Vector3(
                        Random.Range(-m_CreateQueueHeight / 2f, m_CreateQueueHeight / 2f),
                        Random.Range(-m_CreateQueueWidth / 2f, m_CreateQueueWidth / 2f),
                        0
                    );
                }
                break;
            case DirType.Left:
                {
                    pos += new Vector3(
                        m_CreateDis,
                        m_CreateDis,
                        0
                    );
                    pos += new Vector3(
                        Random.Range(-m_CreateQueueWidth / 2f, m_CreateQueueWidth / 2f),
                        Random.Range(-m_CreateQueueHeight / 2f, m_CreateQueueHeight / 2f),
                        0
                    );
                }
                break;
            case DirType.Right:
                {
                    pos += new Vector3(
                        -m_CreateDis,
                        m_CreateDis,
                        0
                    );
                    pos += new Vector3(
                        Random.Range(-m_CreateQueueWidth / 2f, m_CreateQueueWidth / 2f),
                        Random.Range(-m_CreateQueueHeight / 2f, m_CreateQueueHeight / 2f),
                        0
                    );
                }
                break;
        }

        int num = Mathf.FloorToInt(m_NextCreateNum);
        num = Random.Range(1, num + 1);
        Debug.Log("Create num = " + num);
        for (int i = 0; i < num; i++)
        {
            GameObject createdGO = Instantiate(prefab,
                pos,
                Quaternion.identity);
            // Debug.Log("Prefab : " + prefab.name + "\n" +
            //     "Pos : " + pos);
            createdGO.transform.SetParent(transform, true);
            Enemy createdCO = createdGO.GetComponent<Enemy>();
            createdCO.Init(dir);
            m_Enemies.Add(createdCO);
        }

        m_NextCreateInte -= m_CreateInteGrowVal;
        m_NextCreateInte = Mathf.Clamp(m_NextCreateInte, 
            m_MinCreateInte, m_CreateInteStartVal);
        m_NextCreateNum += m_CreateNumGrow;
        m_NextCreateNum = Mathf.Clamp(m_NextCreateNum, 
            1, m_MaxCreateNum);
    }

    public void RemoveEnemy(Enemy emy)
    {
        if (m_Enemies.Contains(emy))
        {
            m_Enemies.Remove(emy);

            Destroy(emy.gameObject);
        }
    }

    public void MyUpdate()
    {
        this.AutoRemove();

        foreach (Enemy oneEmy in m_Enemies)
        {
            oneEmy.MyUpdate();
        }

        if (m_NextCreateTime <= Global.instance.m_Time)
        {
            this.CreateEnemy();
        }
    }

    private void AutoRemove()
    {
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            if (m_Enemies[i].m_Die)
                this.RemoveEnemy(m_Enemies[i]);
        }
        // while (m_Enemies.Count > MAXLISTCOUNT)
        // {
        //     this.RemoveEnemy(m_Enemies[0]);
        // }
    }
}
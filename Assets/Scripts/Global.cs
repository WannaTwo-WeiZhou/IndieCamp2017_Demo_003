using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    public static Global instance { get; private set; }

	public Role m_Role;
    public float m_DeltaTime = 0.05f;
	[HideInInspector]
    public float m_Time = 0f;

    protected void OnEnable()
    {
        if (Global.instance == null)
        {
            Global.instance = this;
        }
        else if (Global.instance != this)
        {
            Destroy(gameObject);
        }
    }

    // private void Start()
    // {
	// 	InvokeRepeating("MyUpdate", 0f, m_DeltaTime);
    // }

	public void MyUpdate()
	{
		m_Time += m_DeltaTime;
        // Debug.Log("Time : " + m_Time);

		// enemy manager
		EnemyManager.instance.MyUpdate();

		// role
		m_Role.MyUpdate();
	}

    public void RestartGame()
    {	
		m_Time = 0f;
		EnemyManager.instance.Restart();

        SceneManager.LoadScene(0);
    }
}

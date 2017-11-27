using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirType
{
    Up = 0,
    Left = 1,
    Down = 2,
    Right = 3,

    SIZE
};

public class Role : MonoBehaviour
{
    public float m_MoveVec = 1f;

    public void MyUpdate()
    {

    }

    public void Shoot()
    {
        
    }

    public void Move(Vector2 vec)
    {
        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            if (vec.x > 0)
            {
                vec = Vector2.right;
            }
            else
            {
                vec = Vector2.left;
            }
        }
        else
        {
            if (vec.y > 0)
            {
                vec = Vector2.up;
            }
            else
            {
                vec = Vector2.down;
            }
        }

        transform.Translate(m_MoveVec * vec * Global.instance.m_DeltaTime,
            Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy emy = other.GetComponent<Enemy>();
        if (emy != null)
        {
            EnemyManager.instance.RemoveEnemy(emy);
            this.GetHurt();
        }
    }

    private void GetHurt()
    {
        // add time delay


        Global.instance.RestartGame();
    }
}

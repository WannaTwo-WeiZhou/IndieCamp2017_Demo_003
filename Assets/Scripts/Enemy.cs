using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float m_MoveVec = 1f;
    public DirType m_Dir = DirType.Left;
    public float m_RemoveDelay = 20f;
    [HideInInspector]
    public bool m_Die = false;

    public void Init(DirType dir)
    {
        m_Die = false;
        m_Dir = dir;

        Vector2 vec = Vector2.zero;
        switch (m_Dir)
        {
            case DirType.Up:
                {
                    vec = Vector2.up;
                }
                break;
            case DirType.Down:
                {
                    vec = Vector2.down;
                }
                break;
            case DirType.Left:
                {
                    vec = Vector2.left;
                }
                break;
            case DirType.Right:
                {
                    vec = Vector2.right;
                }
                break;
        }

        float ang = Vector2.Angle(vec, Vector2.up);
        if (vec.x > 0)
        {
            ang = -ang;
        }

        transform.SetLocalEulerAngles_Z(ang);

        m_RemoveDelay += Global.instance.m_Time;
    }

    public void MyUpdate()
    {
        this.Move();

        if (Global.instance.m_Time >= m_RemoveDelay)
        {
            m_Die = true;
        }
    }

    public void Move()
    {
        Vector2 vec = Vector2.up;
        // switch (m_Dir)
        // {
        //     case DirType.Up:
        //         {
        //             vec = Vector2.up;
        //         }
        //         break;
        //     case DirType.Down:
        //         {
        //             vec = Vector2.down;
        //         }
        //         break;
        //     case DirType.Left:
        //         {
        //             vec = Vector2.left;
        //         }
        //         break;
        //     case DirType.Right:
        //         {
        //             vec = Vector2.right;
        //         }
        //         break;
        // }

        vec *= m_MoveVec;
        // vec += Global.instance.m_Role.m_MoveVec * Vector2.up;

        transform.Translate(vec * Global.instance.m_DeltaTime,
            Space.Self);
    }

}
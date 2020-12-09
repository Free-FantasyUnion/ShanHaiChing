using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Player player;


    private void Awake()
    {
        GameManager.instance = this;
    }

    public Vector3 playerPos;

    public static bool AttackJudge(Transform point, float attackRadius, float attackAngel, LayerMask attackLayer, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(point.position, attackRadius, attackLayer);
        bool tmp = targets != null;
        if (tmp)
        {
            List<Transform> judgeList = new List<Transform>();
            foreach (Collider2D target in targets)
            {
                //TODO: Check the vector3.right is correct
                if (CaculateAngel(point.right, target.transform.position - point.position) < attackAngel)
                {
                    if (attackLayer == LayerMask.NameToLayer("Enemy"))
                    {
                        target.GetComponent<EnemyBase>().Hurt(damage);
                    }
                    else if (attackLayer == LayerMask.NameToLayer("Player"))
                    {
                        target.GetComponent<Player>().Hurt(damage);
                    }
                }
            }
        }

        return tmp;
    }

    public void setPlayerPos(Vector3 v)
    {
        playerPos = v;
    }

    /// <summary>
    /// 计算2个向量的夹角
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="joinedLine"></param>
    /// <returns></returns>
    static float CaculateAngel(Vector3 direction, Vector3 joinedLine)
    {
        return Mathf.Acos(Vector3.Dot(direction.normalized, joinedLine.normalized)) * Mathf.Rad2Deg;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public enum Key
    {
        Attack = KeyCode.J,
        Up = KeyCode.W,
        Down = KeyCode.S,
        Left = KeyCode.A,
        Right = KeyCode.D,
        // 快速移动未确定
        QuickMove = KeyCode.LeftShift,
        Dialog = KeyCode.Space,
    }

}


// 这里是全局的攻击判定函数

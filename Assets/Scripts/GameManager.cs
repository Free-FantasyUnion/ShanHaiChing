using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 测试完了自己的代码之后把debug.log()和print给注释掉

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Player player;
    private GameObject 元气实体;
    public Vector3 playerPos;


    private void Awake()
    {
        GameManager.instance = this;
        instance.player = transform.Find("/Player").GetComponent<Player>();
        instance.元气实体 = Resources.Load<GameObject>("Prefabs/元气");
    }

    public static bool AttackJudge(Transform point, float attackRadius, float attackAngel, LayerMask attackLayer, float damage)
    {
        Collider[] targets = Physics.OverlapSphere(point.position, attackRadius, attackLayer);
        bool tmp = targets.Length != 0;
        if (tmp)
        {
            foreach (Collider target in targets)
            {
                //TODO: Check the vector3.right is correct//目前为止未出现错误
/*                if (Mathf.Abs(CaculateAngel(point.right, target.transform.position - point.position)) < attackAngel)
                {*/
                    if (target.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                    //print("Enemy Hurt!");
                    Wolf tmpWolf = target.gameObject.GetComponent<Wolf>();
                    if (tmpWolf!=null)
                    {
                        tmpWolf.isInterrupted = true;
                    }
                        target.GetComponent<EnemyBase>().Hurt(damage);
                    }
                    else if (target.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        //print("Player Under Attack!");
                        target.GetComponent<Player>().Hurt(damage);
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
        Vector3 tmp = new Vector3(joinedLine.x, 0, joinedLine.z);
        return Mathf.Acos(Vector3.Dot(direction.normalized, tmp.normalized)) * Mathf.Rad2Deg;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public static void 生成元气物体(Transform tf, float value)
    {
        GameObject tmp= Instantiate(instance.元气实体, tf);
        tmp.GetComponent<YuanQiObject>().yuanQiValue = value;
        tmp.GetComponent<YuanQiObject>().player = instance.player;
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
        Pause = KeyCode.Escape
    }

}


// 这里是全局的攻击判定函数

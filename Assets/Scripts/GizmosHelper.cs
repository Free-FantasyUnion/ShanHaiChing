using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosHelper : MonoBehaviour
{
    public Transform judgePoint;
    public float attackRadius;
    public Transform NPC;
    public float NPCRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(judgePoint.position, attackRadius);
        Gizmos.DrawWireSphere(NPC.position, NPCRadius);
    }
}

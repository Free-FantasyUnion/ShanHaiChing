using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyRushPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> rushEnemy;
    [SerializeField] private float radius;
    [SerializeField] private Player player;
    [SerializeField] private GameObject bullet;//TODO: 如果生成蛇的话,就给蛇的bullet赋值这个(已加载)

    private void Start()
    {
        player = GameManager.GetInstance().player;
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
    }
    public void makeEnemy()
    {
        List<Vector3> poss = calculatePosition();
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < rushEnemy.Count; ++i)
        {
            var tmp = Instantiate(rushEnemy[i]);
            tmp.GetComponent<EnemyBase>().player = this.player;
            tmp.name = this.name + i;
            tmp.transform.position = transform.position + poss[i];
        }

    }

    public List<Vector3> calculatePosition()
    {
        Vector3 toPlayerPos = GameManager.GetInstance().playerPos - transform.position;
        toPlayerPos.z = 0;
        toPlayerPos = Vector3.Normalize(toPlayerPos);
        List<Vector3> targetPos = new List<Vector3>();
        if (rushEnemy.Count == 1)
        {
            targetPos.Add(toPlayerPos * radius);
            // print("rush" + targetPos[0]);
        }
        if (rushEnemy.Count == 2)
        {
            //print(toPlayerPos);
            //print(Quaternion.AngleAxis(60, Vector3.forward) * toPlayerPos);
            //print(Quaternion.AngleAxis(-60, Vector3.forward) * toPlayerPos);
            targetPos.Add(new Vector3(1,0,0) * radius);
            targetPos.Add(new Vector3(-1,0,0) * radius);
        }
        if (rushEnemy.Count == 3)
        {
            targetPos.Add(new Vector3(1, 0, 0) * radius);
            targetPos.Add(new Vector3(0, 0, 0));
            targetPos.Add(new Vector3(-1, 0, 0) * radius);
        }
        return targetPos;
    }
}

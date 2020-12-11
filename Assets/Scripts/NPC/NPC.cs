using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sprite))]
public class NPC : MonoBehaviour
{

    [SerializeField] private int scriptsIndex;
    [SerializeField] private float radius = 5f;

    private float calcDistance(Vector3 playerPos)
    {
        Vector3 toCurrentAxis = new Vector3(playerPos.x, playerPos.z * Mathf.Tan(Mathf.PI / 12) + 1.5f, 0);
        return Vector3.Distance(toCurrentAxis, transform.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)GameManager.Key.Dialog))
        //TODO : 设置判定范围//貌似已经设置好了
        {
            print(calcDistance(GameManager.GetInstance().player.transform.position));
            if (calcDistance(GameManager.GetInstance().player.transform.position) <= radius)
            {
                NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);
            }
            /*if (Vector3.Distance(PlayerManager.GetInstance().playerTF.position, this.transform.position) <= radius)
                NPCManager.GetInstance().ReseBonseToNPC(scriptsIndex);*/
        }
    }
}

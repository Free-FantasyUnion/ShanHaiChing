using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance = new PlayerManager();
    public Transform playerTF;
    private PlayerManager()
    {

    }

    void Start()
    {
        instance.playerTF = GameObject.FindGameObjectWithTag("Player").transform;
        print(instance.playerTF);
    }


    void Update()
    {

    }
    public static PlayerManager GetInstance()
    {
        return instance;
    }



}

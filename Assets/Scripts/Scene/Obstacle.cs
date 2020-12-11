using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Player player;

    public float isMovable()
    {

        return 1.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        //player.transform.Find("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

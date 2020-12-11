using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float xMargin = 1f; 
    public float yMargin = 1f; 
    public float xSmooth = 2f; 
    public float ySmooth = 2f; 
    public Vector2 maxXAndY; 
    public Vector2 minXAndY; 
    public float yOffset = 14f;

    private Transform m_Player; 


    private void Awake()
    {
        m_Player = transform.Find("/Player").transform;
    }


    private bool CheckXMargin()
    {        
        return Mathf.Abs(transform.position.x - m_Player.position.x) > xMargin;
    }


    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - zPosToy(m_Player.position.z)) > yMargin;
    }


    private void Update()
    {
        TrackPlayer();
    }

    private float zPosToy(float z)
    {
        return z * Mathf.Tan(Mathf.PI / 12.0f)+yOffset;
    }


    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, xSmooth * Time.deltaTime);
        }
        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, zPosToy(m_Player.transform.position.z), ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    public void setMaxMoveX(float maxX)
    {
        maxXAndY.x = maxX;
    }
}

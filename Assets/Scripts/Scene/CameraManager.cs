using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float xMargin = 1f; // Distance in the x axis the player can move before the camera follows.
    public float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.
    public float xSmooth = 2f; // How smoothly the camera catches up with it's target movement in the x axis.
    public float ySmooth = 2f; // How smoothly the camera catches up with it's target movement in the y axis.
    public Vector2 maxXAndY; // The maximum x and y coordinates the camera can have.
    public Vector2 minXAndY; // The minimum x and y coordinates the camera can have.
    public float yOffset = 14f;

    private Transform m_Player; // Reference to the player's transform.


    private void Awake()
    {
        // Setting up the reference.
        m_Player = transform.Find("/Player").transform;
    }


    private bool CheckXMargin()
    {
        // Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
        return Mathf.Abs(transform.position.x - m_Player.position.x) > xMargin;
    }


    private bool CheckYMargin()
    {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
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
            print("checked " + m_Player.transform.position.z + " to " + zPosToy(m_Player.transform.position.z));
            targetY = Mathf.Lerp(transform.position.y, zPosToy(m_Player.transform.position.z), ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        // Set the camera's position to the target position with the same z component.
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}

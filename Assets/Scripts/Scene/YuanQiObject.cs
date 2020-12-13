using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YuanQiObject : MonoBehaviour
{
    [SerializeField] private Sprite yuanQiSprite;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float followDistance;
    [SerializeField] private float absorbDistance;
    public float yuanQiValue;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = transform.Find("/Player").GetComponent<Player>();
        maxSpeed = 3.0f;
        followDistance = 10.0f;
        absorbDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = this.gameObject.transform.position - player.transform.position;
        if (temp.magnitude <= absorbDistance)
        {
            GiveYuanQi();
        }
        if (temp.magnitude <= followDistance)
        {
            this.gameObject.transform.Translate(Vector3.ClampMagnitude(-temp, this.maxSpeed * Time.deltaTime));
        }


    }

    public YuanQiObject(float value)
    {
        this.yuanQiValue = value;
    }
    private void GiveYuanQi()
    {
        player.GetYuanQi(this.yuanQiValue);
        MusicManager.PlayMusic(MusicManager.absorbGenki);
        Destroy(this.gameObject);
    }
}

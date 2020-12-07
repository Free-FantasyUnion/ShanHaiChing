using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentTarget : MonoBehaviour
{
    private SpriteRenderer render;
    private Color _color;
    [SerializeField] private float appearTime;
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        _color = render.color;
        _color.a = 0;
        render.color = _color;
    }

    void Update()
    {
        if (_color.a <= 255)
        {
            _color.a += 255 * Time.deltaTime / appearTime;
            render.color = _color;
        }
        
    }
}

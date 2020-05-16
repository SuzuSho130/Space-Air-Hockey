using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuckControllor : PuckControllor
{
    private float max_life = 20.0f;
    private float life;
    public Vector3 _pool_pos = new Vector3(1000f, 1000f, 1000f);

    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.zero;
        life = max_life;
        field_size = new Vector2 (28f, 66f);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Reset();
        }
    }

    public void Reset()
    {
        transform.position = _pool_pos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

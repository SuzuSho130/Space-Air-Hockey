using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuckControllor : PuckControllor
{
    private float max_life = 20.0f;
    private float life;
    public Vector3 _pool_pos = new Vector3(1000, 1000, 1000);

    public void Init(Vector3 position)
    {
        transform.position = position;
        life = max_life;
        field_size = new Vector2 (280f, 660f);
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

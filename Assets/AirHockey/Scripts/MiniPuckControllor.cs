using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuckControllor : PuckControllor
{
    public float life = 20;

    // Update is called once per frame
    void Update()
    {
        Move();
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}

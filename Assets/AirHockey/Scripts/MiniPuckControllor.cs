using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPuckControllor : PuckControllor
{
    private float life = 20.0f;

    public void Init(float max_life)
    {
        life = max_life;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        life -= Time.deltaTime;
        if (life <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

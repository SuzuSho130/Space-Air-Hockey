using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckControllor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -135, 135);
        player_pos.z = Mathf.Clamp(player_pos.z, -310, 310);
        transform.position = player_pos;
    }
}

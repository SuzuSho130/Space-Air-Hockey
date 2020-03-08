using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckControllor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -140, 140);
        player_pos.z = Mathf.Clamp(player_pos.z, -330, 330);
        transform.position = player_pos;
    }
}

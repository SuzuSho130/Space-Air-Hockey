using UnityEngine;

/*
packの動きを制御するスクリプト
*/

public class PuckControllor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -140, 140);
        player_pos.z = Mathf.Clamp(player_pos.z, -330, 330);
        player_pos.y = 0;
        transform.position = player_pos;
    }
}

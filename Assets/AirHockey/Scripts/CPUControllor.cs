using UnityEngine;

/*
対戦相手となるCPUを動かすスクリプト
*/

public class CPUControllor : MonoBehaviour
{
    private Vector3 position;
    private Rigidbody _rb;
    public float speed = 0.5f;  // Strikerの移動速度
    public GameObject Puck;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        /* packの位置にx軸を合わせるようにstrikerを移動させる */
        Vector3 pos = new Vector3(Puck.transform.position.x * speed, 0f, transform.position.z); 
        _rb.MovePosition(pos);
        /* 台内に収まるようにStrikerの移動を制限 */
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -135, 135);
        player_pos.z = Mathf.Clamp(player_pos.z, 0, 300);
        transform.position = player_pos;
    }
}

using UnityEngine;

/*
packの動きを制御するスクリプト
*/

public class PuckControllor : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float max_speed = 1000f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb.velocity.magnitude > max_speed)
        {
            SpeedLimit();
            Debug.Log(_rb.velocity);
        }
        Move();
    }

    void SpeedLimit()
    {
        var vec = _rb.velocity;
        _rb.velocity = Vector3.ClampMagnitude(vec, max_speed);
    }

    public void Move()
    {
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -140, 140);
        player_pos.z = Mathf.Clamp(player_pos.z, -330, 330);
        player_pos.y = 0;
        transform.position = player_pos;
    }
}

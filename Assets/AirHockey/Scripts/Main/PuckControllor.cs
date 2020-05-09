using UnityEngine;

/*
packの動きを制御するスクリプト
*/

public class PuckControllor : MonoBehaviour
{
    protected Rigidbody _rb;
    public GameObject hit_effect;
    [SerializeField] float max_speed = 1000f;
    [SerializeField] protected Vector2 field_size;
    public bool side;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public Vector3 GetSpeed()
    {
        return _rb.velocity;
    }

    public float GetMaxSpeed()
    {
        return max_speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.velocity.magnitude > max_speed)
        {
            SpeedLimit();
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
        player_pos.x = Mathf.Clamp(player_pos.x, -field_size.x / 2f, field_size.x / 2f);
        player_pos.z = Mathf.Clamp(player_pos.z, -field_size.y / 2, field_size.y / 2f);
        player_pos.y = 0;
        transform.position = player_pos;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Player")
        {
            side = false;
        }
        else if (other.gameObject.tag=="Enemy")
        {
            side = true;
        }
        // Vector3 hit_pos;
        foreach(ContactPoint contact in other.contacts)
        {
            Instantiate(hit_effect, contact.point, Quaternion.identity);
        }
    }

    public float GetDirectionZ()
    {
        return _rb.velocity.z;
    }
}

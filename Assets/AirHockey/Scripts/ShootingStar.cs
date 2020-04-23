using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{

    public GameObject _mini_puck_prefab;
    Rigidbody _rb;
    private Vector2 _field_size;
    [SerializeField] private Vector3 _target_pos;
    [SerializeField] private float _speed;

    void Start(){Init(new Vector2(24, 50), 2.0f);}

    // Start is called before the first frame update
    public void Init(Vector2 field_size, float speed)
    {
        _rb = GetComponent<Rigidbody>();
        _speed = speed;
        _field_size = field_size;
        _target_pos = new Vector3(Random.Range(-_field_size.x / 2.0f, _field_size.x / 2.0f), 0.0f, Random.Range(-_field_size.y / 2.0f, _field_size.y / 2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = (_target_pos - transform.position).normalized;
        Debug.Log(direction);
        _rb.AddForce(direction * _speed, ForceMode.Force);
    }
}

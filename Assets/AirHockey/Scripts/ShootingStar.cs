using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{

    public GameObject _burst_particle;
    Rigidbody _rb;
    Vector3 _pool_pos = new Vector3(1000f, 1000f, 1000f);
    [SerializeField] private float _speed = 0.1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    public void Init()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y<=1)
        {
            Burst();
        }
    }

    private void Move()
    {
        var pos = transform.position;
        pos.x += _speed;
        pos.y -= _speed;
        _rb.MovePosition(pos);
    }

    private void Burst()
    {
        Instantiate(_burst_particle, transform.position, Quaternion.identity);
        GameObject.Find("ShootingStarManager").GetComponent<ShootingStarManager>().Birth(transform.position);
        Reset();
    }

    public void Reset()
    {
        transform.position = _pool_pos;
        gameObject.SetActive(false);
    }
}

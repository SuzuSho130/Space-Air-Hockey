using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : MonoBehaviour
{

    GameObject _star;
    public GameObject _burst_particle;
    Rigidbody _rb;
    Vector3 _pool_pos = new Vector3(1000, 1000, 1000);
    [SerializeField] private float _speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    public void Init(float speed, GameObject star)
    {
        _speed = speed;
        _star = star;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y<=1)
        {
            Birth();
        }
    }

    private void Move()
    {
        var pos = transform.position;
        pos.x += _speed;
        pos.y -= _speed;
        _rb.MovePosition(pos);
    }

    private void Birth()
    {
        Instantiate(_burst_particle, transform.position, Quaternion.identity);
        _star.GetComponent<MiniPuckControllor>().Init(transform.position);
        Reset();
    }

    public void Reset()
    {
        transform.position = _pool_pos;
        gameObject.SetActive(false);
    }
}

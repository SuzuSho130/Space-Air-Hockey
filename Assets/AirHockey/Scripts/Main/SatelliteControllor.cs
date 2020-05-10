using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteControllor : MonoBehaviour
{

    Rigidbody _rb;
    [SerializeField] private float v_x = 1f;
    [SerializeField] private float v_z = 1f;

    private bool _side;
    private bool free = true;
    private int direction;

    public Vector3 _pool_pos = new Vector3(1000f, 1000f, 1000f);

    public SatelliteSearcher satellite_seacher;

    public void Start()
    {
        Init();
    }

    // Start is called before the first frame update
    public void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Hit(bool side)
    {
        _rb.velocity = Vector3.zero;
        _side = side;
        if (_side)
        {
            direction = -1;
            transform.Rotate(new Vector3(0f, 180f, 0f));
            transform.position = new Vector3(Random.Range(-15f, 15f), 1f, Random.Range(0f, 30f));
        }
        else
        {
            direction = 1;
            transform.position = new Vector3(Random.Range(-15f, 15f), 1f, Random.Range(-30f, 0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (satellite_seacher == null || free)
        {
        }
        else if (satellite_seacher.puck_list.Count > 0)
        {
            Move(satellite_seacher.puck_list[0].position);
        }
        float current_z = transform.position.z;
        if (current_z * direction > 0 && free != true)
        {
            Reset();
        }
    }

    private void Move(Vector3 target_position)
    {
        if (target_position.x > transform.position.x)
        {
            _rb.velocity = new Vector3(v_x, 0f, v_z * direction) * Time.deltaTime;
        }
        else
        {
            _rb.velocity = new Vector3(-v_x, 0f, v_z * direction) * Time.deltaTime;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Puck" || other.gameObject.tag == "MiniPuck")
        {
            if (free)
            {
                free = false;
                Hit(other.gameObject.GetComponent<PuckControllor>().side);
            }
            else
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        free = true;
        transform.position = _pool_pos;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public class SatelliteControllor : MonoBehaviour
{

    Rigidbody _rb;
    [SerializeField] private float v_x = 1f;
    [SerializeField] private float v_z = 1f;

    private bool _side;
    private bool free = true;
    private int direction;
    private float limit = 10f;
    private bool attack = true;
    private float life = 10f;

    private Vector3 init_pos;

    public Vector3 _pool_pos = new Vector3(1000f, 1000f, 1000f);

    public SatelliteSearcher satellite_seacher;

    // Start is called before the first frame update
    public void Init()
    {
        GetComponent<Collider>().isTrigger = true;
        life = 10f;
        _rb = GetComponent<Rigidbody>();
    }

    public void Hit(bool side)
    {
        GetComponent<Collider>().isTrigger = false;
        _rb.velocity = Vector3.zero;
        _side = side;
        if (_side)
        {
            CPUControllor c = GameObject.Find("StrikerCPU").GetComponent<CPUControllor>();
            if (c.has_satellite)
            {
                Debug.Log("E");
                life += 10f;
                Leave();
            }
            else
            {
                c.has_satellite = true;
                direction = -1;
                transform.Rotate(new Vector3(0f, 180f, 0f));
                transform.position = new Vector3(0f, 0f, 25);
                init_pos = transform.position;
            }
        }
        else
        {
            StrikerController s = GameObject.Find("StrikerPlayer").GetComponent<StrikerController>();
            if (s.has_satellite)
            {
                Debug.Log("Player");
                life += 10f;
                Leave();
            }
            else
            {
                s.has_satellite = true;
                direction = 1;
                transform.position = new Vector3(0f, 0f, -25f);
                init_pos = transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!free)
        {
            if ((transform.position - init_pos).magnitude < 3f)
            {
                _rb.velocity = Vector3.zero;
                attack = true;
            }
            if ((transform.position - init_pos).magnitude > limit)
            {
                attack = false;
            }
            if (satellite_seacher != null && satellite_seacher.puck_list.Count > 0)
            {
                if (attack)
                {
                    AttackMove(satellite_seacher.puck_list[0].position);
                }
            }
            if (!attack)
            {
                ReturnMove();
            }
            life -= Time.deltaTime;
        }
        if (life <= 0f)
        {
            Reset();
        }
    }

    private void AttackMove(Vector3 target_position)
    {
        if (target_position.x > transform.position.x)
        {
            _rb.velocity = new Vector3(v_x, 0f, v_z * direction);
        }
        else
        {
            _rb.velocity = new Vector3(-v_x, 0f, v_z * direction);
        }
    }

    private void ReturnMove()
    {
        if (init_pos.x > transform.position.x)
        {
            _rb.velocity = new Vector3(v_x, 0f, -v_z * direction);
        }
        else
        {
            _rb.velocity = new Vector3(-v_x, 0f, -v_z * direction);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puck" || other.gameObject.tag == "MiniPuck")
        {
            if (free)
            {
                free = false;
                Hit(other.gameObject.GetComponent<PuckControllor>().side);
            }
        }
    }

    private void Reset()
    {
        Leave();
        if (_side)
        {
            GameObject.Find("StrikerCPU").GetComponent<CPUControllor>().has_satellite = false;
        }
        else
        {
            GameObject.Find("StrikerPlayer").GetComponent<StrikerController>().has_satellite = false;
        }
    }

    private void Leave()
    {
        free = true;
        transform.position = _pool_pos;
        _rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}

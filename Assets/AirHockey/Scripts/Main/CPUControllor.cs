using UnityEngine;

/*
対戦相手となるCPUを動かすスクリプト
*/

public class CPUControllor : MonoBehaviour
{
    private Vector3 init_position;
    private Rigidbody _rb;
    [SerializeField] Vector2 field_size;
    [SerializeField] private float x_speed = 0.5f;  // Strikerの移動速度
    [SerializeField] private float z_speed = 10f;  // Strikerの移動速度
    [SerializeField] private float max_speed = 1f;  // Strikerの移動速度
    [SerializeField] float sp = 100f;
    public GameObject Puck;
    GameObject[] mini_puck_list;
    int[] count_mini_puck = {0, 0, 0, 0, 0, 0};

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        init_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // StateTransition();
        // _rb.velocity = new Vector3(0, 0, -sp);
        mini_puck_list = GameObject.FindGameObjectsWithTag("MiniPuck");
        Debug.Log(mini_puck_list.Length);
    }

    private void CountMiniPuck()
    {
        foreach (var mini_puck in mini_puck_list)
        {

        }
    }

    private void StateTransition()
    {
        if (Puck.transform.position.z <= 0)
        {
            ReturnMove();
        }
        else
        {
            if (Puck.transform.position.z - transform.position.z <= 0)
            {
                AttackMove();
            }
            else
            {
                DeffenceMove();
            }
        }
        PositionRestriction();
    }

    private void AttackMove()
    {
        float temp_x;
        float temp_z;
        if (Puck.transform.position.z - transform.position.z < 100)
        {
            temp_x = 1.0f;
            temp_z = z_speed;
        }
        else
        {
            temp_x = 2.0f;
            temp_z = z_speed / 4;   
        }
        /*  */
        // Vector3 pos = new Vector3(Puck.transform.position.x * x_speed * temp_x, 0f, transform.position.z - temp_z);
        Vector3 pos = new Vector3(Puck.transform.position.x, 0f, transform.position.z);
        pos = SpeedLimit(pos);
        _rb.MovePosition(transform.position + pos * Time.deltaTime);
    }

    private void DeffenceMove()
    {
        float temp_z = transform.position.z;
        temp_z += z_speed;
        if (temp_z > init_position.z)
        {
            temp_z = init_position.z;
        }
        /*  */
        Vector3 pos = new Vector3(Puck.transform.position.x * x_speed, 0f, temp_z);
        pos = SpeedLimit(pos);
        _rb.MovePosition(transform.position + pos * Time.deltaTime);
    }

    private void ReturnMove()
    {
        float temp_z = transform.position.z;
        temp_z += z_speed/2;
        if (temp_z > init_position.z)
        {
            temp_z = init_position.z;
        }
        /*  */
        Vector3 pos = new Vector3(Puck.transform.position.x * x_speed, 0f, temp_z);
        pos = SpeedLimit(pos);
        _rb.MovePosition(transform.position + pos * Time.deltaTime);
    }

    private Vector3 SpeedLimit(Vector3 pos)
    {
        if (pos.magnitude > max_speed)
        {
            return Vector3.ClampMagnitude(pos, max_speed);
        }
        else
        {
            return pos;
        }
    }

    /* 台内に収まるようにStrikerの移動を制限 */
    private void PositionRestriction()
    {
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -field_size.x / 2f, field_size.x / 2f);
        player_pos.z = Mathf.Clamp(player_pos.z, 0, field_size.y / 2f);
        transform.position = player_pos;
    }

    public void ResetPosition()
    {
        transform.position = init_position;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Puck")
        {
            sp = 0;
            _rb.velocity = Vector3.zero;
        }
    }
}

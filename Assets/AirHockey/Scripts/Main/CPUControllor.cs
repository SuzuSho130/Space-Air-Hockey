using UnityEngine;

/*
対戦相手となるCPUを動かすスクリプト
*/

public class CPUControllor : MonoBehaviour
{
    
    private Vector3 init_position;
    private Rigidbody _rb;
    [SerializeField] Vector2 field_size;
    [SerializeField] private float x_speed = 10f;  // Strikerの移動速度
    [SerializeField] private float z_speed = 10f;  // Strikerの移動速度
    [SerializeField] float speed = 300f;
    public GameObject Puck;
    private GameObject target;
    GameObject[] mini_puck_list;

    public bool has_satellite = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        init_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mini_puck_list = GameObject.FindGameObjectsWithTag("MiniPuck");
        StateTransition();
    }

    private void StateTransition()
    {
        if (Puck.transform.position.z <= 0)
        {
            if (mini_puck_list.Length > 0)
            {
                AttackMiniPuck();
            }
            else
            {
                ReturnMove(Puck.transform.position);
            }
        }
        else
        {
            if (Puck.GetComponent<PuckControllor>().GetDirectionZ() >= 0)
            {
                if (Puck.transform.position.z - transform.position.z <= 0)
                {
                    AttackMove(Puck.transform.position);
                }
                else
                {
                    DeffenceMove(Puck.transform.position);
                }
            }
            else
            {
                if (mini_puck_list.Length > 0)
                {
                    AttackMiniPuck();
                }
                else
                {
                    DeffenceMove(Puck.transform.position);
                }
            }
        }
        PositionRestriction();
    }

    private void AttackMiniPuck()
    {
        foreach (var mini_puck in mini_puck_list)
        {
            if (mini_puck.GetComponent<MiniPuckControllor>().GetDirectionZ() >= 0)
            {
                target = mini_puck;
                break;
            }
        }
        if (target != null)
        {
            if (target.transform.position.z > 0)
            {
                if (target.transform.position.z < transform.position.z)
                {
                    AttackMove(target.transform.position);
                }
                else
                {
                    DeffenceMove(target.transform.position);
                }
            }
            else
            {
                ReturnMove(target.transform.position);
            }
        }
    }

    private void AttackMove(Vector3 target_position)
    {
        float v_x = 0f;
        float v_z = 0f;
        if (target_position.z - transform.position.z < 10) v_z = -z_speed;
        else v_z = -(z_speed / 4);
        if (target_position.x - transform.position.x > 1f) v_x = x_speed;
        else if (target_position.x - transform.position.x < -1f) v_x = -x_speed;
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * speed * Time.deltaTime;
    }

    private void DeffenceMove(Vector3 target_position)
    {
        float v_x = 0f;
        float v_z = 0f;
        if (target_position.x - transform.position.x > 1f) v_x = x_speed;
        else if (target_position.x - transform.position.x < -1f) v_x = -x_speed;
        if (transform.position.z < init_position.z)
        {
            v_z = z_speed;
        }
        else
        {
            v_z = -z_speed;
        }
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * speed * Time.deltaTime;
    }

    private void ReturnMove(Vector3 target_position)
    {
        float v_x = 0f;
        float v_z = 0f;
        if (target_position.x - transform.position.x > 1f) v_x = x_speed;
        else if (target_position.x - transform.position.x < -1f) v_x = -x_speed;
        if (transform.position.z < init_position.z)
        {
            v_z = z_speed;
        }
        else
        {
            v_z = -z_speed;
        }
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * speed * Time.deltaTime;
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
}

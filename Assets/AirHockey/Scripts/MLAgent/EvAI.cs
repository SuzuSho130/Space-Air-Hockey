using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvAI : MonoBehaviour
{
    private Vector3 init_position;
    private Rigidbody _rb;
    [SerializeField] Vector2 field_size;
    [SerializeField] private float x_speed = 0.5f;  // Strikerの移動速度
    [SerializeField] private float z_speed = 10f;  // Strikerの移動速度
    [SerializeField] private float max_speed = 1f;  // Strikerの移動速度
    [SerializeField] float speed = 10f;
    public GameObject Puck;
    GameObject[] mini_puck_list;
    int[] count_mini_puck = { 0, 0, 0, 0, 0, 0 };

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
        // mini_puck_list = GameObject.FindGameObjectsWithTag("MiniPuck");
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
        float v_x = 0f;
        float v_z = 0f;
        if (Puck.transform.position.z - transform.position.z < 10) v_z = -z_speed;
        else v_z = -(z_speed / 4);
        if (Puck.transform.position.x - transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - transform.position.x < 0 ) v_x = -x_speed;
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * speed * Time.deltaTime;
    }

    private void DeffenceMove()
    {
        float v_x = 0f;
        float v_z = 0f;
        if (Puck.transform.position.x - transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - transform.position.x < 0) v_x = -x_speed;
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

    private void ReturnMove()
    {
        float v_x = 0f;
        float v_z = 0f;
        if (Puck.transform.position.x - transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - transform.position.x < 0) v_x = -x_speed;
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

    // public void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Puck")
    //     {
    //         _rb.velocity = Vector3.zero;
    //     }
    // }
}

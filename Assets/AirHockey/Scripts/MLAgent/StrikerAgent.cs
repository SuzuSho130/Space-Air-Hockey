using UnityEngine;
using MLAgents;

public class StrikerAgent : Agent
{
    Rigidbody rBody;
    public float speed = 10;
    private float timer = 0;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        _rb = EvStriker.GetComponent<Rigidbody>();
    }

    public Transform Puck;
    public Transform EnemyStriker;
    // public GameObject MyGoal;
    // public GameObject EnemyGoal;

    public override void AgentReset()
    {
        timer = 0f;
        //エージェントを初期位置に戻す
        this.rBody.velocity = Vector3.zero;
        this.transform.position = new Vector3(0f, 0f, -25f);
        //Puckを再配置
        Puck.position = new Vector3(0f, 0f, 0f);
        Puck.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //評価用AIのStrikerを再配置
        Debug.Log("CH");
        EnemyStriker.position = new Vector3(0f, 0f, 25f);
    }

    public override void CollectObservations()
    {
        // Puckと各ストライカーの位置
        AddVectorObs(Puck.position);
        AddVectorObs(EnemyStriker.position);
        AddVectorObs(this.transform.position);

        //エージェントの速度
        AddVectorObs(rBody.velocity.x);
        AddVectorObs(rBody.velocity.z);
    }

    public override void AgentAction(float[] vectorAction)
    {
        timer += Time.deltaTime;
        //行動
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        rBody.velocity = controlSignal * speed;

        StateTransition();
        if (this.transform.position.z > 10f)
        {
            Done();
        }
        if (timer > 50f)
        {
            Done();
        }
    }

    public void EndOneBattle(bool power)
    {
        Debug.Log("check");
        if (power)
        {
            SetReward(1.0f);
            Done();
        }
        else
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

    private Vector3 init_position;
    private Rigidbody _rb;
    public GameObject EvStriker;
    [SerializeField] Vector2 field_size;
    [SerializeField] private float x_speed = 0.5f;  // Strikerの移動速度
    [SerializeField] private float z_speed = 10f;  // Strikerの移動速度
    [SerializeField] private float max_speed = 1f;  // Strikerの移動速度
    [SerializeField] float t_speed = 10f;
    GameObject[] mini_puck_list;
    int[] count_mini_puck = { 0, 0, 0, 0, 0, 0 };

    private void StateTransition()
    {
        if (Puck.transform.position.z <= 0)
        {
            ReturnMove();
        }
        else
        {
            if (Puck.transform.position.z - EvStriker.transform.position.z <= 0)
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
        if (Puck.transform.position.z - EvStriker.transform.position.z < 10) v_z = -z_speed;
        else v_z = -(z_speed / 4);
        if (Puck.transform.position.x - EvStriker.transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - EvStriker.transform.position.x < 0) v_x = -x_speed;
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * t_speed * Time.deltaTime;
    }

    private void DeffenceMove()
    {
        float v_x = 0f;
        float v_z = 0f;
        if (Puck.transform.position.x - EvStriker.transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - EvStriker.transform.position.x < 0) v_x = -x_speed;
        if (transform.position.z < init_position.z)
        {
            v_z = z_speed;
        }
        else
        {
            v_z = -z_speed;
        }
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * t_speed * Time.deltaTime;
    }

    private void ReturnMove()
    {
        float v_x = 0f;
        float v_z = 0f;
        if (Puck.transform.position.x - EvStriker.transform.position.x > 0) v_x = x_speed;
        else if (Puck.transform.position.x - EvStriker.transform.position.x < 0) v_x = -x_speed;
        if (transform.position.z < init_position.z)
        {
            v_z = z_speed;
        }
        else
        {
            v_z = -z_speed;
        }
        /*  */
        _rb.velocity = new Vector3(v_x, 0f, v_z) * t_speed * Time.deltaTime;
    }

    /* 台内に収まるようにStrikerの移動を制限 */
    private void PositionRestriction()
    {
        Vector3 player_pos = EvStriker.transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -field_size.x / 2f, field_size.x / 2f);
        player_pos.z = Mathf.Clamp(player_pos.z, 0, field_size.y / 2f);
        EvStriker.transform.position = player_pos;
    }
}
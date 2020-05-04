using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAgent : MonoBehaviour
{

    public bool power;
    public GameObject puck;
    public StrikerAgent striker_agent;

    void Start()
    {
        // _rb = puck.GetComponent<Rigidbody>();
    }

    /*
    packがゴールに入ったときに行う処理
    スコアを更新してpackを再配置する
    */
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Puck")
        {
            striker_agent.EndOneBattle(power);
        }
        if (other.transform.tag == "MiniPuck")
        {
            other.gameObject.SetActive(false);
        }
    }
}

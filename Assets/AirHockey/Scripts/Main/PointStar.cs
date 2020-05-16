using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointStar : MonoBehaviour
{
    [SerializeField] private int point = 1;
    public Vector3 _pool_pos = new Vector3(1000f, 1000f, 1000f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0.5f, 0f);
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 1), transform.position.z);
    }

    public void Init(int point, Color col)
    {
        this.point = point;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().materials[1].color = col;
        // GetComponentInChildren<Renderer>().material.color = col;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puck")
        {
            bool side = other.transform.GetComponent<PuckControllor>().side;
            GameObject.Find("GameManager").GetComponent<GameManager>().ScoreCount(side, point);
            Reset();
        }
    }
    private void Reset()
    {
        gameObject.SetActive(false);
        transform.position = _pool_pos;
    }
}

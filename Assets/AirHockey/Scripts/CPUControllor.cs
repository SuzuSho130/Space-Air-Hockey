using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUControllor : MonoBehaviour
{
    private Vector3 position;
    private Rigidbody _rb;
    public float speed = 0.5f;
    public GameObject Puck;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 pos = new Vector3(Puck.transform.position.x * speed, 0f, transform.position.z); 
        _rb.MovePosition(pos);
        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -135, 135);
        player_pos.z = Mathf.Clamp(player_pos.z, 0, 300);
        transform.position = player_pos;
    }
}

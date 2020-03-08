using System.Collections;
using UnityEngine;

public class StrikerController: MonoBehaviour {
	
	public float scroll_speed = 0.01f;
	public float min_scroll = 0.01f;
	public float max_scroll = 1.0f;
	private Vector3 startPostion;
	private Vector3 preMousePosition;
	private Vector3 startMousePosition;
	private Rigidbody _rb;
	public float speed = 0.5f;

	void Start () {
		startPostion = transform.position;
		startMousePosition = Input.mousePosition;
		_rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) {
			startMousePosition = Input.mousePosition;
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		ChangeSpeed (scroll);
		var mousePosition = Input.mousePosition;
		Move (mousePosition);
		preMousePosition = mousePosition;
	}

	private void Move(Vector3 currentMousePosition) {
        Vector3 position = (currentMousePosition - startMousePosition);
        position.z = position.y - 250.0f / speed;
		_rb.MovePosition(position * speed);
		if(_rb.velocity.magnitude > 1) {
            _rb.velocity = _rb.velocity.normalized;
		}
		Vector3 player_pos = transform.position;
		player_pos.x = Mathf.Clamp(player_pos.x, -135, 135);
		player_pos.z = Mathf.Clamp(player_pos.z, -300, 0);
		transform.position = player_pos;
	}
		
	private void ChangeSpeed(float scroll) {
		if (scroll < 0) {
			speed -= scroll_speed;
		} else if (scroll > 0) {
			speed += scroll_speed;
		}
		speed = Mathf.Clamp (speed, min_scroll, max_scroll);
	}
}
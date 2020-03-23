﻿using UnityEngine;

/*
プレイヤーが操作するstrikerの動きを制御するスクリプト
マウスの初期位置と現在のマウスの位置の差に応じてstrikerを移動する
*/

public class StrikerController: MonoBehaviour {
	
	public float scroll_speed = 0.01f;	// マウスとストライカーの動きを調整するための変数
	public float min_scroll = 0.01f;
	public float max_scroll = 1.0f;
	private Vector3 startPostion;		// strikerの初期位置
	private Vector3 startMousePosition;	// 最初のマウスの位置
	private Rigidbody _rb;
	public float speed = 0.5f;			// strikerの移動速度

	void Start () {
		/* strikerとマウスの初期値を計算 */
		startPostion = transform.position;
		startMousePosition = Input.mousePosition;
		_rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) {	// マウスの初期位置を現在の場所に更新
			startMousePosition = Input.mousePosition;
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		ChangeSpeed (scroll);

		var mousePosition = Input.mousePosition;
		Move (mousePosition);
	}


    /* マウスの移動位置からstrikerの移動位置を決定 */
    private void Move(Vector3 currentMousePosition) {
        Vector3 position = (currentMousePosition - startMousePosition);
        position.z = position.y - 250.0f / speed;
		_rb.MovePosition(position * speed);
		if(_rb.velocity.magnitude > 1) {
            _rb.velocity = _rb.velocity.normalized;
        }
        /* 台内に収まるようにStrikerの移動を制限 */
        Vector3 player_pos = transform.position;
		player_pos.x = Mathf.Clamp(player_pos.x, -135, 135);
		player_pos.z = Mathf.Clamp(player_pos.z, -300, 0);
		transform.position = player_pos;
	}
	
	/*
	strikerの移動速度を変更する関数
	引数:マウスのスクロール変化量
	*/
	private void ChangeSpeed(float scroll) {
		if (scroll < 0) {
			speed -= scroll_speed;
		} else if (scroll > 0) {
			speed += scroll_speed;
		}
		speed = Mathf.Clamp (speed, min_scroll, max_scroll);
	}
}
using UnityEngine;

/*
プレイヤーが操作するstrikerの動きを制御するスクリプト
マウスの初期位置と現在のマウスの位置の差に応じてstrikerを移動する
*/

public class StrikerController: MonoBehaviour {
	
	[SerializeField] Vector2 field_size;
    [SerializeField] float scroll_speed = 0.01f;    // マウスとストライカーの動きを調整するための変数
    [SerializeField] float min_scroll = 0.01f;
    [SerializeField] float max_scroll = 1.0f;
    [SerializeField] float speed = 0.5f;			// strikerの移動速度
	[SerializeField] float limit = 1f;
	private Vector3 startPostion;		// strikerの初期位置
	private Vector3 startMousePosition;	// 最初のマウスの位置
	private Rigidbody _rb;

	private Vector3 prevMousePos;

	void Start () {
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		/* strikerとマウスの初期値を計算 */
		startPostion = transform.position;
		startMousePosition = Input.mousePosition;
		prevMousePos = Input.mousePosition;
		_rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) {	// マウスの初期位置を現在の場所に更新
			transform.position = new Vector3(0, 0, -25);
		}
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		ChangeSpeed (scroll);
	}

	void FixedUpdate ()
    {
        var mousePosition = Input.mousePosition;
        Move(mousePosition);
	}


    /* マウスの移動位置からstrikerの移動位置を決定 */
    private void Move(Vector3 currentMousePosition) {
        Vector3 position = (currentMousePosition - prevMousePos);
        position.z = position.y;
		var pos = Vector3.ClampMagnitude(position / speed, limit);
		_rb.MovePosition(transform.position + pos * Time.deltaTime);
		prevMousePos = currentMousePosition;

        /* 台内に収まるようにStrikerの移動を制限 */
        Vector3 player_pos = transform.position;
		player_pos.x = Mathf.Clamp(player_pos.x, -field_size.x / 2f, field_size.x / 2f);
		player_pos.z = Mathf.Clamp(player_pos.z, -field_size.y / 2, 0);
		transform.position = player_pos;
	}

	/*
	strikerの移動速度を変更する関数
	引数:マウスのスクロール変化量
	*/
	private void ChangeSpeed(float scroll) {
		if (scroll < 0) {
			speed += scroll_speed;
		} else if (scroll > 0) {
			speed -= scroll_speed;
		}
		speed = Mathf.Clamp (speed, min_scroll, max_scroll);
	}
}
using UnityEngine;

/*
packがゴールに入って場合にスコア計算を行うスクリプト
*/

public class Goal : MonoBehaviour {

	public bool power;
	public GameObject puck;
	private Rigidbody _rb;
	private Vector3 puckPosition;
	public GameObject GameManager;

	void Start() {
		_rb = puck.GetComponent<Rigidbody>();
		/* packを再配置する際にどちらがのゴールかを登録 */
		if (transform.position.z > 0) {
			puckPosition = new Vector3 (0, 0, 200);
		} else {
			puckPosition = new Vector3 (0, 0, -200);
		}
	}

	/*
	packがゴールに入ったときに行う処理
	スコアを更新してpackを再配置する
	*/
	void OnTriggerEnter (Collider other)
	{
		GameManager gameManager = GameManager.GetComponent<GameManager>();
		if(other.transform.tag == "Puck")
		{
			gameManager.Reset();
			gameManager.ScoreCount(power, 1);
			ResetPuck ();
        }
        if (other.transform.tag == "MiniPuck")
        {
            gameManager.ScoreCount(power, 1);
			Destroy(other.gameObject);
        }
	}
	
	/* packの再配置する関数 */
	void ResetPuck() {
		puck.transform.position = puckPosition;
		_rb.velocity = Vector3.zero;
	}
}

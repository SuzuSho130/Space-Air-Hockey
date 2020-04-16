using UnityEngine;
using UnityEngine.UI;

/*
packがゴールに入って場合にスコア計算を行うスクリプト
*/

public class Goal : MonoBehaviour {

	public GameObject _score;
	public GameObject puck;
	private Rigidbody _rb;
	private int scoreCount = 0;
	private Vector3 puckPosition;

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
		if(other.transform.tag == "Puck")
		{
			scoreCount++;
			ResetPuck ();
			_score.GetComponent<Text> ().text = scoreCount.ToString();
        }
        if (other.transform.tag == "MiniPuck")
        {
            scoreCount++;
            _score.GetComponent<Text>().text = scoreCount.ToString();
			Destroy(other.gameObject);
        }
	}
	
	/* packの再配置する関数 */
	void ResetPuck() {
		puck.transform.position = puckPosition;
		_rb.velocity = Vector3.zero;
	}
}

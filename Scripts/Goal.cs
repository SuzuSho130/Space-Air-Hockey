using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

	public GameObject _score;
	public GameObject puck;
	private Rigidbody _rb;
	private int scoreCount = 0;
	private Vector3 puckPosition;

	void Start() {
		_rb = puck.GetComponent<Rigidbody>();
		if (transform.position.z > 0) {
			puckPosition = new Vector3 (0, 0, 200);
		} else {
			puckPosition = new Vector3 (0, 0, -200);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		string layerName = LayerMask.LayerToName(other.gameObject.layer);
		if( layerName == "Puck")
		{
			scoreCount++;
			ResetPuck ();
			_score.GetComponent<Text> ().text = scoreCount.ToString();
		}
	}
		
	void ResetPuck() {
		puck.transform.position = puckPosition;
		_rb.velocity = Vector3.zero;
	}
}

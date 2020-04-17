using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mini_pack;
    public Vector2 field_size;
    public GameObject CPUContorollor;
    public GameObject ResultMenu;
    public GameObject player_score_object;
    public GameObject enemy_score_object;
    public float timer = 100.0f;
    private int player_score = 0;
    private int enemy_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShootingStarTimer");
        
    }

    void Update()
    {
        if (GameTimer())
        {
            Time.timeScale = 0f;
            ResultMenu.SetActive(true);
        }
    }

    private bool GameTimer()
    {
        Debug.Log(timer);
        timer -= Time.deltaTime;
        if(timer <= 0) return true;
        else return false;
    }

    public void Reset()
    {
        CPUContorollor.GetComponent<CPUControllor>().ResetPosition();
    }

    IEnumerator ShootingStarTimer()
    {
        float timer;
        while (true)
        {
            timer = Random.Range(30.0f, 60.0f);
            yield return new WaitForSeconds(timer);
            int count = Random.Range(5, 30);
            StartCoroutine(ShootingStar(count));
        }
    }

    IEnumerator ShootingStar(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(mini_pack, new Vector3(Random.Range(-field_size.x / 2.0f, field_size.x / 2.0f), 10.0f, Random.Range(-field_size.y / 2.0f, field_size.y / 2.0f)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ScoreCount(bool power, int value)
    {
        if (power)
        {
            player_score += value;
            player_score_object.GetComponent<Text>().text = player_score.ToString();
        }
        else
        {
            enemy_score += value;
            enemy_score_object.GetComponent<Text>().text = enemy_score.ToString();
        }
    }
}

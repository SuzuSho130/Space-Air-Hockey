using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector2 field_size;
    public GameObject CPUContorollor;
    public GameObject ui_object;
    public GameObject result_menu;
    public GameObject shooting_star_obj;
    public GameObject satellite_star_obj;
    public GameObject point_star_obj;
    Text player_score_text;
    Text enemy_score_text;
    Slider timer_slider;
    public float timer = 100.0f;
    private int player_score = 0;
    private int enemy_score = 0;
    public bool shooting_star_mode = true;
    public bool satellite_mode = true;
    public bool point_star_mode = true;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        player_score_text = ui_object.transform.Find("PlayerScore").GetComponent<Text>();
        enemy_score_text = ui_object.transform.Find("EnemyScore").GetComponent<Text>();
        timer_slider = ui_object.transform.Find("Timer").GetComponent<Slider>();
        timer_slider.maxValue = timer;
        UseMode();
    }

    private void UseMode()
    {
        if (shooting_star_mode)
        {
            shooting_star_obj.SetActive(true);
            shooting_star_obj.GetComponent<ShootingStarControllor>().Init(field_size);
        }
    }

    void Update()
    {
        if (GameTimer())
        {
            Time.timeScale = 0f;
            Result();
        }
    }

    private bool GameTimer()
    {
        timer -= Time.deltaTime;
        timer_slider.value = timer;
        if(timer <= 0) return true;
        else return false;
    }

    public void Reset()
    {
        CPUContorollor.GetComponent<CPUControllor>().ResetPosition();
    }

    public void ScoreCount(bool power, int value)
    {
        if (power)
        {
            player_score += value;
            player_score_text.text = player_score.ToString();
        }
        else
        {
            enemy_score += value;
            enemy_score_text.text = enemy_score.ToString();
        }
    }

    private void Result()
    {
        int score = enemy_score - player_score;
        string result = "";
        Color color;
        if (score > 0)
        {
            result = "Win";
            color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (score == 0)
        {
            result = "Draw";
            color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        else 
        {
            result = "Lose";
            color = new Color(0.0f, 0.0f, 6.0f, 1.0f);
        }
        result_menu.SetActive(true);
        result_menu.transform.Find("ResultText").GetComponent<Text>().text = result;
        result_menu.transform.Find("ResultText").GetComponent<Text>().color = color;
        result_menu.transform.Find("PlayerScoreValueText").GetComponent<Text>().text = score.ToString();
    }

    public void PushButtonTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}

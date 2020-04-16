using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject mini_pack;
    public Vector2 field_size;
    public GameObject CPUContorollor;
    public GameObject ResultMenu;
    public float timer = 100.0f;
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
}

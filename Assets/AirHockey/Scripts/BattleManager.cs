using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject mini_pack;
    public Vector2 field_size;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShootingStarTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootingStarTimer()
    {
        float timer;
        while(true)
        {
            timer = Random.Range(30.0f, 60.0f);
            yield return new WaitForSeconds(timer);
            int count = Random.Range(5, 30);
            StartCoroutine(ShootingStar(count));
        }
    }

    IEnumerator ShootingStar(int count)
    {
        for(int i=0;i<count;i++)
        {
            Instantiate(mini_pack, new Vector3(Random.Range(-field_size.x / 2.0f, field_size.x / 2.0f), 10.0f, Random.Range(-field_size.y / 2.0f, field_size.y / 2.0f)), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}

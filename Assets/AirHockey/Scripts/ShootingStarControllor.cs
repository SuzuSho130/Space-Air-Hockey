using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ShootingStarControllor : MonoBehaviour
{

    public GameObject _mini_puck_prefab;
    [SerializeField] private float frequency = 30.0f;
    [SerializeField] private float interval = 0.5f;
    [SerializeField] private int mini_puck_max = 50;
    [SerializeField] private int mini_puck_min = 20;
    [SerializeField] private float mini_puck_life = 20f;
    private ObjectPool _mini_puck_pool;
    private float timer;
    private Vector2 _field_size;

    // Start is called before the first frame update
    public void Init(Vector2 field_size)
    {
        _field_size = field_size;
        frequency = Random.Range(frequency - 10f, frequency + 10f);
        _mini_puck_pool = GetComponent<ObjectPool>();
        _mini_puck_pool.CreatePool(_mini_puck_prefab, mini_puck_max);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frequency)
        {
            timer = 0;
            StartCoroutine(StartShootingStarMode(Random.Range(mini_puck_min, mini_puck_max)));
        }
    }

    IEnumerator StartShootingStarMode(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var mini_puck = _mini_puck_pool.GetMiniPuck();
            mini_puck.GetComponent<MiniPuckControllor>().Init(mini_puck_life);
            var pos = new Vector3(Random.Range(-_field_size.x / 2.0f, _field_size.x / 2.0f), 10.0f, Random.Range(-_field_size.y / 2.0f, _field_size.y / 2.0f));
            mini_puck.transform.position = pos;
            yield return new WaitForSeconds(interval);
        }
    }
}

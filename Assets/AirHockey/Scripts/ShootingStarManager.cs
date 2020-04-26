using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarManager : MonoBehaviour
{
    private ObjectPool _shooting_star_pool;
    private ObjectPool _puck_pool;
    private ObjectPool _point_pool;

    public GameObject _shooting_star;
    public GameObject _puck;
    public GameObject _point;

    [SerializeField] private int _max_shooting_star = 60;
    [SerializeField] private int _max_puck = 50;
    [SerializeField] private int _max_point = 10;

    private Vector3 _field_size;
    private float timer;

    [SerializeField] private float frequency = 2f;

    // Start is called before the first frame update

    void Start()
    {
        Init(new Vector3(28, 0, 68));
    }

    public void Init(Vector3 field_size)
    {
        _field_size = field_size;

        _shooting_star_pool = transform.Find("ShootingStarPool").GetComponent<ObjectPool>();
        _puck_pool = transform.Find("PuckPool").GetComponent<ObjectPool>();
        _point_pool = transform.Find("PointPool").GetComponent<ObjectPool>();

        _shooting_star_pool.CreatePool(_shooting_star, _max_shooting_star);
        _puck_pool.CreatePool(_puck, _max_puck);
        _point_pool.CreatePool(_point, _max_point);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frequency)
        {
            timer = 0;
            ShootStar();
        }
    }

    private void ShootStar()
    {
        Debug.Log("Shoot");
        var shooting_star = _shooting_star_pool.GetObject();
        var pos = new Vector3(Random.Range(-_field_size.x / 2.0f - 40, _field_size.x / 2.0f - 40), 40.0f, Random.Range(-_field_size.z / 2.0f + 10, _field_size.z / 2.0f - 10));
        shooting_star.transform.position = pos;
        shooting_star.GetComponent<ShootingStar>().Init();
    }

    public void Birth(Vector3 pos)
    {
        var puck = _puck_pool.GetObject();
        puck.transform.position = pos;
        puck.GetComponent<MiniPuckControllor>().Init();
    }
}

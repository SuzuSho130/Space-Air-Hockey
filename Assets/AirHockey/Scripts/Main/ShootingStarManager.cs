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

    [SerializeField] private float _puck_rate = 0f;
    [SerializeField] private float _point_rate = 70f;

    [SerializeField] private int _max_meteor = 60;
    [SerializeField] private int _min_meteor = 30;

    private Vector3 _field_size;
    private float timer;

    [SerializeField] private float _shooting_star_frequency = 10f;
    [SerializeField] private float _meteor_shower_frequency = 10f;
    private float shooting_star_interval = 0;
    private float meteor_shower_interval = 0;

    // Start is cated before the first frame update

    void Start()
    {
        shooting_star_interval += _shooting_star_frequency * Random.Range(0.7f, 1.3f);
        meteor_shower_interval += _meteor_shower_frequency * Random.Range(0.7f, 1.3f);
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
        if (timer >= shooting_star_interval)
        {
            shooting_star_interval += _shooting_star_frequency * Random.Range(0.7f, 1.3f);
            ShootStar();
        }
        if (timer >= meteor_shower_interval)
        {
            meteor_shower_interval += _meteor_shower_frequency * Random.Range(0.7f, 1.3f);
            StartCoroutine(MeteorShower(Random.Range(_min_meteor, _max_meteor)));
        }
    }

    private void ShootStar()
    {
        var shooting_star = _shooting_star_pool.GetObject();
        var pos = new Vector3(Random.Range(-_field_size.x / 2.0f - 40, _field_size.x / 2.0f - 40), 40.0f, Random.Range(-_field_size.z / 2.0f + 10, _field_size.z / 2.0f - 10));
        shooting_star.transform.position = pos;
        shooting_star.GetComponent<ShootingStar>().Init();
    }

    public void Birth(Vector3 pos)
    {
        float select = Random.Range(0, 100);
        if (select >=_point_rate)
        {
            var point = _point_pool.GetObject();
            point.transform.position = pos;
            float point_value = Random.Range(0, 10);
            if (point_value >= 9)
            {
                point.GetComponent<PointStar>().Init(10, Color.red);
            }
            else if (point_value >= 6)
            {
                point.GetComponent<PointStar>().Init(3, Color.yellow);
            }
            else
            {
                point.GetComponent<PointStar>().Init(1, Color.blue);
            }

        }
        else if (select >= _puck_rate)
        {
            var puck = _puck_pool.GetObject();
            puck.transform.position = pos;
            puck.GetComponent<MiniPuckControllor>().Init();
        }
    }

    private IEnumerator MeteorShower(int count)
    {
        float interval;
        for (int i = 0; i < count; i++)
        {
            ShootStar();
            interval = Random.Range(0.05f, 0.2f);
            yield return new WaitForSeconds(interval);
        }
    }
}

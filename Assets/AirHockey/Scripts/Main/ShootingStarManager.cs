using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarManager : MonoBehaviour
{
    private ObjectPool _shooting_star_pool;
    private ObjectPool _puck_pool;
    private ObjectPool _point_pool;
    private ObjectPool _satellite_pool;

    public GameObject _shooting_star_prefab;
    public GameObject _puck_prefab;
    public GameObject _point_prefab;
    public GameObject _satellite_prefab;

    private bool use_shootingstar;
    private bool use_meteorshower;
    private bool use_minipuck;
    private bool use_point;
    private bool use_satellite;

    private float _shooting_star_frequency;
    private float _meteor_shower_frequency;
    private float _puck_rate = 0f;
    private float _point_rate = 0f;
    private float _satellite_rate = 0f;

    private float _sum_rate = 0f;

    [SerializeField] private int _max_shooting_star = 60;
    [SerializeField] private int _max_count = 40;

    [SerializeField] private int _max_meteor = 60;
    [SerializeField] private int _min_meteor = 30;

    private Vector3 _field_size = new Vector3(28, 0, 68);
    private float timer;

    private float shooting_star_interval = 0;
    private float meteor_shower_interval = 0;

    void Start()
    {
        UseMode();
    }

    private void UseMode()
    {
        use_shootingstar = Setting.use_shootingstar;
        use_meteorshower = Setting.use_meteorshower;
        use_minipuck = Setting.use_minipuck;
        use_point = Setting.use_point;
        use_satellite = Setting.use_satellite;
        if (use_shootingstar)
        {
            _shooting_star_frequency = Setting.shootingstar_frequency;
            shooting_star_interval +=  _shooting_star_frequency * Random.Range(0.7f, 1.3f);
        }
        if (use_meteorshower)
        {
            _meteor_shower_frequency = Setting.meteorshower_frequency;
            meteor_shower_interval += _meteor_shower_frequency * Random.Range(0.7f, 1.3f);
        }
        if (use_shootingstar || use_meteorshower)
        {
            _shooting_star_pool = transform.Find("ShootingStarPool").GetComponent<ObjectPool>();
            _shooting_star_pool.CreatePool(_shooting_star_prefab, _max_shooting_star);
        }
        if (use_minipuck && (use_shootingstar || use_meteorshower))
        {
            _puck_rate = Setting.minipuck_rate;
            _puck_pool = transform.Find("PuckPool").GetComponent<ObjectPool>();
            _puck_pool.CreatePool(_puck_prefab, _max_count);
        }
        if (use_point && (use_shootingstar || use_meteorshower))
        {
            _point_rate = Setting.point_rate;
            _point_pool = transform.Find("PointPool").GetComponent<ObjectPool>();
            _point_pool.CreatePool(_point_prefab, _max_count);
        }
        if (use_satellite && (use_shootingstar || use_meteorshower))
        {
            _satellite_rate = Setting.satellite_rate;
            _satellite_pool = transform.Find("SatellitePool").GetComponent<ObjectPool>();
            _satellite_pool.CreatePool(_satellite_prefab, _max_count);
        }
        _sum_rate = _puck_rate + _point_rate + _satellite_rate;
        if(use_minipuck)
        {
            _puck_rate += _point_rate + _satellite_rate;
        }
        if(use_point)
        {
            _point_rate += _satellite_rate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shooting_star_interval && use_shootingstar)
        {
            shooting_star_interval += _shooting_star_frequency * Random.Range(0.7f, 1.3f);
            ShootStar();
        }
        if (timer >= meteor_shower_interval && use_meteorshower)
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
        float select = Random.Range(0, _sum_rate);
        if (select <= _satellite_rate)
        {
            var satellite = _satellite_pool.GetObject();
            satellite.transform.position = pos;
            satellite.GetComponent<SatelliteControllor>().Init();
        }
        else if (select <= _point_rate)
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
        else if (select <= _puck_rate)
        {
            var puck = _puck_pool.GetObject();
            puck.transform.position = pos;
            puck.GetComponent<MiniPuckControllor>().Init();
        }
        else
        {
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

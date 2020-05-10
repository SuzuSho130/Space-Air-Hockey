using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    public GameObject UISetting;

    public static bool use_shootingstar = true;
    public static bool use_meteorshower = true;
    public static bool use_minipuck = true;
    public static bool use_point = true;
    public static bool use_satellite = true;
    
    public static float shootingstar_frequency = 8f;
    public static float meteorshower_frequency = 30f;
    public static float minipuck_rate = 70f;
    public static float point_rate = 30f;
    public static float satellite_rate = 30f;

    public Toggle use_shootingstar_toggle;
    public Toggle use_meteorshower_toggle;
    public Toggle use_minipuck_toggle;
    public Toggle use_point_toggle;
    public Toggle use_satellite_toggle;

    public Slider shootingstar_frequency_slider;
    public Slider meteorshower_frequency_slider;
    public Slider minipuck_rate_slider;
    public Slider point_rate_slider;
    public Slider satellite_rate_slider;

    // public bool GetUseShootingStar() { return use_shootingstar; }
    // public bool GetUseMeteorShower() { return use_meteorshower; }
    // public bool GetUseMiniPuck() { return use_minipuck; }
    // public bool GetUsePoint() { return use_point; }
    // public bool GetUseSatellite() { return use_satellite; }

    // public float GetShootingStarFrequency() { return shootingstar_frequency; }
    // public float GetMeteorShowerFrequency() { return meteorshower_frequency; }
    // public float GetMiniPuckrate() { return minipuck_rate; }
    // public float GetPointRate() { return point_rate; }
    // public float GetSatelliteRate() { return satellite_rate; }

    public void SetUseShootingStar() { use_shootingstar = use_shootingstar_toggle.isOn; }
    public void SetUseMeteorShower() { use_meteorshower = use_meteorshower_toggle.isOn; }
    public void SetUseMiniPuck() { use_minipuck = use_minipuck_toggle.isOn; }
    public void SetUsePoint() { use_point = use_point_toggle.isOn; }
    public void SetUseSatellite() { use_satellite = use_satellite_toggle.isOn; }

    public void SetShootingStarFrequency() { shootingstar_frequency = shootingstar_frequency_slider.value; }
    public void SetMeteorShowerFrequency() { meteorshower_frequency = meteorshower_frequency_slider.value; }
    public void SetMinipuckRate() { minipuck_rate = minipuck_rate_slider.value; }
    public void SetPointRate() { point_rate = point_rate_slider.value; }
    public void SetSatelliteRate() { satellite_rate = satellite_rate_slider.value; }
    public void PushButtonReturn() { UISetting.SetActive(false); }
}

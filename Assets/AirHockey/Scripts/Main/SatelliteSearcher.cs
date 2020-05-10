using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteSearcher : MonoBehaviour
{
    public List<Transform> puck_list;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puck" || other.gameObject.tag == "MiniPuck")
        {
            puck_list.Add(other.gameObject.transform);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Puck" || other.gameObject.tag == "MiniPuck")
        {
            puck_list.Remove(other.gameObject.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _obj_list;
    private GameObject _obj;

    public void CreatePool(GameObject obj, int max_count)
    {
        _obj = obj;
        _obj_list = new List<GameObject>();
        for (int i = 0; i < max_count; i++)
        {
            var new_mini_puck = CreateNewMiniPuck();
            new_mini_puck.SetActive(false);
            _obj_list.Add(new_mini_puck);
        }
    }

    public GameObject GetMiniPuck()
    {
        foreach (var obj in _obj_list)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        var new_mini_puck = CreateNewMiniPuck();
        new_mini_puck.SetActive(true);
        _obj_list.Add(new_mini_puck);

        return new_mini_puck;
    }
    
    private GameObject CreateNewMiniPuck()
    {
        var new_mini_puck = Instantiate(_obj);

        return new_mini_puck;

    }
}

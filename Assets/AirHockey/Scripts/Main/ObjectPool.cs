using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _obj_list;
    private GameObject _obj;

    public void CreatePool(GameObject obj, int max_count)
    {
        // Debug.Log(max_count);
        _obj = obj;
        _obj_list = new List<GameObject>();
        for (int i = 0; i < max_count; i++)
        {
            var new_object = CreateNewObject();
            new_object.SetActive(false);
            _obj_list.Add(new_object);
        }
    }

    public GameObject GetObject()
    {
        foreach (var obj in _obj_list)
        {
            if (obj.activeSelf == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        var new_object = CreateNewObject();
        new_object.SetActive(true);
        _obj_list.Add(new_object);

        return new_object;
    }
    
    private GameObject CreateNewObject()
    {
        var new_object = Instantiate(_obj);

        return new_object;

    }
}

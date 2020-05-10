using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class How : MonoBehaviour
{

    public GameObject UIHow;
    public GameObject PageList;

    public void PushButtonReturn()
    {
        UIHow.SetActive(false);
    }

    public void PushButtonLeft()
    {
        var pos = PageList.transform.localPosition;
        pos.x += 740f;
        PageList.transform.localPosition = pos;
    }

    public void PushButtonRight()
    {
        var pos = PageList.transform.localPosition;
        pos.x -= 740f;
        PageList.transform.localPosition = pos;
    }
}

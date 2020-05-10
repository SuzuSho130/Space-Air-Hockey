using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public GameObject UISetting;
    public GameObject UIHow;

    public void PushButtonStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PushButtonSetting()
    {
        UISetting.SetActive(true);
    }

    public void PushButtonHow()
    {
        UIHow.SetActive(true);
    }
}

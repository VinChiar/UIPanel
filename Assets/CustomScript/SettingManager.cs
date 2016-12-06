using UnityEngine;
using System.Collections;

public class SettingManager : MonoBehaviour {

    public GameObject rootScene;
    public GameObject panel;
    private bool act;

    void Start()
    {
        act = false;
        panel.SetActive(act);
    }

    public void ToggleUI()
    {
        act = !act;
        panel.SetActive(act);
    }

    public void scaleScene(float sc)
    {
        rootScene.transform.localScale *= sc;
    }
}

using UnityEngine;
using System.Collections;

public class SettingManager : MonoBehaviour {

    public GameObject panel;
    SceneManager scnMgr;
    private bool act;

    void Start()
    {
        act = false;
        panel.SetActive(act);
        scnMgr = FindObjectOfType<SceneManager>();
    }

    public void ToggleUI()
    {
        act = !act;
        panel.SetActive(act);
    }

    public void scaleScene(float sc)
    {
        scnMgr.scaleScene(sc);
    }
}

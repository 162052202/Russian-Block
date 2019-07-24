using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUImanage : MonoBehaviour
{
    float time, startTime;
    public Text timer;
    public GameObject ui;
    bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        //初始化游戏开始的时间（秒）
        startTime = Time.time;
        isEnd = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd == true)
            return;
        
        //得到当前时间和游戏开始时间的差别（秒）
        time = Time.time - startTime;

        //得到秒
        int seconds = (int)(time % 60);

        //得到分
        int minute = (int)(time / 60);

        string strTime = string.Format("{0:00}:{1:00}", minute, seconds);
        timer.text = strTime;

    }
    public void gameover()
    {
        ui.SetActive(true);
        isEnd = true;
    }
}

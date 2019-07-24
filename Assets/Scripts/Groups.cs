using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Groups : MonoBehaviour
{
    // Start is called before the first frame update
    float lastFall=0;
    public  static int curScore;
    Text score;
    GameObject c;
    
    void Start()
    {
        c = GameObject.FindGameObjectWithTag("Finish");
        score = c.GetComponent<Text>();
        if(!isValidGridPos())
        {
            Debug.Log("Game Over");
            GameObject.Find("gameContral").GetComponent<GUImanage>().gameover();
            Destroy(gameObject); 
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        //控制Group左移
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }

        //控制Group右移
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        //控制Group旋转
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }

        //控制Group快速降落
        else if (Input.GetKeyDown(KeyCode.DownArrow)||Time.time-lastFall>=1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidGridPos())
            {
                updateGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

                //已经到位，是否可以删除已经“满”一行的数据
                Gird.deleteFullRows();
                score.text = curScore + "";
                FindObjectOfType<spwaner>().Spwanernext();
                enabled = false;
            }
            lastFall = Time.time;
        }
    }

    bool isValidGridPos()
    {   
        foreach(Transform child in transform)
        {
            Vector2 v = Gird.roundVer2(child.position);

            //1.判断是否在边界之内（左、右、下）
            if (!Gird.insideBorder(v))
            {
                return false;
            }

            //2.判断gird所对应的格子是否为空
            if(Gird.gird[(int)v.x,(int)v.y]!=null  &&
               Gird.gird[(int)v.x,(int)v.y].parent !=transform )
            {
                return false;   
            }
        }
        return true ;
    }

    void updateGrid()
    {
        //上一次的数据清理，移去原来占据的格子信息
        for(int y=0;y<Gird.h;y++)
            for(int x=0;x<Gird.w;x++)
            {
                if (Gird.gird[x, y] != null)
                    if (Gird.gird[x, y].parent == transform)
                        Gird.gird[x, y] = null;
            }

        //加入本次更新的位置信息
        foreach(Transform child in transform)
        {
            Vector2 v = Gird.roundVer2(child.position);
            Gird.gird[(int)v.x, (int)v.y] = child;
        }
    }
}

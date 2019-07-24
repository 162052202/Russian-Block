using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gird : MonoBehaviour
{
    public static int w = 10;  //width
    public static int h = 20;   //height

    private void Start()
    {
     
    }
    //数据结构
    public static Transform[,] gird = new Transform[w, h];
   
    //保证每个被检查的位置，不小于左边框，不大于右边框。
    public static Vector2 roundVer2(Vector2 v)
    { 
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        

        return ((int)pos.x >= 0 &&
                (int)pos.x < w  &&
                (int)pos.y >= 0);
    }

    public static bool isRowFull(int y)
    {
        //
        for (int x = 0; x < w; x++)
            if (gird[x, y] == null)
                return false;

        return true;
    }

    public static void deleteRow(int y)
    {
        //删除某一行的所有数据
        for (int x=0;x<w;x++)
        {
            Destroy(gird[x, y].gameObject);
            gird[x, y] = null;
            Groups.curScore += 20;
        }
       
    }
    public static void decreaseRow(int y)
    {
        //1.移动该行的数据到下一行
  
        //2.清空该行数据
        //3.视觉上的，改变原来方块的位置，y-1
        for (int x=0;x<w;x++)
        {
            if (gird[x, y] != null)
            {
                gird[x, y - 1] = gird[x, y];
                gird[x, y] = null;

                gird[x, y - 1].position += new Vector3(0, -1, 0);

            }

        }

    }
    public static void decreaseRowAbove(int y)
    {
        //从指定的行数开始检查改行和该行上的位置，把上面的数据搬到下面
        for (int i = y; i < h; i++)           
            decreaseRow(i);
    }

    public static void deleteFullRows()
    {
        for (int y=0;y<h;)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                //i=score.text+20;
                //score.text = curscore+"";
                decreaseRowAbove(y + 1);
            }
            else
                y++;
        }
        
    }

}

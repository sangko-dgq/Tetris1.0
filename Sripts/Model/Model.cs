using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    public const int NOMAL_ROWS = 20, MAX_ROWS = 23, MAX_COLUMNS = 10;

    private Transform[,] map = new Transform[MAX_COLUMNS, MAX_ROWS];  //存储最大行数列数的二维数组

    private int score = 0;
    private int highScore = 0;
    private int numbersGame = 0;

    public int Score {get{return score;}}
    public int HighScore {get {return highScore;}}
    public int NumbersGame {get {return numbersGame;}}

    public bool isDataUpdate = false;

    
    private void Awake() 
    {
        LoadData();
    }
    //遍历+判断下落位置是否可用
    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            if (IsInsideMap(pos) == false) return false; //若超出边界：return + return false
            if (map[(int)pos.x, (int)pos.y] != null)  //若没有超出边界,且这个位置有别的shape，：return+ return false
                return false;
        }
        return true; // 若没有超出边界，且当前位置没有其他shape，return true
    }
    
    public bool ISGameOver()
    {
        for(int i = NOMAL_ROWS; i< MAX_ROWS; i++)
        {
            for(int j = 0; j < MAX_COLUMNS; j++)
            {
                if(map[j,i] != null) 
                {
                    numbersGame ++;
                    SaveData();
                    return true;
                }
            }
        }
        return false;
    }
    private bool IsInsideMap(Vector2 pos)
    {
        return pos.x >= 0 && pos.x < MAX_COLUMNS && pos.y >= 0;
    }
    //摆放shape
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x, (int)pos.y] = child;
        }
        return CheckMap();
    }
    //检查是否能消除
    private bool CheckMap()
    {
        int count = 0; //消除了count行
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if (isFull)
            {
                count++;
                //消除这一行
                DeleteRow(i);
                //把所有上面的移到下面 
                MoveDownRowAbove(i + 1);
                i--;
            }
        }
        if (count > 0) 
        {
            score += (count*100);
            if(score > highScore)
            {
                highScore = score;
            }
            isDataUpdate = true;
            return true;
        }
        else return false;
    }
    private bool CheckIsRowFull(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] == null) return false;
        }
        return true;
    }
    private void DeleteRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;

        }
    }
    private void MoveDownRowAbove(int row)
    {
        for (int i = row; i < MAX_ROWS; i++)
        {
            MoveDownRow(i);
        }
    }
    private void MoveDownRow(int row)
    {
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            if (map[i, row] != null)
            {
                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    private void LoadData()  //加载数据
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        numbersGame = PlayerPrefs.GetInt("NumbersGame", 0);
    }
    private void SaveData() //保存数据到本地
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumbersGame", numbersGame);
    }
 
    public void ClearData()  //清空数据
    {

        score = 0;
        highScore =0;
        numbersGame = 0;
        SaveData();
    }
    public void Restart()
    {
        for(int i =0 ; i < MAX_COLUMNS; i++)
        {
            for(int j =0; j < MAX_ROWS; j++)
            {
                 if(map[i,j] != null)
                 {
                     Destroy(map[i,j].gameObject);
                     map[i,j] = null;
                 }
            }
        }
        score = 0;
    }
}

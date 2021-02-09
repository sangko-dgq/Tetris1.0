using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPause = true; //是否暂停
    private Shape currentShape = null;
    
    private Transform blockHoder;
    private Ctrl ctrl;

    public Shape[] shapes;  //存储7中shape的数组
    public Color[] colors;  //不同颜色数组

    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        blockHoder = transform.Find("BlockHolder");
    }


    private void Update()
    {
        if (isPause) return;
        if (currentShape == null)
        {
            SpawnShape();
        }
    }
    public void ClearShape()
    {
        if(currentShape != null)
        {
            Destroy(currentShape.gameObject);
            currentShape = null;
        }
    }
    public void StartGame()
    {
        isPause = false;
        if (currentShape != null)
            currentShape.Resum();

    }
    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
            currentShape.Pause();
    }
    //控制方块的随机生成
    void SpawnShape()
    {
        int indexShape = Random.Range(0, shapes.Length); //随机得到一个shape
        int indexColor = Random.Range(0, colors.Length); //随机得到一个颜色



        currentShape = GameObject.Instantiate(shapes[indexShape]);
        currentShape.transform.parent = blockHoder;
        currentShape.Init(colors[indexColor], ctrl, this); //调用shape类的随机颜色函数
    }
    //方落下来了
    public void FallDown()
    {
        currentShape = null;
        if (ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HighScore);
        }
        foreach(Transform t in blockHoder)
        {
            if(t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
        if (ctrl.model.ISGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }
}

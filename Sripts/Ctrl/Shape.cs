using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Transform pivot;
    private Ctrl ctrl;
    private GameManager gameManager;

    //public GameObject block;
    private bool isPause = false ; //是否暂停，暂停的时候偶shape也要暂停下落
    private bool isSpeedup = false;
    
    private float timer = 0; //计时器（多少秒的时间间隔下落一个shape）
    private float stepTime = 0.6f; //每一步需要多少时间（下落一格耗费时间）

    private int speedMultpie = 20;

    private GameObject leftButton, rightButton, rotateButton,speedUpButton;

   


    private void Awake() 
    {
        pivot = transform.Find("Pivot");

        leftButton = GameObject.Find("Canvas/GameUI/ControlBan/LeftButton");
        rightButton = GameObject.Find("Canvas/GameUI/ControlBan/RightButton");
        rotateButton = GameObject.Find("Canvas/GameUI/ControlBan/RotateButton");
        speedUpButton = GameObject.Find("Canvas/GameUI/ControlBan/SpeedUpButton");
        
    }
    private void Update() {
        if(isPause) return; //如果是暂停状态，返回，不往下执行
        
        timer += Time.deltaTime; //Time开始计时 
        if(timer > stepTime)
        {
            /******************************下落******************************************************/
            timer = 0;
            Fall();
        }
        InputControl();

    }

    public void Init(Color color, Ctrl ctrl, GameManager gameManager) //颜色初始化,设置为静态ststic方法，其他地方可以通过 Shape.Init访问
    {
        //是shape预制体里面的子级才变颜色
        /*if(block.tag == "Block")
        {
            block.GetComponent<SpriteRenderer>().color = color;
        }
        */
        foreach (Transform t in transform)  //在当前对象的所有子类里遍历查询，t为子类对象shape下面的block
        {
            if (t.tag == "Block") 
                t.GetComponent<SpriteRenderer>().color = color;
        }
        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }

    private void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;

        if(ctrl.model.IsValidMapPosition(this.transform) == false)
        {
            pos.y += 1;
            transform.position = pos;
            //停止运动
            isPause = true;
            bool isLineClear = ctrl.model.PlaceShape(this.transform);
            if(isLineClear) ctrl.audioManager.PlayLineClear();
            gameManager.FallDown();
            return;
        }
        ctrl.audioManager.PlayDrop();
    }

    private void InputControl()
    {

        float h = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow) || ctrl.view.isLeftButtonDown == true)
        {
            ctrl.view.isLeftButtonDown = false;
            h = -1;

        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) || ctrl.view.isRightButtonDown == true)
        {   
            ctrl.view.isRightButtonDown = false;
            h = 1;

        }
        if(h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;

            if(ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;
                
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }

/*******************************************************************************/
        //上键顺时针旋转(长按保持)
        if(Input.GetKeyDown (KeyCode.UpArrow) || ctrl.view.isRotateButtonDown == true)
        {
            ctrl.view.isRotateButtonDown = false;
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if(ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayControl();
            }
        }
        //下键加速下落
        if(Input.GetKeyDown(KeyCode.DownArrow) || ctrl.view.isSpeedupButtonDown == true)
        {
            ctrl.view.isSpeedupButtonDown = false;
            isSpeedup = true;
            stepTime /= speedMultpie;
        }

    }


    //暂停shape下落
    public void Pause()
    {
        isPause = true;
    }
    public void Resum()
    {
        isPause = false;
    }


}

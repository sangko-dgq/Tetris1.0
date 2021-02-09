using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    [HideInInspector] public Model model;
    [HideInInspector] public View view;

    [HideInInspector] public CameraManager cameraManager;

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public AudioManager audioManager;

    private FSMSystem fsm;
    
    private void Awake() 
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>(); //  Model的脚本组件，Model.cs
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();  // View的脚本组件 ,View.cs 
        cameraManager = GetComponent<CameraManager>();  
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }
    private void Start()
    {
        MakeFSM();
    }

    void MakeFSM()
    {
        fsm = new FSMSystem();
        //获取所有的状态组件
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach(FSMState state in states)
        {
            fsm.AddState(state, this);
        }
        //默认状态为菜单状态
        MenuState s = GetComponentInChildren<MenuState>();
        fsm.SetCurrentState(s); 
    }
}

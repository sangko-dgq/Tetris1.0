using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
    
    private void Awake() {
        stateID = StateID.Menu;
        AddTransition(Transition.StartButtonClick, StateID.Play);
    }

//DoBeforeEntering为切换进某个状态，调用
    public override void DoBeforeEntering()
    {
        //访问和调用Menu中的ShowMenu方法
        ctrl.view.ShowMenu();
        ctrl.cameraManager.ZoomOut();
    }
    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }

    public void OnStartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.StartButtonClick);

    }
    public void OnRankButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }
    public void OnDestoryButton()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.model.ClearData();
        OnRankButtonClick();
    }
    public void OnRestartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.model.Restart();
        ctrl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StartButtonClick);
    }
}

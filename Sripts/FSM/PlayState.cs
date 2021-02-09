using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : FSMState
{
    

    private void Awake() 
    {
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }

    public override void DoBeforeEntering()  //重写FSMState类里面的DoBeforeEntering()
    {
        ctrl.view.ShowGameUI(ctrl.model.Score, ctrl.model.HighScore);
        ctrl.cameraManager.ZoomIn(); 
        ctrl.gameManager.StartGame();
    }

//当离开这个状态
     public override void DoBeforeLeaving() 
    {
        ctrl.view.HideGameUI();
        ctrl.view.ShowRestartButton();
        ctrl.gameManager.PauseGame();
    }

    public void OnPauseButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        //切换到Menus
        fsm.PerformTransition(Transition.PauseButtonClick);
    }
    public void OnRestartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.view.HideGameOverUI();
        ctrl.model.Restart();
        ctrl.gameManager.StartGame();
        //ctrl.view.UpdateGameUI(0, ctrl.model.HighScore);
    }
    

}

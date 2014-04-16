using UnityEngine;
using System.Collections;

public class GlobalGameController {

    public delegate void GameOverDel();
    public event GameOverDel gameOverEvent;

    public CameraView_t cameraView = CameraView_t.CV2D;
    public PlayerData playerData;

    private bool isGameOver = false;

    /// <summary>
    /// In GlobalManager, at this class be created , InitGameConfigure be called.
    /// </summary>
    public void InitGameConfigure() {
        Application.targetFrameRate = 60;
    }

    public bool IsGameOver() {
        return isGameOver;
    }


    public void SetGameOver() {
        isGameOver = true;
        BroadcastGameOver();
    }

    public void BroadcastGameOver() {
        gameOverEvent();
    }

    public void RetryGame() {
        isGameOver = false;

    }

    public void LoadPlayerInfo() {

    }

    public void SavePlayerInfo() {


    }

    public void LoadNewLevel( string levelName ) {
        Application.LoadLevel( levelName );
    }


    public void ChangeCameraView() {
        GameObject.FindGameObjectWithTag( "MainCamera" ).GetComponent<CameraFollowControl>().ChangeView();
    }
}

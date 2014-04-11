using UnityEngine;
using System.Collections;

public class GlobalGameController {
    public CameraView_t cameraView = CameraView_t.CV2D;
    public PlayerData playerData;

    private bool isGameOver = false;

    public bool IsGameOver() {
        return isGameOver;
    }

    public void SetGameOver() {
        isGameOver = true;
    }

    public void RetryGame() {
        isGameOver = false;

    }

    public void LoadPlayerInfo() {

    }

    public void SavePlayerInfo() {


    }
}

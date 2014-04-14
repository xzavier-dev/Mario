using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {
    void Awake() {
        GlobalManager.instance.gameStart.InitGame();
    }

    //GameLoader --> GameStart --> LevelLoader --> LevelDataWizard --> LevelLoader(Parse,Instantiate)

}

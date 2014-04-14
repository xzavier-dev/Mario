using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour{
    public void InitGame() {
        // load some basic tings , like player, or LevelLoader
        Instantiate(Resources.Load( "LevelLoader" ));
    }

}

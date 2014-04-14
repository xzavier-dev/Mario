using UnityEngine;
using System.Collections;



public class PlayerData {
    public int coins = 0;
    public int life = 0;
    PlayerSize_t playerSize = PlayerSize_t.NORMAL;
    public string currentLevelName;

    public void AddCoin() {
        ++coins;
    }

}

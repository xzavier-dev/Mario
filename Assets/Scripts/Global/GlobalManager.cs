using UnityEngine;
using System.Collections;

public class GlobalManager {
    static private GlobalManager _instance = null;
    static public GlobalManager instance {
        get {
            if ( _instance == null ) {
                _instance = new GlobalManager();
            }
            return _instance;
        }
    }


    static private GlobalGameController _globalGameController = null;
    public GlobalGameController globalGameController {
        get {
            if ( _globalGameController == null ) {
                _globalGameController = new GlobalGameController();
                Application.targetFrameRate = 60;
            }
            return _globalGameController;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Pipeline : MonoBehaviour {
    public bool isLevel = false;
    public string levelName = null;

    /// <summary>
    /// when i have the key of new level , and player stand on my top, i will give the player this key.
    /// </summary>
    /// <param name="obj"></param>
    void OnTriggerEnter( Collider obj ) {
        if ( !isLevel ) { return; }
        if ( obj.CompareTag( "Player" ) ) {
            obj.SendMessage( "SetNewLevelReady", "levelName", SendMessageOptions.DontRequireReceiver );
        }
    }
    

}

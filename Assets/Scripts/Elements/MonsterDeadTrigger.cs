using UnityEngine;
using System.Collections;

public class MonsterDeadTrigger : MonoBehaviour {
    /// <summary>
    /// When player hit me from above, then i will be killed
    /// </summary>
    /// <param name="obj"></param>
    void OnTriggerEnter(Collider obj) {
        if ( obj.CompareTag( "Player" ) ) {
            SendMessageUpwards( "BeKilled", SendMessageOptions.DontRequireReceiver );

        }

    }
}

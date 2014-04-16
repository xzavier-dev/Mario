using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

    void OnTriggerEnter( Collider obj ) {
        if ( obj.gameObject.CompareTag( "Player" ) ) {
            GlobalManager.instance.globalGameController.playerData.AddCoin();
            Destroy( gameObject );
        }
    }
}

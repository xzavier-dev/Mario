using UnityEngine;
using System.Collections;

public class CrazyFlower : MonoBehaviour {


    // kill the player
    void OnTriggerEnter( Collider obj ) {
        if ( obj.gameObject.CompareTag( "Player" ) ) {
            obj.gameObject.SendMessage( "BeKilled" );
        }
    }



}

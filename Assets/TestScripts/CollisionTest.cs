using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

    void OnCollisionEnter( Collision obj ) {
        Debug.Log( "Collision: " + obj.gameObject.name );
    }

    void OnTriggerEnter( Collider obj ) {
        Debug.Log( "Collider: " + obj.gameObject.name );
    }
}

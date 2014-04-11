using UnityEngine;
using System.Collections;

public class CollisionMessage : MonoBehaviour {

    void OnCollisionEnter() {
        Debug.Log( "Collision" );
    }
}

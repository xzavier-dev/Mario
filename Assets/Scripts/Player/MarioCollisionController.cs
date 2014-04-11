using UnityEngine;
using System.Collections;

public class MarioCollisionController : MonoBehaviour {

    private RaycastHit hit;
    private bool headCollisionLock = false;
    private bool footCollisionLock = false;
    private int lastFootCollisionObjInstantiateID = 0;
	// Update is called once per frame
	void Updatea () {
        if ( Physics.Raycast( transform.position, Vector3.up, out hit, 1.1f ) && !headCollisionLock ) {
            headCollisionLock = true;
            OnHeadCollision( hit.transform.gameObject );
        } else {
            if ( !Physics.Raycast( transform.position, Vector3.up, out hit, 1.1f ) ) {
                headCollisionLock = false;
            }
        }

        if ( Physics.Raycast( transform.position, Vector3.down, out hit, 1.1f ) && !footCollisionLock ) {
            footCollisionLock = true;
            lastFootCollisionObjInstantiateID = hit.transform.GetInstanceID();
            OnFootCollision( hit.transform.gameObject );
        } else {
            if ( !Physics.Raycast( transform.position, Vector3.down, out hit, 1.1f )
                || hit.transform.GetInstanceID() != lastFootCollisionObjInstantiateID ) {
                footCollisionLock = false;
            }
        }
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine( transform.position, transform.position + new Vector3( 0, 1.1f, 0 ) );
        Gizmos.DrawLine( transform.position, transform.position + new Vector3( 0, -1.1f, 0 ) );
    }

    void OnHeadCollision( GameObject obj ) {
         if ( obj.CompareTag( "Brick" ) ) {
             obj.gameObject.SendMessage( "BeStrike" );
         }
    }



    void OnFootCollision (GameObject obj){
        if ( obj.gameObject.CompareTag( "LazyTortoise" ) ) {
            lastCollisionHitID = lastFootCollisionObjInstantiateID;
            obj.SendMessage( "BeKilled" );
        }
        Debug.Log( "Foot Collision Detected: " + Time.time );

    }


    private int lastCollisionHitID = 0;
    void OnControllerColliderHit( ControllerColliderHit hit ) {
        if ( lastCollisionHitID == hit.gameObject.GetInstanceID() ) {
            return;
        }
        lastCollisionHitID = hit.gameObject.GetInstanceID();

        if ( hit.gameObject.CompareTag( "Coin" ) ) {

        }else if( hit.gameObject.CompareTag("LazyTortoise") ){
            GameOver();
        } else if ( hit.gameObject.CompareTag( "CrazyFlower" ) ) {
            GameOver();
        }
    }

    void GameOver() {
        GetComponent<MarioController>().enabled = false;          // when mario die , just disable input control script
        GlobalManager.instance.globalGameController.SetGameOver();

    }

}

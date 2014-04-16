using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    //public float speed = 3.0f;
    private int dir = -1;

    void Start() {
        gameObject.layer = LayerMask.NameToLayer( "Monster" );
        Physics.IgnoreLayerCollision( LayerMask.NameToLayer( "Monster" ), LayerMask.NameToLayer( "Monster" ), true );
        GlobalManager.instance.globalGameController.gameOverEvent += GameOver;
    }

    void OnDestroy() {
        GlobalManager.instance.globalGameController.gameOverEvent -= GameOver;
    }

    void OnTriggerEnter(Collider obj) {
        if ( obj.gameObject.CompareTag( "Pipeline" ) ) {
            dir = -dir;
        }
    }

    void OnCollisionEnter( Collision obj ) {
        if ( obj.gameObject.CompareTag( "Player" ) ) {
            obj.gameObject.SendMessage( "BeKilled" );
        }
    }

    void GameOver() {
        this.enabled = false;
    }


    void Update() {
        transform.position += new Vector3(dir * Time.deltaTime ,0,0);
    }
}

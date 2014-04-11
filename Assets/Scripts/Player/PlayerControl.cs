/*
================================================================================
FileName    : PlayerMoveControl.cs
Description : 
Date        : 2014-03-16
Author      : Linkrules
================================================================================
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputControl))]
public class PlayerControl : MonoBehaviour {
    public float walkingSpeed = 10;
    public float walkingAirSpeed = 2;
    public float jumpSpeed = 30;
    public float jumpIncrease = 3;
    public float gravity = -37;


    public bool isOnPipeline = false;
    public string pipelineLevelName = "";

    private PlayerInputControl _playerInput;

    private PlayerState_t _playerState;
    private CollisionFlag_t _collisionFlag;
    private float jumpStartTime = 0;
    private float fallStartTime = 0;

    private float _walkingSpeed = 0;


	void Start () {

        _playerInput = GetComponent<PlayerInputControl>();

        rigidbody.useGravity = false;
        rigidbody.freezeRotation = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

        ChangePlayerState( PlayerState_t.JUMPDOWN );
        
	
	}
	

	void Update () {

        if( _playerInput.jump ) {
            ChangePlayerState( PlayerState_t.JUMPUP );
        }

        if ( _playerInput.fire ) {
            Fire();
        }

        if ( isOnPipeline && _playerInput.yAxis < 0 ) {
            PipelineNextLevel();
        }

        if ( _playerState == PlayerState_t.GROUNDED ) {
            if ( GlobalManager.instance.globalGameController.cameraView == CameraView_t.CV2D ) {
                _walkingSpeed = walkingSpeed * _playerInput.xAxis;
            } else {
                _walkingSpeed = walkingSpeed * _playerInput.yAxis;
            }

        } else {
            if ( Mathf.Abs( walkingAirSpeed * _playerInput.xAxis ) > Mathf.Abs( rigidbody.velocity.x ) ) {
                if ( GlobalManager.instance.globalGameController.cameraView == CameraView_t.CV2D ) {
                    _walkingSpeed = walkingAirSpeed * _playerInput.xAxis;
                } else {
                    _walkingSpeed = walkingAirSpeed * _playerInput.yAxis;

                }

            }
        }

        switch( _playerState ) {
            case PlayerState_t.GROUNDED:
                rigidbody.velocity = new Vector3( _walkingSpeed, 0, 0 );
                break;
            case PlayerState_t.JUMPUP:
                rigidbody.velocity = new Vector3( _walkingSpeed, jumpSpeed + Input.GetAxis("Jump") * jumpIncrease + gravity * Mathf.Sqrt( 2*(Time.time - jumpStartTime )), 0 );
                break;
            case PlayerState_t.JUMPDOWN:
                rigidbody.velocity = new Vector3( _walkingSpeed, gravity * Mathf.Sqrt( Mathf.Abs( Time.time - fallStartTime )) * 1.2f , 0 );
                break;
        }

        
	}


    /*
    void OnGUI() {
        GUILayout.Label( _playerInput.xAxis.ToString() );
        GUILayout.Label( _walkingSpeed.ToString() );

    }
    */


    void ChangeSize() {

    }


    void Fire() {

    }

    void PipelineNextLevel() {
        // animation
        Application.LoadLevel( pipelineLevelName );

    }

    void OnCollisionEnter( Collision obj ) {
        if ( obj.gameObject.CompareTag( "Brick" ) ) {
            ChangePlayerState( PlayerState_t.GROUNDED );
        } else if ( obj.gameObject.CompareTag( "Tortoise" ) ) {
            obj.gameObject.SendMessage( "BeKilled" );
        }

    }

    void OnCollisionExit( Collision obj ) {
        if ( _playerState == PlayerState_t.GROUNDED ) {
            ChangePlayerState( PlayerState_t.JUMPDOWN );
        }
    }


    public PlayerState_t GetPlayerState() {
        return _playerState;
    }


    public void ChangePlayerState( PlayerState_t state ) {
        if( state == PlayerState_t.JUMPUP ) {
            if( _playerState != PlayerState_t.GROUNDED ) {
                return;
            }
        }

        if( _playerState == state ) {
            return;
        }

        switch( state ) {
            case PlayerState_t.GROUNDED:

                break;
            case PlayerState_t.JUMPUP:
                jumpStartTime = Time.time;
                break;
            case PlayerState_t.JUMPDOWN:
                fallStartTime = Time.time;
                break;
            case PlayerState_t.WALKING:

                break;
        }
        
        _playerState = state;
    }


    public void BeKilled() {


    }


   

}

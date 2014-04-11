using UnityEngine;
using System.Collections;

public struct TouchAxis {
    public float x;
    public float y;
    public float jump;
    public float changeView;
    public float fire;
}

public class MobileInput : MonoBehaviour {



    public TouchAxis touchAxis;


    public GUITexture button_Left;
    public GUITexture button_Right;
    public GUITexture button_Up;
    public GUITexture button_Down;
    public GUITexture button_Jump;
    public GUITexture button_ChangeView;

    public Vector2 scaleTo;
    public Vector2 scaleFrom;

    private float _left = 0;
    private float _right = 0;
    private float _up = 0;
    private float _down = 0;
    private float _jump = 0;
    private float _changeView = 0;

    private bool isLeftHit = false;
    private bool isRightHit = false;
    private bool isUpHit = false;
    private bool isDownHit = false;
    private bool isJumpHit = false;
    private bool isChangeViewHit = false;

    private float increaseRate = 6;              // axis speed
    private float scaleIncreaseRate = 10;        // scale speed

    void Start() {

    }

	// Update is called once per frame
	void Update () {
        
        isLeftHit = false;
        isRightHit = false;
        isUpHit = false;
        isDownHit = false;
        isJumpHit = false;
        isChangeViewHit = false;

        for ( int i = 0; i < Input.touchCount; ++i ) {
            Touch touch = Input.GetTouch( i );

            // left button
            if ( button_Left != null && button_Left.HitTest( touch.position ) ) {
                isLeftHit = true;
                _left += Time.deltaTime * increaseRate;
                _left = Mathf.Clamp( _left, 0, 1 );

                button_Left.pixelInset = new Rect( button_Left.pixelInset.x,
                                            button_Left.pixelInset.y,
                                            Mathf.Lerp( button_Left.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Left.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );

            } 

            // right button
            if ( button_Right != null && button_Right.HitTest( touch.position ) ) {
                isRightHit = true;
                _right += Time.deltaTime * increaseRate;
                _right = Mathf.Clamp( _right, 0, 1 );

                button_Right.pixelInset = new Rect( button_Right.pixelInset.x,
                                            button_Right.pixelInset.y,
                                            Mathf.Lerp( button_Right.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Right.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );

            } 

            // up button
            if ( button_Up != null && button_Up.HitTest( touch.position ) ) {
                isUpHit = true;
                _up += Time.deltaTime * increaseRate;
                _up = Mathf.Clamp( _up, 0, 1 );

                button_Up.pixelInset = new Rect( button_Up.pixelInset.x,
                                            button_Up.pixelInset.y,
                                            Mathf.Lerp( button_Up.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Up.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );

            }

            // down button
            if ( button_Down != null && button_Down.HitTest( touch.position ) ) {
                isDownHit = true;
                _down += Time.deltaTime * increaseRate;
                _down = Mathf.Clamp( _down, 0, 1 );

                button_Down.pixelInset = new Rect( button_Down.pixelInset.x,
                                            button_Down.pixelInset.y,
                                            Mathf.Lerp( button_Down.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Down.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
            } 

            // jump button
            if ( button_Jump != null && button_Jump.HitTest( touch.position ) ) {       
                isJumpHit = true;
                _jump = 1;

                button_Jump.pixelInset = new Rect( button_Jump.pixelInset.x,
                                            button_Jump.pixelInset.y,
                                            Mathf.Lerp( button_Jump.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Jump.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
            }

            // change view button
            if ( button_ChangeView != null && button_ChangeView.HitTest( touch.position ) ) { 
                isChangeViewHit = true;
                _changeView = 1;

                button_ChangeView.pixelInset = new Rect( button_ChangeView.pixelInset.x,
                                            button_ChangeView.pixelInset.y,
                                            Mathf.Lerp( button_ChangeView.pixelInset.width, scaleTo.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_ChangeView.pixelInset.height, scaleTo.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
            }

        }


        // reset all button
        if ( !isLeftHit ) {
            _left -= Time.deltaTime * increaseRate;
            _left = Mathf.Clamp( _left, 0, 1 );

            button_Left.pixelInset = new Rect( button_Left.pixelInset.x,
                                            button_Left.pixelInset.y,
                                            Mathf.Lerp( button_Left.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Left.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }

        if ( !isRightHit ) {
            _right -= Time.deltaTime * increaseRate;
            _right = Mathf.Clamp( _right, 0, 1 );

            button_Right.pixelInset = new Rect( button_Right.pixelInset.x,
                                            button_Right.pixelInset.y,
                                            Mathf.Lerp( button_Right.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Right.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }

        if ( !isUpHit ) {
            _up -= Time.deltaTime * increaseRate;
            _up = Mathf.Clamp( _up, 0, 1 );

            button_Up.pixelInset = new Rect( button_Up.pixelInset.x,
                                            button_Up.pixelInset.y,
                                            Mathf.Lerp( button_Up.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Up.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }

        if ( !isDownHit ) {
            _down -= Time.deltaTime * increaseRate;
            _down = Mathf.Clamp( _down, 0, 1 );

            button_Down.pixelInset = new Rect( button_Down.pixelInset.x,
                                            button_Down.pixelInset.y,
                                            Mathf.Lerp( button_Down.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Down.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }

        if ( !isJumpHit ) {
            _jump = 0;

            button_Jump.pixelInset = new Rect( button_Jump.pixelInset.x,
                                            button_Jump.pixelInset.y,
                                            Mathf.Lerp( button_Jump.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_Jump.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }


        if ( !isChangeViewHit ) {
            _changeView = 0;

            button_ChangeView.pixelInset = new Rect( button_ChangeView.pixelInset.x,
                                            button_ChangeView.pixelInset.y,
                                            Mathf.Lerp( button_ChangeView.pixelInset.width, scaleFrom.x, Time.deltaTime * increaseRate * scaleIncreaseRate ),
                                            Mathf.Lerp( button_ChangeView.pixelInset.height, scaleFrom.y, Time.deltaTime * increaseRate * scaleIncreaseRate ) );
        }




        touchAxis.jump = _jump;
        touchAxis.changeView = _changeView;

        if ( _right >= _left ) {
            touchAxis.x = _right;
        }else{
            touchAxis.x = -(_left);
        }

        if ( _up >= _down ) {
            touchAxis.y = _up;
        } else {
            touchAxis.y = -(_down);
        }

    }

            
	
    /*
    void OnGUI(){
        GUILayout.Label( touchAxis.x.ToString() );
        GUILayout.Label( touchAxis.y.ToString() );
        GUILayout.Label( touchAxis.jump.ToString() );
        GUILayout.Label( "up: " + _up );
        GUILayout.Label( "down: " + _down );
    }
     */

}

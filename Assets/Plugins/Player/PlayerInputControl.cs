using UnityEngine;
using System.Collections;

public class PlayerInputControl : MonoBehaviour {
    public float xAxis = 0;
    public float yAxis = 0;
    public bool jump = false;
    public bool fire = false;

    private bool isJumpLock = false;
    private bool isChangeViewLock = false;
    private bool isFireLock = false;

#if UNITY_ANDROID || UNITY_IPHONE
    private MobileInput mobileInput;
#endif


    void Start() {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
        GameObject mI = Instantiate( Resources.Load( "TouchButton/TouchButton" )) as GameObject;
        mobileInput = mI.GetComponent<MobileInput>();
        if ( mI == null ) {
            Debug.Log( "Null" );
        }
#endif

    }

    void Update() {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX
        
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if ( Input.GetButtonDown( "Jump" ) ) {
            jump = true;
        } else {
            jump = false;
        }

        if ( Input.GetButtonDown( "Fire1" ) ) {
            fire = true;
        } else {
            fire = false;
        }


        

#endif

#if (UNITY_ANDROID || UNITY_IPHONE) //&& UNITY_EDITOR
        xAxis = mobileInput.touchAxis.x;
        yAxis = mobileInput.touchAxis.y;

        if ( mobileInput.touchAxis.jump > 0) {
            if ( !isJumpLock ) {
                isJumpLock = true;
                jump = true;
            } else {
                jump = false;
            }
        } else {
            isJumpLock = false;
            jump = false;
        }

        if ( mobileInput.touchAxis.fire > 0 ) {
            if ( !isFireLock ) {
                isFireLock = true;
                fire = true;
            } else {
                fire = false;
            }
        } else {
            isFireLock = false;
            fire = false;
        }


        if ( mobileInput.touchAxis.changeView > 0 ) {
            if ( !isChangeViewLock ) {
                isChangeViewLock = true;
                ChangeCameraView();
            }
        } else {
            isChangeViewLock = false;
        }

        if( Input.GetKeyDown(KeyCode.Escape) ){
            Application.Quit();
        }

#endif
    }

    /// <summary>
    /// When you build to mobile platform , you should process this function specificly
    /// </summary>
    void ChangeCameraView() {

    }
   

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
    void OnGUI() {
        GUILayout.Label( mobileInput.touchAxis.x.ToString() );
        GUILayout.Label( mobileInput.touchAxis.y.ToString() );
        GUILayout.Label( mobileInput.touchAxis.jump.ToString() );
    }
#endif

    



}

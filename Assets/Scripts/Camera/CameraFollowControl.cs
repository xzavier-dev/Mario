using UnityEngine;
using System.Collections;


public class CameraFollowControl : MonoBehaviour {

    public Transform target = null;

    public CameraView_t CameraView;

    public Vector3 offset_2DV = new Vector3(0,1.3f,-5);
    public Vector3 rotate_2DV = Vector3.zero;
    public bool lockYAxis_2DV = true;

    public Vector3 offset_3DV = new Vector3( -4,1,0);
    public Vector3 rotate_3DV = new Vector3( 0, 90, 0 );
    public bool lockYAxis_3DV =  true;

    private Vector3 targetPos = Vector3.zero;
    private Quaternion targetRot = Quaternion.identity;

    private float followIncrease = 0;
    private float startTime = 0;

    void Start() {

        GlobalManager.instance.globalGameController.cameraView = CameraView;

        if ( target == null ) {
            Destroy( this );
        }
        startTime = Time.time;
    }

    void Update() {

        if ( followIncrease < 200 ) {
            followIncrease += ( Time.time - startTime ) * ( Time.time - startTime ) * Time.deltaTime * 100;
        } else {
            followIncrease = 200;
        }


                        // 2D view 
        if ( CameraView == CameraView_t.CV2D ) {

            targetPos = target.position + offset_2DV;
            targetRot = Quaternion.Euler(rotate_2DV);

            if( lockYAxis_2DV ) {
                targetPos.y = offset_2DV.y;
            }

            transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * followIncrease );
            transform.rotation = Quaternion.Lerp( transform.rotation, targetRot ,Time.deltaTime * followIncrease);

        } else {        // 3D View
            targetPos = target.position + offset_3DV;
            targetRot = Quaternion.Euler( rotate_3DV );

            if( lockYAxis_3DV ) {
                targetPos.y = offset_3DV.y;
            }

            transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * followIncrease );
            transform.rotation = Quaternion.Lerp( transform.rotation, targetRot ,Time.deltaTime * followIncrease);
        }




        // change view
        if( Input.GetKeyDown( KeyCode.C ) ) {
            ChangeView();
        }


    }


    public void ChangeView() {
        if( CameraView == CameraView_t.CV2D ) {
            CameraView = CameraView_t.CV3D;
        }
        else {
            CameraView = CameraView_t.CV2D;
        }

        startTime = Time.time;
        followIncrease = 0;

        GlobalManager.instance.globalGameController.cameraView = CameraView;
    }

}

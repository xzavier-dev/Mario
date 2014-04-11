using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public bool isPickStuff = false;
    public GameObject pickStuff = null;
    public int stuffCount = 0;

    public string pickStuffName = null;
    public bool isbreakBrick = false;

    void Start()
    {
        if (pickStuff != null)
        {
            pickStuffName = pickStuff.name;
        }
    }


    void BeStrike() {

    }




    void BreakBrick() {
        if ( isbreakBrick ) {        // break animation

        } else {                     // offset animation
            OffsetAnimation();
        }
    }

    void OffsetAnimation() {

    }

    void MadeStuff() {

        if ( stuffCount > 0 && isPickStuff && pickStuffName != null ) {
            OffsetAnimation();
            pickStuff = Spawner.GetObjectByName( pickStuffName );

            if ( pickStuff != null ) {

                GameObject pickObj = Instantiate( pickStuff ) as GameObject;
                // adjust pick object params
                pickObj.transform.position = transform.position;
                --stuffCount;
            }
        } else {
            BreakBrick();
        }

    }
           
            
}

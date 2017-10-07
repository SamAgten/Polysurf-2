using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMover : MonoBehaviour {

    public float speed;
    public TrackConstructor constructor;
    public float turnDistance;
	
	// Update is called once per frame
	void Update () {
        Vector3 translate = constructor.GetInverseDirection()*speed;
        constructor.Translate(translate);

        Quaternion initRot = constructor.GetMotherRotation(0);
        Quaternion nextRot = constructor.GetDaughterRotation(0);

        float t = (turnDistance - constructor.GetJointPosition(0).magnitude) / turnDistance;

        Quaternion inverseRot = Quaternion.Inverse(Quaternion.Slerp(initRot, nextRot, t));

        constructor.Rotate(inverseRot);
	}
}

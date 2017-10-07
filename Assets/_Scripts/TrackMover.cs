using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMover : MonoBehaviour {

    public float speed;
    public TrackConstructor constructor;
    public float turnDistance;
	
	// Update is called once per frame
	void Update () {
        Vector3 translate = constructor.GetInverseDirection();
        constructor.Translate(translate/translate.magnitude*speed);

        Quaternion initRot = constructor.GetMotherRotation(0) * Quaternion.Euler(new Vector3(-90, 0, -90));
        Quaternion nextRot = constructor.GetDaughterRotation(0) * Quaternion.Euler(new Vector3(-90, 0, -90));

        float t = Mathf.Min(1f, Mathf.Max(0, constructor.GetJointDistance(0)-turnDistance)/2f*turnDistance);

        //Debug.Log("t" + t);

        Quaternion inverseRot = Quaternion.Inverse(Quaternion.Slerp(initRot, nextRot, t));

        //constructor.Rotate(inverseRot);
	}
}

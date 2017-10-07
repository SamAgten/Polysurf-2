using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeTransformer : MonoBehaviour {
    public Transform motherJoint;
    public Transform daughterJoint;

    public void RotateJoint(float theta, float phi)
    {
        daughterJoint.Rotate(new Vector3(0f, Mathf.Rad2Deg*phi, Mathf.Rad2Deg*theta));
    }
}

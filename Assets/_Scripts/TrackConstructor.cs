using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrackConstructor : MonoBehaviour {
    public float diversity;
    public int minLength;
    public int maxLength;
    public string samplingMode = "uniform";

    public float pieceLength = 2.0f;
    public TubeTransformer tubeSegment;
    public GameObject joint;
    public int drawNumber = 5;
    public int warmupLength = 3;

    private int length;
    private bool initialized = false;

    private List<Vector2> trackTransforms = new List<Vector2>();
    public Transform trackHolder;
    private List<TubeTransformer> currentObjects = new List<TubeTransformer>();

    private int currentSegment = 0;

    private float maxAngleTheta;
    private float maxAnglePhi;

    private Vector3 nextTubePosition;

    void Start()
    {
        Initialize();
        ConstructTrackStructure();

        DrawTrack();
    }

    void Initialize() {
        maxAnglePhi = Mathf.PI / 2.0f * diversity;
        maxAngleTheta = Mathf.PI / 2.0f * diversity;

        nextTubePosition = trackHolder.position;

        GetTrackLength();

    }

    void GetTrackLength()
    {
        length = Random.Range(minLength, maxLength);
    }

    void ConstructTrackStructure()
    {
        trackTransforms.Clear();
        for (int i = 0; i < length; i++)
        {
            Vector2 orientation;
            if (i < warmupLength)
            {
                orientation = new Vector2();
            }
            else
            {
                float phi = SamplePhi(samplingMode);
                float theta = SampleTheta(samplingMode);
                orientation = new Vector2(theta, phi);
            }

            trackTransforms.Add(orientation);

        }
    }

    void DrawTrack()
    {
        Vector3 currentPos = nextTubePosition;
        if (!initialized)
        {

            for (int i = 0; i < Math.Min(drawNumber, length); i++)
            {
                Vector2 currentTrans = trackTransforms[i];

                currentPos = DrawTube(currentPos, currentTrans);

                initialized = true;
            }
        }
        else
        {
            Destroy(currentObjects[0].gameObject);
            currentObjects.Remove(currentObjects[0]);

            Vector2 currentTrans = trackTransforms[currentSegment + drawNumber];

            currentPos = DrawTube(currentPos, currentTrans);

        }

        nextTubePosition = currentPos;
    }

    Vector3 GetDirection(float theta, float phi)
    {
        return new Vector3(Mathf.Sin(theta) * Mathf.Cos(phi), Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(theta));
    }

    Vector3 DrawTube(Vector3 currentPos, Vector2 transform)
    {
        Quaternion rotation = Quaternion.AngleAxis(90, new Vector3(0,1,0));
        if (currentObjects.Count > 0)
        {
            rotation = currentObjects[currentObjects.Count - 1].daughterJoint.rotation;
        }
        TubeTransformer instanceTube = Instantiate<TubeTransformer>(tubeSegment, currentPos, Quaternion.identity);
        
        instanceTube.motherJoint.rotation = rotation;
        instanceTube.RotateJoint(transform[0], transform[1]);

        Vector3 targetPos = instanceTube.daughterJoint.position + -pieceLength / 2f * instanceTube.daughterJoint.right;

        currentObjects.Add(instanceTube);

        instanceTube.transform.SetParent(trackHolder);
        return targetPos;
    }

    public Vector3 GetInverseDirection()
    {
        Vector3 direction = currentObjects[currentSegment].transform.position - currentObjects[currentSegment + 1].transform.position;
        return direction;
    }
    public Quaternion GetDaughterRotation(int hop = 0)
    {
        return currentObjects[hop].daughterJoint.rotation;
    }

    public Quaternion GetMotherRotation(int hop = 0)
    {
        return currentObjects[hop].motherJoint.rotation;
    }

    public float GetJointDistance(int hop = 0)
    {
        return Vector3.Distance(trackHolder.transform.position, currentObjects[hop].daughterJoint.position);
    }


    public void Translate(Vector3 translation)
    {
        trackHolder.transform.Translate(translation);
        /*if (currentObjects[1].transform.position.z < 0)
        {
            currentSegment += 1;
            DrawTrack();
        }*/
    }

    public void SetRotation(Quaternion q)
    {
        trackHolder.transform.SetPositionAndRotation(trackHolder.transform.position, q);
    }

    public void Rotate(Quaternion q)
    {
        trackHolder.transform.rotation = q;
    }

    float SamplePhi(string mode)
    {
        float phi = 0;
        if (mode == "uniform")
        {
            phi = Random.Range(-maxAnglePhi, maxAnglePhi);
        } else if (mode == "gaussian")
        {
            phi = nextGaussian(0f, maxAnglePhi / 3f);
        }
        return phi;
    }
    float SampleTheta(string mode)
    {
        float theta = 0;
        if (mode == "uniform")
        {
            theta = Random.Range(0.0f, maxAngleTheta);
        }
        else if (mode == "gaussian")
        {
            theta = Mathf.Abs(nextGaussian(0.0f, maxAngleTheta/3.0f));
        }
        return theta;
    }

    float nextGaussian(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.Range(0f, 1f); //uniform(0,1] random floats
        float u2 = 1.0f - Random.Range(0f, 1f);
        float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1)) *
                     Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        float randNormal =
                     mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        return randNormal;
    }
}

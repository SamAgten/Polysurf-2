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
    public GameObject gate;
    public int drawNumber = 5;
    public int warmupTubeLength = 3;
    public int warmupGateLength = 2;

    public int nrGateColors = 2;

    private List<int> gateColors = new List<int>(); 

    public float gateDistance = 10f;

    private int length;
    private bool initialized = false;

    private List<Vector2> trackTransforms = new List<Vector2>();
    private List<Vector2> gates = new List<Vector2>();
    public Transform trackHolder;
    private List<TubeTransformer> currentTubes = new List<TubeTransformer>();
    private List<Gate> currentGates = new List<Gate>();

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

        for (int i = 0; i < nrGateColors; i++)
        {
            gateColors.Add(Random.Range(0, 3));
        }
    }

    void GetTrackLength()
    {
        length = Random.Range(minLength, maxLength);
    }

    void ConstructTrackStructure()
    {
        trackTransforms.Clear();
        currentTubes.Clear();
        for (int i = 0; i < length; i++)
        {
            Vector2 orientation;
            if (i < warmupTubeLength)
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

    void ConstructGateStructure()
    {
        currentGates.Clear();

        int nrOfGates = (int)((length-warmupGateLength) * pieceLength / gateDistance);

        for (int i = 0; i < nrOfGates; i++)
        {
            float phi = Random.Range(0f, 2f * Mathf.PI);
            int color = gateColors[Random.Range(0, gateColors.Count-1)];

            gates.Add(new Vector2(phi, color));
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
            Vector2 currentTrans = trackTransforms[Math.Min(length, currentSegment + drawNumber)];

            currentPos = DrawTube(currentPos, currentTrans);

        }

        nextTubePosition = currentPos;
    }

    void DrawGate()
    {
        Vector3 position;
    }

    Vector3 GetDirection(float theta, float phi)
    {
        return new Vector3(Mathf.Sin(theta) * Mathf.Cos(phi), Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(theta));
    }

    Vector3 DrawTube(Vector3 currentPos, Vector2 transform)
    {
        Quaternion rotation = Quaternion.AngleAxis(90, new Vector3(0,1,0));
        if (currentTubes.Count > 0)
        {
            rotation = currentTubes[currentTubes.Count - 1].daughterJoint.rotation;
        }
        TubeTransformer instanceTube;
        if (currentTubes.Count == drawNumber)
        {
            currentTubes[0].ResetJoint();
            currentTubes[0].transform.position = currentPos;

            instanceTube = currentTubes[0];
        }
        else
        {
            instanceTube = Instantiate<TubeTransformer>(tubeSegment, currentPos, Quaternion.identity);
            currentTubes.Add(instanceTube);
        }
        
        instanceTube.motherJoint.rotation = rotation;
        instanceTube.RotateJoint(transform[0], transform[1]);

        Vector3 targetPos = instanceTube.daughterJoint.position + -pieceLength / 2f * instanceTube.daughterJoint.right;

        instanceTube.transform.SetParent(trackHolder);
        return targetPos;
    }

    void DrawGate()
    {

    }

    public Vector3 GetInverseDirection()
    {
        Vector3 direction = new Vector3();
        if (currentTubes[0].motherJoint.position.z < 0)
        {
            direction = currentTubes[0].motherJoint.position - currentTubes[0].daughterJoint.position;
        }
        else
        {
            direction = currentTubes[0].daughterJoint.position - currentTubes[1].motherJoint.position;
        }
        return direction;
    }
    public Quaternion GetDaughterRotation(int hop = 0)
    {
        return currentTubes[hop].daughterJoint.rotation;
    }

    public Quaternion GetMotherRotation(int hop = 0)
    {
        return currentTubes[hop].motherJoint.rotation;
    }

    public float GetJointDistance(int hop = 0)
    {
        return Vector3.Distance(trackHolder.transform.position, currentTubes[hop].daughterJoint.position);
    }


    public void Translate(Vector3 translation)
    {
        trackHolder.transform.Translate(translation);
        if (currentTubes[1].transform.position.z < 0)
        {
            currentSegment += 1;
            //currentSegment %= drawNumber;
            DrawTrack();
        }
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

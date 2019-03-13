using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    // The position that the came will be follow
    public Transform target;
    // Smothcam
    public float _camsmooth = 0.9f;
    // Vector of the target
    Vector3 _targetoffset;

    // Use this for initialization
    void Start()
    {
        // calc offset
        _targetoffset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // create a postion the camre is aming a
        Vector3 targetCamPos = target.position + _targetoffset;
        // smooth
        transform.position = targetCamPos;
        //Debug.Log(transform.position + " " + _camsmooth * Time.deltaTime);
        _targetoffset = transform.position - target.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target.position + _targetoffset,0.25f);
    }
}

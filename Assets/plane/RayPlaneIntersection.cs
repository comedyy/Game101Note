using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPlaneIntersection : MonoBehaviour
{
    public Transform planeTrans;
    public Transform rayTrans;
    public Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        ray = new Ray(rayTrans.position, rayTrans.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ray.origin, 0.1f);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);

        var normal = planeTrans.forward;
        var p = planeTrans.position;

        var t = Vector3.Dot(p - ray.origin, normal) / Vector3.Dot(ray.direction, normal);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * t);
    }
}

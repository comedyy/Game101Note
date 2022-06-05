// AABB -> axis-aligned bounding box

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBTest : MonoBehaviour
{
    public Bounds box;
    public Ray ray;

    public Transform boxRans;
    public Transform rayTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    (Vector3, Vector3) CalIntersection()
    {
        var tMin = 0;
        var point = ray.origin + tMin * ray.direction;

        var tMinX = (box.min.x - ray.origin.x) / ray.direction.x;
        var tMinY = (box.min.y - ray.origin.y) / ray.direction.y;
        var tMinZ = (box.min.z - ray.origin.z) / ray.direction.z;

        var tMaxX = (box.max.x - ray.origin.x) / ray.direction.x;
        var tMaxY = (box.max.y - ray.origin.y) / ray.direction.y;
        var tMaxZ = (box.max.z - ray.origin.z) / ray.direction.z;

        var tMinEnterX = Mathf.Min(tMinX, tMaxX);
        var tMinEnterY = Mathf.Min(tMinY, tMaxY);
        var tMinEnterZ = Mathf.Min(tMinZ, tMaxZ);

        var tMinExitX = Mathf.Max(tMinX, tMaxX);
        var tMinExitY = Mathf.Max(tMinY, tMaxY);
        var tMinExitZ = Mathf.Max(tMinZ, tMaxZ);

        var tEnter = Mathf.Max(tMinEnterX, tMinEnterY, tMinEnterZ);
        var tExit = Mathf.Min(tMinExitX, tMinExitY, tMinExitZ);

        if(tExit > 0 && tExit > tEnter)
        {
            return (ray.origin + tEnter * ray.direction, ray.origin + tExit * ray.direction);
        }

        return (default, default);
    }

    void OnDrawGizmos()
    {
        ray = new Ray(rayTrans.position, rayTrans.forward);
        box = new Bounds(boxRans.position, boxRans.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ray.origin, 0.1f);
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(box.center, box.size);

        (var enter, var exit) = CalIntersection();
        Debug.Log(enter + " " + exit);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enter, Vector3.one * 0.3f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(exit, Vector3.one* 0.3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NewBehaviourScript : MonoBehaviour
{
    public GameObject o1;
    public GameObject o2;
    public GameObject o3;

    Vector4 f1;
    Vector4 f2;
    Vector4 f3;

    public GameObject objIntersect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var n1 = o1.transform.forward;
        var n2 = o2.transform.forward;
        var n3 = o3.transform.forward;

        var v1 = o1.transform.position;
        var v2 = o2.transform.position;
        var v3 = o3.transform.position;
        var b1 = -(v1.x * n1.x + v1.y * n1.y + v1.z * n1.z);
        var b2 = -(v2.x * n2.x + v2.y * n2.y + v2.z * n2.z);
        var b3 = -(v3.x * n3.x + v3.y * n3.y + v3.z * n3.z);

        f1 = new Vector4(n1.x, n1.y, n1.z, b1);
        f2 = new Vector4(n2.x, n2.y, n2.z, b2);
        f3 = new Vector4(n3.x, n3.y, n3.z, b3);

        Matrix4x4 m1 = new Matrix4x4(
            new Vector4(f1.x, f2.x, f3.x, 0),
            new Vector4(f1.y, f2.y, f3.y, 0),
            new Vector4(f1.z, f2.z, f3.z, 0),
            new Vector4(0, 0, 0, 1)
        );
        var ret = -1 *  m1.inverse.MultiplyVector(new Vector3(f1.w, f2.w, f3.w));
        objIntersect.transform.position = ret;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Gun : MonoBehaviour {

    LineRenderer rend;
    void Awake()
    {
        rend = GetComponent<LineRenderer>();
        rend.SetVertexCount(2);
    }

    void LateUpdate()
    {
        var origin = transform.position;
        var direction = transform.up * -1;
        var endPoint = origin + direction * 100f;
        rend.SetPosition(0, transform.position);
        rend.SetPosition(1, endPoint);
    }
}

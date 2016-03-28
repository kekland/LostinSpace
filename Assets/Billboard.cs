using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    Camera m_Cam;
    RectTransform rect;
    void Awake()
    {
        m_Cam = Camera.main;
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.LookAt(2 * rect.position - m_Cam.transform.position);
    }
}

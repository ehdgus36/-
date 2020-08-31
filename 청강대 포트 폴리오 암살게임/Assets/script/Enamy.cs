using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Enamy : MonoBehaviour
{
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 dir = Quaternion.Euler(0, -angle * 0.5f,0) * transform.forward;


        Handles.color = new Color(1, 1, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, dir, angle, 10);
    }
}

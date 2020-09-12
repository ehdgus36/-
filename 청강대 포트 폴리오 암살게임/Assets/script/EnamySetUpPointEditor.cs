using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnamySetUpPoint))]
public class EnamySetUpPointEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EnamySetUpPoint enamySetUp = (EnamySetUpPoint)target;
        if (GUILayout.Button("페트롤 포인트 생성"))
        {
            GameObject gm = new GameObject("Patrol_point"+enamySetUp.number);           
            gm.transform.SetParent(enamySetUp.transform);
            gm.transform.localPosition = new Vector3(0, 0, 0);
            enamySetUp.number += 1;
            enamySetUp.Patrol_point = enamySetUp.gameObject.GetComponentsInChildren<Transform>();
        }
    }

}

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
            for (int i = 0; i < enamySetUp.Patrol_point.Length; i++)
            {
                enamySetUp.enemy_Patrols[i].patrol_point = enamySetUp.Patrol_point[i];
            }

        }
        if (GUILayout.Button("패트롤 포인트 활성화"))
        {
            enamySetUp.enemy_Patrols = new Enemy_Patrol_Point[enamySetUp.Patrol_point.Length];
            
        }
        for (int i = 0; i < enamySetUp.enemy_Patrols.Length; i++)
        {
            enamySetUp.enemy_Patrols[i].patrol_point = enamySetUp.Patrol_point[i];
        }
    }

}

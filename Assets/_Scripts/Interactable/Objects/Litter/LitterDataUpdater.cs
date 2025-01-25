using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LitterDataUpdater : MonoBehaviour
{
    [SerializeField]
    private LitterDataList data;

    public bool TriggerOnValidate;

#if UNITY_EDITOR

    private void OnValidate()
    {
        LoadAllLitterData();
    }

    private void LoadAllLitterData()
    {
        string[] guids = AssetDatabase.FindAssets("t:LitterData", new[] { "Assets/ScriptableObjects/Litter" });
        int count = guids.Length;

        LitterData[] dataArray = new LitterData[count];

        for (int i = 0; i < count; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            dataArray[i] = AssetDatabase.LoadAssetAtPath<LitterData>(path);
        }

        data.litterData = dataArray.ToList();
        EditorUtility.SetDirty(data);
        Undo.RecordObject(data, "Updated LitterDataList");
    }

#endif
}

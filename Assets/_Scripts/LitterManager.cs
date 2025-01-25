using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

// Class to handle litter spawned in the world. Separate manager for extended functionality, mainly for AI implementation. (BH)
public class LitterManager
{
    public HashSet<Litter> _worldLitter;
    private readonly List<LitterData> _dataList;

    private Action updateUICallback;
    public LitterManager()
    {
        _worldLitter = new HashSet<Litter>();
        _dataList = GameManager.GetReferenceManager().LitterData.litterData;
    }

    public LitterData GetRandomLitterData()
    {
        if(_dataList == null || _dataList.Count == 0)
        {
            Debug.LogError("Null or empty LitterDataList. Check that the LitterDataUpdater prefab contains the LitterDataList object. If it does, toggle the checkbox");
            return null;
        }

        return _dataList[UnityEngine.Random.Range(0, _dataList.Count)];
    }
    public HashSet<Litter> GetWorldLitter()
    {
        return _worldLitter;
    }
    public int Count()
    {
        if(_worldLitter == null)
        {
            return 0;
        }

        return _worldLitter.Count;
    }
    public void ClearLitter()
    {
        if(_worldLitter == null || _worldLitter.Count == 0)
        {
            return;
        }

        foreach(Litter litter in _worldLitter)
        {
            UnityEngine.Object.Destroy(litter.gameObject);
        }

        _worldLitter.Clear();

        if (updateUICallback != null)
        {
            updateUICallback();
        }
    }
    public void AddLitter(Litter litter)
    {
        if (litter == null)
        {
            Debug.LogError("Tried to add null litter object to LitterManager.");
            return;
        }
        //if world litter reaches 200, ends the game
        if (_worldLitter.Count >= 200)
        {
            GameManager.GetScoreManager().endgame();
        }
        _worldLitter.Add(litter);

        if (updateUICallback != null)
        {
            updateUICallback();
        }
    }
    public void RemoveLitter(Litter litter)
    {
        if(_worldLitter.Contains(litter) == false)
        {
            Debug.LogError("Tried to remove unmanaged litter from LitterManager.");
            return;
        }

        _worldLitter.Remove(litter);

        if(updateUICallback != null)
        {
            updateUICallback();
        }
    }
    public void AddUpdateUICallback(Action action)
    {
        updateUICallback += action;
    }
    public void RemoveUpdateUICallback(Action action)
    {
        updateUICallback -= action;
    }
}

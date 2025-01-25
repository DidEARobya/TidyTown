using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum referenceLayers
{
    INTERACTABLE,
    AI,
    CAR,
    PLAYER,
    PROJECTILE
}
public class ReferenceManager : MonoBehaviour
{
    [SerializeField]
    private LitterDataList _data;
    public LitterDataList LitterData => _data;

    [Header("Object References")]
    [SerializeField]
    private GameObject _storeEntrance;
    public GameObject StoreEntrance => _storeEntrance;
    [SerializeField]
    private PlaceableLitter _placeableLitterPrefab;
    public PlaceableLitter PlaceableLitterPrefab => _placeableLitterPrefab;

    [SerializeField]
    private GameObject _storeExit;
    public GameObject StoreExit => _storeExit;

    [Header("AI Prefabs")]
    [SerializeField]
    private GameObject _customerPrefab;
    public GameObject CustomerPrefab => _customerPrefab;

    [SerializeField]
    private GameObject _pedestrianPrefab;
    public GameObject PedestrianPrefab => _pedestrianPrefab;

    [SerializeField]
    private GameObject _carPrefab;
    public GameObject CarPrefab => _carPrefab;

    [Header("Layer References")]
    [SerializeField]
    private LayerMask _aiLayer;
    [SerializeField]
    private LayerMask _interactableLayer;
    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _projectileLayer;
    private Dictionary<referenceLayers, LayerMask> _layerMasks;

    [Header("Count Limits")]
    [SerializeField]
    private int _maxCarCount;
    public int MaxCarCount => _maxCarCount;

    [SerializeField]
    private int _maxPedestrianCount;
    public int MaxPedestrianCount => _maxPedestrianCount;

    [SerializeField]
    private int _maxLitterAmount;
    public int MaxLitterAmount => _maxLitterAmount;

    //This needs to be handled properly, fast implementation just for a build and because the assets are unorganised.
    [SerializeField]
    private List<Mesh> _carModels;
    [SerializeField]
    private List<Material> _car1Materials;
    [SerializeField]
    private List<Material> _car2Materials;
    [SerializeField]
    private List<Material> _car3Materials;
    [SerializeField]
    private List<Material> _car4Materials;
    public void Init()
    {
        _layerMasks = new Dictionary<referenceLayers, LayerMask>();
        _layerMasks.Add(referenceLayers.AI, _aiLayer);
        _layerMasks.Add(referenceLayers.INTERACTABLE, _interactableLayer);
        _layerMasks.Add(referenceLayers.PLAYER, _playerLayer);
        _layerMasks.Add(referenceLayers.PROJECTILE, _projectileLayer);
    }
    public LayerMask GetLayerMask(referenceLayers tag)
    {
        if(_layerMasks.ContainsKey(tag) == false)
        {
            Debug.LogError("Reference manager missing layermask for " +  tag.ToString());
            return 0;
        }

        return _layerMasks[tag];
    }
    public int GetLayerFromMask(referenceLayers tag)
    {
        if (_layerMasks.ContainsKey(tag) == false)
        {
            Debug.LogError("Reference manager missing layermask for " + tag.ToString());
            return 0;
        }

        return Mathf.RoundToInt(Mathf.Log(_layerMasks[tag].value, 2));
    }
    public KeyValuePair<Mesh, Material> GetRandomCarModel()
    {
        int index = UnityEngine.Random.Range(0, _carModels.Count);
        Mesh mesh = _carModels[index];

        Material material;

        switch (index)
        {
            case 0:
                material = _car1Materials[UnityEngine.Random.Range(0, _car1Materials.Count)];
                return new KeyValuePair<Mesh, Material>(mesh, material);

            case 1:
                material = _car2Materials[UnityEngine.Random.Range(0, _car2Materials.Count)];
                return new KeyValuePair<Mesh, Material>(mesh, material);

            case 2:
                material = _car3Materials[UnityEngine.Random.Range(0, _car3Materials.Count)];
                return new KeyValuePair<Mesh, Material>(mesh, material);

            case 3:
                material = _car4Materials[UnityEngine.Random.Range(0, _car4Materials.Count)];
                return new KeyValuePair<Mesh, Material>(mesh, material);
        }

        return new KeyValuePair<Mesh, Material>(null, null);
    }
}

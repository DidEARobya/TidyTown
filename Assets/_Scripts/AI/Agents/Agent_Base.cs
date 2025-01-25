using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_Base : MonoBehaviour
{
    [SerializeField]
    protected int pathSearchRadius;
    protected LayerMask layerMask;
    public LayerMask LayerMask => layerMask;
    protected LitterDropper litterDropper;
    protected Seeker seeker;

    protected AIType type;
    protected AI_SpawnPoint spawnPoint;

    protected void Start()
    {
        layerMask = GameManager.GetReferenceManager().GetLayerMask(referenceLayers.AI);

        if (gameObject.GetComponent<LitterDropper>() != null)
        {
            litterDropper = gameObject.GetComponent<LitterDropper>();
        }
        else
        {
            litterDropper = gameObject.AddComponent<LitterDropper>();
        }

        if (gameObject.GetComponent<Seeker>() != null)
        {
            seeker = gameObject.GetComponent<Seeker>();
        }
        else
        {
            seeker = gameObject.AddComponent<Seeker>();
        }

        seeker.Init();
    }
    protected void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        GameManager.GetAISpawnManager().Decrement(type);
    }
    public void SetSpawnPoint(AI_SpawnPoint sP)
    {
        spawnPoint = sP;
    }
}

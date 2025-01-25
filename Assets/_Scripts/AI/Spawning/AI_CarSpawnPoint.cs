using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CarSpawnPoint : AI_SpawnPoint
{
    [SerializeField]
    private PathPlan _carPath;

    protected new void Start()
    {
        GameManager.GetAISpawnManager().AddSpawnPoint(this, AIType.CAR);
    }
    public override void SpawnAgent(GameObject prefab)
    {
        isUsed = true;

        GameObject agent = Instantiate(prefab, transform.position, transform.rotation);
        agent.GetComponent<Agent_Car>().SetSpawnPoint(this);
        agent.GetComponent<Agent_Car>().InitPath(_carPath);
    }
}

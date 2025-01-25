using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SpawnPoint : MonoBehaviour
{
    [SerializeField]
    protected AIType spawnPointType;

    public bool isActive = false;
    public bool isUsed = false;

    protected void Start()
    {
        GameManager.GetAISpawnManager().AddSpawnPoint(this, spawnPointType);
    }

    public void SpawnCustomer(GameObject prefab, Agent_Car car)
    {
        if (spawnPointType == AIType.CUSTOMER && isUsed == true)
        {
            Debug.LogError("SpawnPoint in use, check logic.");
            return;
        }

        isUsed = true;

        GameObject agent = Instantiate(prefab, transform.position, transform.rotation);
        agent.GetComponent<Agent_Customer>().Init(car);
        agent.GetComponent<Agent_Customer>().SetSpawnPoint(this);
    }
    public virtual void SpawnAgent(GameObject prefab)
    {
        isUsed = true;

        GameObject agent = Instantiate(prefab, transform.position, transform.rotation);
        agent.GetComponent<Agent_Base>().SetSpawnPoint(this);
    }
}

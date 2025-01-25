using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLitterDropper : LitterDropper
{
    [SerializeField]
    private int delayMin;
    [SerializeField]
    private int delayMax;

    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = Random.Range(delayMin, delayMax);
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer <= 0)
        {
            DropLitter();
            _timer = Random.Range(delayMin, delayMax);
        }
    }
}

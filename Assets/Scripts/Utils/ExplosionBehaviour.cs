using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float radiusDoor = 10f; 
    [SerializeField] private float radiusCore = 3f;

    public void Explodes()
	{
        List<CharacterCore> cores = FindObjectsOfType<CharacterCore>().ToList();
        foreach (CharacterCore core in cores)
        {
            if (Vector3.Distance(transform.position, core.transform.position) < radiusCore)
                core.Kill((core.transform.position - transform.position).normalized * 1000, true);
            if (Vector3.Distance(transform.position, core.transform.position) < radiusDoor)
                core.Kill((core.transform.position - transform.position).normalized * 200, false);

        }
        List<BigDoors> bigDoors = FindObjectsOfType<BigDoors>().ToList();
        foreach(BigDoors door in bigDoors)
		{
            if (Vector3.Distance(transform.position, door.transform.position) < radiusDoor)
                door.Kill();
        }

        gameObject.SetActive(false);
        gameObject.SetActive(true);

    }
    
}

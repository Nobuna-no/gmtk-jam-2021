using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 2f; 
    
    public void Explodes()
	{
        List<CharacterCore> cores = FindObjectsOfType<CharacterCore>().ToList();
        foreach (CharacterCore core in cores)
        {
            if (Vector3.Distance(transform.position, core.transform.position) < radius)
                core.Kill((core.transform.position - transform.position) * 10000);
        }
        List<BigDoors> bigDoors = FindObjectsOfType<BigDoors>().ToList();
        foreach(BigDoors door in bigDoors)
		{
            if (Vector3.Distance(transform.position, door.transform.position) < radius)
                door.Kill();
        }

        gameObject.SetActive(false);
        gameObject.SetActive(true);

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFXAtPosition : MonoBehaviour
{
    [SerializeField] private GameObject fxToSpawn;
    
    public void Spawn()
	{
        Instantiate(fxToSpawn, transform.position, Quaternion.identity);
	}
}

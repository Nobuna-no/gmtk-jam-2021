using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BigDoors : MonoBehaviour
{
    private UnityEvent OnDeath;

    public void Kill()
	{
		OnDeath?.Invoke();
		Destroy(gameObject);
	}
}

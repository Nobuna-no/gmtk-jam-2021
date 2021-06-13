using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ProximityTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onPlayerCoreTriggerEnter;
    [SerializeField]
    private UnityEvent onPlayerCoreTriggerExit;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField]
    private bool debug_trigger = false;
#endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterCore character = collision.GetComponent<CharacterCore>();
        
        if (character != null)
        {
            onPlayerCoreTriggerEnter?.Invoke();
#if UNITY_EDITOR
            debug_trigger = true;
#endif
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CharacterCore character = collision.GetComponent<CharacterCore>();
        
        if (character != null)
        {
            onPlayerCoreTriggerExit?.Invoke();
#if UNITY_EDITOR
            debug_trigger = false;
#endif
        }
    }
}

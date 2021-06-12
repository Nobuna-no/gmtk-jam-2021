using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterCore character = collision.GetComponent<CharacterCore>();

        if (character != null && character.IsAlive)
        {
            character.Kill();
        }
    }
}
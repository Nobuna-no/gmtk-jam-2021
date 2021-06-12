using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deadzone : MonoBehaviour
{
    [SerializeField]
    private float deathDelay = 1f;
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    private UnityEvent OnDeath;
    [SerializeField]
    private UnityEvent OnRespawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICharacter character = collision.GetComponent<ICharacter>();

        if (character != null && !character.IsDead())
        {
            character.Kill();
            this.StartCoroutine(RespawnCoroutine(character));
        }
    }

    IEnumerator RespawnCoroutine(ICharacter character)
    {
        this.OnDeath?.Invoke();
        yield return new WaitForSeconds(this.deathDelay);
        this.OnRespawn?.Invoke();
        character.Respawn(this.respawnPoint.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class CharacterCore : MonoBehaviour
{
    [SerializeField]
    private AbstractCharacter fishActor;
    [SerializeField]
    private AbstractCharacter squidActor;

    [SerializeField]
    private float deathDelay = 1f;
    [SerializeField]
    private UnityEvent OnDeath;
    [SerializeField]
    private UnityEvent onRespawn;

    private Checkpoint lastCheckpoint;


    private bool isAlive = true;
    public bool IsAlive => isAlive;


    private void Update()
    {
        this.transform.position = 0.5f * (fishActor.transform.position + squidActor.transform.position);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsAlive)
        {
            return;
        }

        Checkpoint ck = collision.GetComponent<Checkpoint>();

        if (ck != null)
        {
            this.lastCheckpoint = ck;
        }
    }
    public void Kill()
    {
        if (!this.IsAlive)
        {
            return;
        }

        this.isAlive = false;

        this.fishActor.Kill();
        this.squidActor.Kill();
        this.OnDeath?.Invoke();

        StartCoroutine(Respawn_Coroutine());
    }

    public void Kill(Vector2 impulse, bool dead)
	{
        this.fishActor.GetComponent<Rigidbody2D>()?.AddForce(impulse);
        this.squidActor.GetComponent<Rigidbody2D>()?.AddForce(impulse);
        if (dead) Kill();
    }

    IEnumerator Respawn_Coroutine()
    {
        yield return new WaitForSeconds(this.deathDelay);
        this.onRespawn?.Invoke();
        this.isAlive = true;

        this.fishActor.Respawn(lastCheckpoint.transform.position + Vector3.right);
        this.squidActor.Respawn(lastCheckpoint.transform.position + Vector3.left);
    }
}

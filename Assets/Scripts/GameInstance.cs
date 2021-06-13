using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;

    public UnityEngine.Events.UnityAction PlaySolo;
    public UnityEngine.Events.UnityAction PlayDuo;

    public float startTime { get; private set; } = 0f;
    private bool solo = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    public void StartGameplay()
    {
        if (solo)
            PlaySolo();
        else
            PlayDuo();

        startTime = Time.time;
    }

    public void SetSolo(bool solo)
    {
        this.solo = solo;
    }
}

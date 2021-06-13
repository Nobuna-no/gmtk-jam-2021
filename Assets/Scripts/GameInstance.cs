using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance;

    public UnityEngine.Events.UnityAction PlaySolo;
    public UnityEngine.Events.UnityAction PlayDuo;

    private bool solo = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void StartGameplay()
    {
        if (solo)
            PlaySolo();
        else
            PlayDuo();
    }

    public void SetSolo(bool solo)
    {
        this.solo = solo;
    }
}

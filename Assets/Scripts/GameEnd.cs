using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] UnityEngine.UI.Image fadeImage;
    [SerializeField] UnityEngine.UI.Text text;

    [SerializeField, Min(0f)] float fadeDelay;
    [SerializeField, Min(0f)] float fadeDuration;
    [SerializeField, Min(0f)] float delayBeforeReturnToMenu;

    void Awake()
    {
        canvas.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<CharacterCore>()?.DisableControls();
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        float gameDuration = Time.time - GameInstance.Instance.startTime;
        
        yield return new WaitForSeconds(fadeDelay);

        canvas.SetActive(true);

        text.text = "You cleared the game in ";

        // Stylax
        int minutes = (int)(gameDuration / 60f);
        int seconds = (int)(gameDuration % 60f);

        if (minutes < 1)
        {
            text.text += seconds + " seconds!";
        }
        else
        {
            text.text += minutes + " minute";
            if (minutes > 1)
                text.text += "s";
            if (seconds > 0)
            {
                text.text += " and " + seconds + " second";
                if (seconds > 1)
                    text.text += "s";
            }
        }

        while (fadeImage.color.a < 1f)
        {
            Color c = fadeImage.color;
            c.a = Mathf.Clamp(c.a + Time.deltaTime / fadeDuration, 0f, 1f);
            fadeImage.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeReturnToMenu);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] CharacterCore core;
    [SerializeField] UnityEngine.UI.Image fadeImage;

    [SerializeField, Min(0f)] float fadeDelay;
    [SerializeField, Min(0f)] float fadeDuration;

    void OnTriggerEnter2D(Collider2D collider)
    {
        core.Kill();
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(fadeDelay);

        while (fadeImage.color.a < 1f)
        {
            Color c = fadeImage.color;
            float v = Mathf.Clamp(c.a + Time.deltaTime / fadeDuration, 0f, 1f);
            print("c.a="+c.a+", t="+Time.deltaTime+", fadeDur="+fadeDuration+"result unclamped="+ (c.a + Time.deltaTime / fadeDuration+"result clamped="+v));
            c.a = v;
            fadeImage.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(fadeDuration);

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}

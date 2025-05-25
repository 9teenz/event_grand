using UnityEngine;
using System.Collections;

public class NotificationLog : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public CanvasGroup panelGroup; 
    public Transform contentParent;

    [Header("Префабы уведомлений")]
    public GameObject notifInsufficientFunds;
    public GameObject notifAddedToInventory;

    [Header("Настройки")]
    public float fadeDuration = 0.3f;
    public float showTime = 2f;

    private Coroutine fadeRoutine;

    public void ShowInsufficientFunds()
    {
        ShowPanel();
        Instantiate(notifInsufficientFunds, contentParent);
    }

    public void ShowAddedToInventory()
    {
        ShowPanel();
        Instantiate(notifAddedToInventory, contentParent);
    }

    private void ShowPanel()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadePanel());
    }

    private IEnumerator FadePanel()
    {
        panel.SetActive(true);
        float t = 0;

       
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        panelGroup.alpha = 1;
        yield return new WaitForSeconds(showTime);

        
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            panelGroup.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        panelGroup.alpha = 0;
        panel.SetActive(true);
    }
}

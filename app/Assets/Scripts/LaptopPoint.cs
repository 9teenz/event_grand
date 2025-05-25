using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class LaptopPoint : MonoBehaviour
{
    [Header("Имя сцены браузера (без .unity)")]
    public string browserSceneName = "Browser";

    private bool playerInRange = false;

    void Awake()
    {
        // Убедимся, что наш Collider – триггер
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            OpenBrowser();
    }

    private void OpenBrowser()
    {
        // Сохраняем позицию и поворот игрока
        var player = GameObject.FindGameObjectWithTag("Player");
        GameSession.Instance.savedPosition = player.transform.position;
        GameSession.Instance.savedRotation = player.transform.rotation;

        // Отключаем управление и курсор
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<ThirdPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Загрузим сцену браузера поверх текущей
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(browserSceneName, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != browserSceneName)
            return;

        // Удаляем все EventSystem из только что загруженной сцены
        foreach (var es in FindObjectsOfType<EventSystem>())
            if (es.gameObject.scene == scene)
                Destroy(es.gameObject);

        // Удаляем все AudioListener из этой же сцены
        foreach (var al in FindObjectsOfType<AudioListener>())
            if (al.gameObject.scene == scene)
                Destroy(al);

        // Больше не нуждаемся в этом обработчике
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Вызывается нажатием на кнопку «Закрыть» внутри BrowserScene
    /// </summary>
    public void CloseBrowser()
    {
        // Выгружаем аддитивную сцену
        SceneManager.UnloadSceneAsync(browserSceneName);

        // Возвращаем управление персонажем и курсор
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<ThirdPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

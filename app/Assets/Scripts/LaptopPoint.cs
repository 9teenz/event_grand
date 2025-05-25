using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class LaptopPoint : MonoBehaviour
{
    [Header("��� ����� �������� (��� .unity)")]
    public string browserSceneName = "Browser";

    private bool playerInRange = false;

    void Awake()
    {
        // ��������, ��� ��� Collider � �������
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
        // ��������� ������� � ������� ������
        var player = GameObject.FindGameObjectWithTag("Player");
        GameSession.Instance.savedPosition = player.transform.position;
        GameSession.Instance.savedRotation = player.transform.rotation;

        // ��������� ���������� � ������
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<ThirdPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // �������� ����� �������� ������ �������
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(browserSceneName, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != browserSceneName)
            return;

        // ������� ��� EventSystem �� ������ ��� ����������� �����
        foreach (var es in FindObjectsOfType<EventSystem>())
            if (es.gameObject.scene == scene)
                Destroy(es.gameObject);

        // ������� ��� AudioListener �� ���� �� �����
        foreach (var al in FindObjectsOfType<AudioListener>())
            if (al.gameObject.scene == scene)
                Destroy(al);

        // ������ �� ��������� � ���� �����������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// ���������� �������� �� ������ ��������� ������ BrowserScene
    /// </summary>
    public void CloseBrowser()
    {
        // ��������� ���������� �����
        SceneManager.UnloadSceneAsync(browserSceneName);

        // ���������� ���������� ���������� � ������
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<ThirdPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

// GameSession.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    // ���� ��������, ��� ����� �����
    public Vector3 savedPosition;
    public Quaternion savedRotation;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // ����������, ����� ��� �������� � ������� ����� ���������������
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ����� ����������� ���� �������� ������� ����� ("MainScene"), ���������� ������
        if (scene.name == "MainScene")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = savedPosition;
                player.transform.rotation = savedRotation;
            }
        }
    }
}

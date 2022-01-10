using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TechManager : MonoBehaviour
{
    public static TechManager s_inst;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        s_inst = this;
        SceneManager.LoadScene("MenuScene");
#if UNITY_SERVER
        LoadStateGame(Static.StateNetManager.Server, "GameScene");
#endif
    }
    public void LoadStateGame(Static.StateNetManager state, string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, state));
    }
    private IEnumerator LoadSceneAsync(string sceneName, Static.StateNetManager state)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
        switch (state)
        {
            case Static.StateNetManager.Client:
                NetworkManager.Singleton.StartClient();
                break;
            case Static.StateNetManager.Host:
                NetworkManager.Singleton.StartHost();
                break;
            case Static.StateNetManager.Server:
                NetworkManager.Singleton.StartServer();
                break;
            case Static.StateNetManager.Shutdown:
                NetworkManager.Singleton.Shutdown();
                break;
        }
    }
}

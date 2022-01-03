using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Transports.UNET;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menu, direct;
    [SerializeField] private Button backBtn, directBtn, playMenuBtn, playDirectBtn, hostBtn;
    [SerializeField] private TMP_InputField inputField;
    private UNetTransport netTransport;

    private void Awake()
    {
        netTransport = GameObject.FindGameObjectWithTag("Net").GetComponent<UNetTransport>();
        inputField.onValueChanged.AddListener(ChangeIpAdress);
        backBtn.onClick.AddListener(ChangeViewDirect);
        directBtn.onClick.AddListener(ChangeViewDirect);
        playMenuBtn.onClick.AddListener(Client);
        playDirectBtn.onClick.AddListener(Client);
        hostBtn.onClick.AddListener(Host);
    }
    private void ChangeIpAdress(string ip)
    {
        netTransport.ConnectAddress = ip;
    }
    private void ChangeViewDirect()
    {
        menu.SetActive(!menu.activeSelf);
        direct.SetActive(!direct.activeSelf);
    }
    private void Client()
    {
        TechManager.s_inst.LoadStateGame(Static.StateNetManager.Client, "GameScene");
    }
    private void Host()
    {
        TechManager.s_inst.LoadStateGame(Static.StateNetManager.Host, "GameScene");
    }
}

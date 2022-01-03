using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Button leaveBtn;
    [SerializeField] private GameObject settingsPanel;
    private void Awake()
    {
        leaveBtn.onClick.AddListener(Leave);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
    private void Leave()
    {
        TechManager.s_inst.LoadStateGame(Static.StateNetManager.Shutdown, "MenuScene");
    }
}

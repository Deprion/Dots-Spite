using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject PrimaryAttack;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int defense;

    public void TakeAttack(int attack)
    {
        health -= attack - defense;
        if (health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        TechManager.s_inst.LoadStateGame(Static.StateNetManager.Shutdown, "MenuScene");
    }
    public int GetAttack()
    {
        return attack;
    }
    public GameObject GetPrimaryAttack() => PrimaryAttack;
}

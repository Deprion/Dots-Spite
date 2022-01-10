using Unity.Netcode;
using UnityEngine;

public class PlayerAction : NetworkBehaviour
{
    private Camera mainCamera;
    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (!IsOwner) return ;
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * Time.deltaTime;
        if (moveX != 0 || moveY != 0)
        {
            MovePlayerServerRpc(new Vector3(moveX, moveY));
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 dir = (mainCamera.ScreenToWorldPoint(Input.mousePosition)
                - transform.localPosition).normalized;
            dir.z = 0;
            print(dir);
            dir.x *= 4;
            dir.y *= 4;
            /*if (dir.x > 0) dir.x += 1;
            else dir.x = -1;
            if (dir.y > 0) dir.y = 1;
            else dir.y = -1;*/
            Vector3 pos = new Vector3(transform.localPosition.x, transform.localPosition.y)
                + dir;
            PrimaryAttackServerRpc(pos, dir);
        }
    }
    [ServerRpc]
    private void MovePlayerServerRpc(Vector3 move) 
    {
        transform.localPosition += move;
        MovePlayerClientRpc(transform.localPosition);
    }
    [ClientRpc]
    private void MovePlayerClientRpc(Vector3 pos)
    {
        transform.localPosition = pos;
    }
    [ServerRpc]
    private void PrimaryAttackServerRpc(Vector3 pos, Vector3 normalize)
    {
        Instantiate(player.GetPrimaryAttack(), pos, Quaternion.Euler(normalize));
        PrimaryAttackClientRpc(pos, normalize);
    }
    [ClientRpc]
    private void PrimaryAttackClientRpc(Vector3 pos, Vector3 normalize)
    {
        if (IsHost || IsServer) return;
        Instantiate(player.GetPrimaryAttack(), pos, Quaternion.Euler(normalize));
    }
}

using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private void FixedUpdate()
    {
        if (!IsOwner) return ;
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        if (moveX != 0 || moveY != 0)
        {
            MovePlayerServerRpc(new Vector3(moveX, moveY));
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
}

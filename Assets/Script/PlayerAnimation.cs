using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (player == null) { return; }

        transform.position = player.transform.position; 

        transform.rotation = player.transform.rotation;
    }
}

using UnityEngine;

public class WatchPlayerBehaviour : MonoBehaviour
{
    private GameObject player;
    public bool Continues;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform.position);
        }
    }

    private void Update()
    {
        if (Continues)
        {
            transform.LookAt(player.transform.position);
        }
    }
}
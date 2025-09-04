using UnityEngine;

public class BadBallon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.transform.parent.tag == "Player")
        {
            Debug.Log("Ballon collected!");
            player.GetComponentInParent<vie>().takeDamage(1);
            Destroy(gameObject);
        }
    }
}

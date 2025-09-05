using Unity.VisualScripting;
using UnityEngine;

public class GoodBallon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject chapeau = collision.gameObject;
        if (chapeau.transform.tag == "Chapeau")
        {
            Debug.Log("Ballon collected!");
            chapeau.GetComponent<Chapeau>().Player.GetComponentInParent<vie>().takeDamage(-1);
            Destroy(gameObject);
        }
    }
}

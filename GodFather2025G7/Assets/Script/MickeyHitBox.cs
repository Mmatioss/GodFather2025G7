using UnityEngine;

public class MickeyHitBox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("ok");
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<vie>().takeDamage(1);
            print("Hit Player");
        }
    }
}

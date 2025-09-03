using UnityEngine;
using System.Collections;

public class lancer : MonoBehaviour
{
    public GameObject ChapPrefab;
    public float LancerMax; //Puissance max du lancer
    public float ForceDistance;
    public bool _CanLancer = false;
    public Collision2D _collision;
    void Start()
    {

    }

    void Update()
    {
        lancerchap();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _CanLancer = true;
    }

    private void lancerchap()
    {
        if (_CanLancer)
        {
            // Pour la souris (clic droit)
            bool lancerInput = Input.GetMouseButtonUp(1);

            // Pour la manette (c'est A là)
            lancerInput |= Input.GetButtonUp("Fire1");

            if (lancerInput)
            {
                Vector2 direction;
                float puissance;

                // Si manette utilisée (prioritaire si joystick déplacé)
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                Vector2 stickDir = new Vector2(h, v);

                if (stickDir.magnitude > 0.1f)
                {
                    direction = stickDir.normalized;
                    puissance = LancerMax;
                }
                else
                {
                    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    cursorPosition.z = 0;
                    direction = (cursorPosition - transform.position).normalized;
                    float distance = Vector2.Distance(cursorPosition, transform.position);
                    distance = Mathf.Min(distance, LancerMax);
                    puissance = Mathf.Lerp(0, LancerMax, distance / ForceDistance);
                }

                // Offset devant le joueur
                float spawnOffset = 1.5f; // Distance devant le joueur (ajuste selon besoin)
                Vector3 spawnPosition = transform.position + (Vector3)(direction * spawnOffset);

                Rigidbody2D rock = Instantiate(ChapPrefab, spawnPosition, Quaternion.identity).GetComponent<Rigidbody2D>();
                rock.gameObject.GetComponent<RockController>().lancer_player = this;

                rock.AddForce(direction * puissance, ForceMode2D.Impulse);
                _CanLancer = false;
            }
        }
    }

}

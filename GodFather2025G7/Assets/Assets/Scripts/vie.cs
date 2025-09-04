using UnityEngine;
using System.Collections.Generic;

public class vie : MonoBehaviour
{
    public int viejoueur = 3;
    public int vieMax = 3;
    public Transform coeurContainer; // Un empty placé au-dessus de la tête du joueur
    public Sprite coeurRouge;
    public Sprite coeurGris;
    private List<SpriteRenderer> coeurs = new List<SpriteRenderer>();

    void Start()
    {
        for (int i = 0; i < vieMax; i++)
        {
            GameObject coeurObj = new GameObject("Coeur" + i);
            coeurObj.transform.SetParent(coeurContainer);
            coeurObj.transform.localPosition = new Vector3(i * 0.8f, 0, 0); // espace les cœurs
            SpriteRenderer sr = coeurObj.AddComponent<SpriteRenderer>();
            sr.sprite = coeurRouge;
            coeurs.Add(sr);
        }
        updateVie();
    }

    void takeDamage(int damage)
    {
        viejoueur -= damage;
        viejoueur = Mathf.Clamp(viejoueur, 0, vieMax);
        updateVie();
        if (viejoueur <= 0)
        {
            jesuisMouru();
        }
    }

    void updateVie()
    {
        for (int i = 0; i < coeurs.Count; i++)
        {
            coeurs[i].sprite = i < viejoueur ? coeurRouge : coeurGris;
        }
    }

    void jesuisMouru()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ballon"))
        {
            takeDamage(1);
        }
    }
}

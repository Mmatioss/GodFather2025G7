using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int NbOfPlayer = 0;

    public TextMeshProUGUI textInfo;

    public List<Sprite> Sprites = new List<Sprite>();

    public List<GameObject> toActivate = new();

    public GameObject chapal;


    public AudioSource lamere;
    public AudioClip musiqueToPlay;
    public AudioSource siffler;



    bool hasStart = false;

    MovementScript[] players = new MovementScript[4];
    public void OnPlayerJoined()
    {
        NbOfPlayer++;

        if(NbOfPlayer == 2 && !hasStart)
        {
            hasStart = true;
            textInfo.text = "Press Start !";
        }

        players = FindObjectsByType<MovementScript>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i > players.Length-1; i--)
        {
            players[NbOfPlayer].GetComponent<SpriteRenderer>().sprite = Sprites[i];
        }

    }
    public void StartGame()
    {
        Instantiate(chapal);
        siffler.Play();

        lamere.clip = musiqueToPlay;    
        lamere.PlayDelayed(2);

        foreach (GameObject g in toActivate)
        {
            g.SetActive(true);
        }

        textInfo.text = "";
    }
    private void Update()
    {
        textInfo.color = new Color(255, 255, 255, Mathf.Lerp(0, 1, Mathf.Sin(Time.time) + .8f));
    }
}

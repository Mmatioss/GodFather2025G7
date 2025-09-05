using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

    Dictionary<int, int> controllers = new();

    public Timer tilmer;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        if (tilmer._currentTime >= 150)
        {
            Destroy(playerInput);
            return;
        }

        int index = NbOfPlayer;
        if (controllers.ContainsKey(playerInput.devices[0].deviceId))
        {
            index = controllers[playerInput.devices[0].deviceId];
        }
        else
        {
            controllers.Add(playerInput.devices[0].deviceId, NbOfPlayer);
            playerInput.GetComponent<MovementScript>().PlayerID = NbOfPlayer;
            NbOfPlayer++;
        }



        print(playerInput.devices[0].deviceId);


        playerInput.GetComponent<SpriteRenderer>().sprite = Sprites[index];



        if (NbOfPlayer == 2 && !hasStart)
        {
            textInfo.text = "Press Start !";
        }
    }

    public void OnPlayerLeft(PlayerInput p)
    {
    }

    public void StartGame()
    {
        if (hasStart) return;
        hasStart = true;
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

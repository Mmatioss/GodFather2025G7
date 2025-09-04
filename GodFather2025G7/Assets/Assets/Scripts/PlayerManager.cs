using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int NbOfPlayer = 0;

    public TextMeshProUGUI textInfo;

    public List<Sprite> Sprites = new List<Sprite>();

    public GameObject chapal;

    public void OnPlayerJoined()
    {
        NbOfPlayer++;

        if(NbOfPlayer >= 2)
        {
            textInfo.text = "Press Start !";
        }

        int i = 0;
        foreach(MovementScript m in FindObjectsByType<MovementScript>(FindObjectsSortMode.InstanceID))
        {
            m.GetComponent<SpriteRenderer>().sprite = Sprites[i++];
        }

    }
    public void StartGame()
    {
        Instantiate(chapal);
        textInfo.text = "";
    }
    private void Update()
    {
        textInfo.color = new Color(255, 255, 255, Mathf.Lerp(0, 1, Mathf.Sin(Time.time) + .8f));
    }
}

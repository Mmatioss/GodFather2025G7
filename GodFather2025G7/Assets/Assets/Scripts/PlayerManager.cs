using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int NbOfPlayer = 0;

    public TextMeshProUGUI textInfo;
    public void OnPlayerJoined()
    {
        NbOfPlayer++;

        if(NbOfPlayer >= 2)
        {
            textInfo.text = "Press Start !";
        }

    }

    private void Update()
    {
        textInfo.color = new Color(255, 255, 255, Mathf.Lerp(1, 0, Mathf.Sin(Time.time) + .7f));
    }
}

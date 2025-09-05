using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Compteur : MonoBehaviour
{
    public static Compteur instance;

    public Dictionary<int, int> controllers = new();

    public TextMeshProUGUI text;

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }   

    
    public void Scores()
    {
        var maxKey = controllers.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;



        text.text = "Player " + maxKey + " win! Score : " + controllers[maxKey];
    }

}

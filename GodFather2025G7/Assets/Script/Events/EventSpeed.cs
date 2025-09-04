using UnityEngine;

public class event_speed : ParentEvent
{
    private float _originalSpeed;
    public lancer lancerScript;

    void Start()
    {
        lancerScript = FindFirstObjectByType<lancer>();
        if (lancerScript != null)
        {
            _originalSpeed = lancerScript.LancerMax;
        }
        else
        {
            Debug.LogError("lancerScript n'est pas assigné dans event_speed");
        }
    }

    public void DoEvent()
    {
        // Génère une valeur aléatoire à ajouter
        int bonus = Random.Range(20, 50);
        lancerScript.LancerMax += bonus;
        Debug.Log("Event Speed! Bonus: " + bonus + " | Nouveau lance_max: " + lancerScript.LancerMax);
        Invoke("EndEvent", 10f); // Appelle EndEvent après 10 secondes
    }
    
    void EndEvent()
    {
        // Réinitialise la vitesse de lancer à sa valeur originale
        lancerScript.LancerMax = _originalSpeed;
        Debug.Log("Fin de l'event Speed. Vitesse de lancer réinitialisée à: " + _originalSpeed);
    }
    
    
}

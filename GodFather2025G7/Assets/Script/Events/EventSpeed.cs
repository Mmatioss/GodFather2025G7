using UnityEngine;

public class event_speed : ParentEvent
{
    private float _originalSpeed;
    public lancer lancerScript;

    [Header("Chapeau Animation")]
    public GameObject chapeauObject; 
    private Animator chapeauAnimator;

    void Start()
    {
        lancerScript = FindFirstObjectByType<lancer>();
        if (lancerScript != null)
        {
            _originalSpeed = lancerScript.ForceDistance;
        }
        else
        {
            Debug.LogError("lancerScript n'est pas assigné dans event_speed");
        }

        if (chapeauObject != null)
        {
            chapeauAnimator = chapeauObject.GetComponent<Animator>();
            if (chapeauAnimator == null)
                Debug.LogError("Animator non trouvé sur le chapeau");
        }
        else
        {
            Debug.LogError("chapeauObject n'est pas assigné dans event_speed");
        }
    }

    public void DoEvent()
{
    var players = FindObjectsByType<lancer>(FindObjectsSortMode.None);

    foreach(lancer p in players)
    {
        lancerScript = p;
        if (lancerScript != null)
        {
            _originalSpeed = lancerScript.ForceDistance;
            speed();
        }
        else
        {
            Debug.LogError("lancerScript n'est pas assigné dans event_speed");
        }
    }

}
    
    public void speed()
    {

        int bonus = Random.Range(50, 100);
        lancerScript.ForceDistance += bonus;
        Debug.Log("Event Speed! Bonus: " + bonus + " | Nouveau lance_max: " + lancerScript.ForceDistance);

        // Lance l'animation du chapeau
        if (chapeauAnimator != null)
            chapeauAnimator.SetTrigger("ChangeApparence");

        Invoke(nameof(EndEvent), 10f);
    }
    
    void EndEvent()
    {
        lancerScript.ForceDistance = _originalSpeed;
        Debug.Log("Fin de l'event Speed. Vitesse de lancer réinitialisée à: " + _originalSpeed);

        // Reviens à l'apparence normale
        if (chapeauAnimator != null)
            chapeauAnimator.SetTrigger("RetourNormal");
    }
}

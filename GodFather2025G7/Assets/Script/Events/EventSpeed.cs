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
        
    }

    public void DoEvent()
{
    var chapeau = FindAnyObjectByType<Chapeau>();
    chapeauAnimator = chapeau.GetComponent<Animator>();
    chapeauAnimator.SetBool("anim" , true);
    print (chapeauAnimator.name);
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
        chapeauAnimator.SetBool("anim" , false);
        lancerScript.ForceDistance = _originalSpeed;
        Debug.Log("Fin de l'event Speed. Vitesse de lancer réinitialisée à: " + _originalSpeed);

        // Reviens à l'apparence normale
        if (chapeauAnimator != null)
            chapeauAnimator.SetTrigger("RetourNormal");
    }
}

using UnityEngine;
using TMPro;

public class HorlogeVision : MonoBehaviour
{
    public TextMeshProUGUI horlogeText; // Référence au TextMeshPro
    private bool estVisible = false; // Suivi de la visibilité de l'horloge
    private string heureActuelle = "";

    private void Start()
    {
        MettreAJourHeure();
    }

    private void Update()
    {
        if (!estVisible) // Si l'horloge n'est pas visible, l'heure change
        {
            MettreAJourHeure();
        }
    }

    void MettreAJourHeure()
    {
        // Génére une heure aléatoire
        int heure = Random.Range(0, 24);
        int minute = Random.Range(0, 60);
        heureActuelle = string.Format("{0:D2}:{1:D2}", heure, minute);
        horlogeText.text = heureActuelle;
    }

    private void OnBecameVisible()
    {
        estVisible = true; // L'horloge est vue
    }

    private void OnBecameInvisible()
    {
        estVisible = false; // L'horloge n'est plus visible, l'heure changera
    }
}

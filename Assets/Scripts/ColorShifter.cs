using UnityEngine;

public enum PolarityState { Blue, Red }

public class ColorShifter : MonoBehaviour
{
    [Header("Status")]
    public PolarityState currentPolarity = PolarityState.Blue;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Color blueColor = Color.cyan;
    public Color redColor = Color.magenta;

    void Start()
    {
        // On force la mise à jour dès le début
        UpdatePolarity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            Debug.Log("CHANGEMENT DE COULEUR !"); // Le mouchard
            SwitchPolarity();
        }
    }

    void SwitchPolarity()
    {
        // Inverse l'état (Bleu devient Rouge, Rouge devient Bleu)
        if (currentPolarity == PolarityState.Blue)
            currentPolarity = PolarityState.Red;
        else
            currentPolarity = PolarityState.Blue;

        UpdatePolarity();
    }

    void UpdatePolarity()
    {
        // 1. Change la couleur visuelle
        if (currentPolarity == PolarityState.Blue)
            spriteRenderer.color = blueColor;
        else
            spriteRenderer.color = redColor;

        // 2. Change la Physique (Collisions)
        int playerLayer = gameObject.layer;
        int redLayer = LayerMask.NameToLayer("Red");
        int blueLayer = LayerMask.NameToLayer("Blue");

        // Sécurité : Vérifie que les layers existent
        if (redLayer == -1 || blueLayer == -1)
        {
            Debug.LogError("ERREUR : Les layers 'Red' et 'Blue' n'existent pas !");
            return;
        }

        if (currentPolarity == PolarityState.Blue)
        {
            // Je suis Bleu : Je traverse le Rouge, je touche le Bleu
            Physics2D.IgnoreLayerCollision(playerLayer, redLayer, true);
            Physics2D.IgnoreLayerCollision(playerLayer, blueLayer, false);
        }
        else
        {
            // Je suis Rouge : Je traverse le Bleu, je touche le Rouge
            Physics2D.IgnoreLayerCollision(playerLayer, redLayer, false);
            Physics2D.IgnoreLayerCollision(playerLayer, blueLayer, true);
        }
    }
}
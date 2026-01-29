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
        UpdatePolarity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            SwitchPolarity();
        }
    }

    void SwitchPolarity()
    {
        if (currentPolarity == PolarityState.Blue)
            currentPolarity = PolarityState.Red;
        else
            currentPolarity = PolarityState.Blue;

        UpdatePolarity();
    }

    void UpdatePolarity()
    {
        if (currentPolarity == PolarityState.Blue)
            spriteRenderer.color = blueColor;
        else
            spriteRenderer.color = redColor;

        int playerLayer = gameObject.layer;
        int redLayer = LayerMask.NameToLayer("Red");
        int blueLayer = LayerMask.NameToLayer("Blue");

        if (redLayer == -1 || blueLayer == -1)
        {
            Debug.LogError("ERREUR : Les layers 'Red' et 'Blue' n'existent pas !");
            return;
        }

        if (currentPolarity == PolarityState.Blue)
        {
            Physics2D.IgnoreLayerCollision(playerLayer, redLayer, true);
            Physics2D.IgnoreLayerCollision(playerLayer, blueLayer, false);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, redLayer, false);
            Physics2D.IgnoreLayerCollision(playerLayer, blueLayer, true);
        }
    }
}
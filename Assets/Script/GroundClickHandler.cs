using UnityEngine;

public class GroundClickHandler : MonoBehaviour
{
    private PlantButton plantButton;
    private int groundIndex;

    public void Setup(PlantButton button, int index)
    {
        plantButton = button;
        groundIndex = index;
    }

    private void OnMouseDown()
    {
        plantButton.SelectGround(groundIndex);
    }
}

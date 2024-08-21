using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Slot : MonoBehaviour
{
    public Image slotImage;
    public Sprite[] symbols;
    public float spinDuration = 2f;

    private Sprite currentSymbol;

    public Sprite CurrentSymbol => currentSymbol;

    public void Spin()
    {
        slotImage.DOFade(0, spinDuration).OnComplete(() =>
        {
            // Randomize the symbol
            int randomIndex = Random.Range(0, symbols.Length);
            currentSymbol = symbols[randomIndex];
            slotImage.sprite = currentSymbol;

            // Fade back in
            slotImage.DOFade(1, 0.5f).OnComplete(() =>
            {
                SlotMachineEvents.RaiseSpinEnd();
            });
        });
    }
}

using UnityEngine;
using System.Linq;

public class SlotMachineManager : MonoBehaviour
{
    public Slot[] slots;
    public Slot bonusSlot;

    private void OnEnable()
    {
        SlotMachineEvents.OnSpinEnd += CheckForMatch;
    }

    private void OnDisable()
    {
        SlotMachineEvents.OnSpinEnd -= CheckForMatch;
    }

    public void StartSpin()
    {
        SlotMachineEvents.RaiseSpinStart();

        foreach (var slot in slots)
        {
            slot.Spin();
        }

        bonusSlot.Spin();
    }

    private void CheckForMatch()
    {
        // Check if all symbols in the regular slots match
        if (slots.Select(slot => slot.CurrentSymbol).Distinct().Count() == 1)
        {
            SlotMachineEvents.RaiseMatchFound();

            // Check if the bonus slot also matches
            if (slots[0].CurrentSymbol == bonusSlot.CurrentSymbol)
            {
                SlotMachineEvents.RaiseBonusApplied();
            }
        }
    }
}

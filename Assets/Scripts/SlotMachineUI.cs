using UnityEngine;

public class SlotMachineUI : MonoBehaviour
{
    private void OnEnable()
    {
        SlotMachineEvents.OnSpinStart += OnSpinStart;
        SlotMachineEvents.OnMatchFound += OnMatchFound;
        SlotMachineEvents.OnBonusApplied += OnBonusApplied;
    }

    private void OnDisable()
    {
        SlotMachineEvents.OnSpinStart -= OnSpinStart;
        SlotMachineEvents.OnMatchFound -= OnMatchFound;
        SlotMachineEvents.OnBonusApplied -= OnBonusApplied;
    }

    private void OnSpinStart()
    {
        Debug.Log("Spinning Started");
    }

    private void OnMatchFound()
    {
        Debug.Log("Match Found!");
    }

    private void OnBonusApplied()
    {
        Debug.Log("Bonus Applied!");
    }
}

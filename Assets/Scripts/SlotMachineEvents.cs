using System;
using UnityEngine;

public static class SlotMachineEvents
{
    public static event Action OnSpinStart;
    public static event Action OnSpinEnd;
    public static event Action OnMatchFound;
    public static event Action OnBonusApplied;

    public static void RaiseSpinStart() => OnSpinStart?.Invoke();
    public static void RaiseSpinEnd() => OnSpinEnd?.Invoke();
    public static void RaiseMatchFound() => OnMatchFound?.Invoke();
    public static void RaiseBonusApplied() => OnBonusApplied?.Invoke();
}

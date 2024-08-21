# Crazy-4-Slots
Creating a production-level, event-driven 2D slot machine game requires well-structured, maintainable, and scalable code. Here's a template that aligns with industry best practices, avoiding `IEnumerator`, `Update`, and unnecessary logic within the main game loop. This template will focus on separation of concerns, event-based triggers, and modularity.

### Step 1: Project Structure

Organize your project into folders:
- **Scripts**
  - `SlotMachine.cs`
  - `Slot.cs`
  - `SlotMachineManager.cs`
  - `SlotMachineEvents.cs`
- **Prefabs**
  - `SlotPrefab` (A prefab containing the slot UI element)
- **Sprites**
  - Your symbols.

### Step 2: Event System

#### `SlotMachineEvents.cs`

This script will define all the events used in the slot machine.

```csharp
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
```

### Step 3: Slot Machine Manager

#### `SlotMachineManager.cs`

This script will handle the overall game flow and orchestrate the spinning and result checking.

```csharp
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
```

### Step 4: Slot Logic

#### `Slot.cs`

This script controls the behavior of an individual slot.

```csharp
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
```

### Step 5: UI & Prefab Setup

1. **Create a Slot Prefab**: 
   - Create a UI `Image` object.
   - Attach the `Slot` script to it.
   - Assign symbols to the `symbols` array and the `Image` component to `slotImage`.
   - Save this as a prefab named `SlotPrefab`.

2. **Create Slot Machine GameObject**:
   - In your scene, create an empty GameObject and name it `SlotMachine`.
   - Add the `SlotMachineManager` script to it.
   - Drag and drop your slot prefabs into the `slots` array and the `bonusSlot` field.

### Step 6: Trigger the Spin

You can now trigger the spin by calling the `StartSpin` method in the `SlotMachineManager` script, which you can tie to a button click or other event in the game.

### Step 7: Handling Events

You can handle slot machine events in various parts of your game, such as updating the UI, playing sounds, or triggering animations.

For example:

```csharp
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
```

### Final Steps

1. **Testing**: Ensure all slots spin and results are detected correctly.
2. **Polishing**: Add UI feedback, sounds, and animations based on events.
3. **Deployment**: Package and deploy your game.

### Summary

This template follows best practices by organizing the code into modular components, using events to decouple the logic, and ensuring that the game flow is easy to manage and extend. The DOTween library handles animations smoothly, keeping the gameplay responsive and visually appealing.

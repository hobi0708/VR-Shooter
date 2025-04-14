using System.Collections.Generic;
using UnityEngine;

public class Preset : MonoBehaviour
{
    public List<GameObject> targets;

    // Call this method with positive or negative values to change speed
    public void AdjustSpeed(float amount)
    {
        foreach (GameObject target in targets)
        {
            MoveBackAndForth mover = target.GetComponent<MoveBackAndForth>();
            if (mover != null)
            {
                mover.speed += amount;
                // Optional: clamp to prevent negative speed
                mover.speed = Mathf.Max(0f, mover.speed);
            }
        }
    }
}
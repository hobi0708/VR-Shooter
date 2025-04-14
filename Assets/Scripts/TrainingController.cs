using System.Collections.Generic;
using UnityEngine;

public class TrainingController : MonoBehaviour
{
    public List<GameObject> presets;
    private int currentPresetIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // Example: Press 'N' to go to next preset
        if (Input.GetKeyDown(KeyCode.N))
        {
            //NextPreset();
        }
    }

    public void StartTraining()
    {
        currentPresetIndex = 0;
        ActivateCurrentPreset();
    }

    public void SelectPreset(int index)
    {
        if (index >= 0 && index < presets.Count)
        {
            currentPresetIndex = index;
            ActivateCurrentPreset();
        }
        else
        {
            Debug.LogWarning("Invalid preset index!");
        }
    }

    public void NextPreset()
    {
        currentPresetIndex = (currentPresetIndex + 1) % presets.Count;
        ActivateCurrentPreset();
    }

    private void ActivateCurrentPreset()
    {
        for (int i = 0; i < presets.Count; i++)
        {
            presets[i].SetActive(i == currentPresetIndex);
        }
    }
}
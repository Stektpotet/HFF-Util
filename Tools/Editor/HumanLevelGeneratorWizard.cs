#if UNITY_EDITOR
using UnityEditor;
public class HumanLevelGeneratorWizard : ScriptableWizard
{
    public int regions = 1;
    [MenuItem("File/New Human Level With Areas %&L")]
    static void CreateWizard()
    {
        var wizard = DisplayWizard<HumanLevelGeneratorWizard>("Create Light", "Create");
        wizard.helpString = "Set number of regions (Checkpoint areas, excluding the initial spawn point)";
    }

    private void OnWizardUpdate()
    { isValid = regions >= 0; }

    void OnWizardCreate()
    { HumanLevelGenerator.NewScene(regions); }
}
#endif
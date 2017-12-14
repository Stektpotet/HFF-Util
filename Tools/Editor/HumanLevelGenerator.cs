#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using HumanAPI;

public static class HumanLevelGenerator
{
    [MenuItem("File/New Human Level %L")]
    public static void NewScene()
    { NewScene(0); }
    public static void NewScene(int regions)
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        SetupScene(regions);
        EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { SceneManager.GetActiveScene() } );
    }
    
    static void SetupScene(int regions=0)
    {
        var level = new GameObject("Level").transform;

        var spawnPoint = new GameObject("SpawnPoint", typeof(Checkpoint)).transform;
        spawnPoint.parent = level;
        level.gameObject.AddComponent<Level>().spawnPoint = spawnPoint;

        var fallTrigger = new GameObject("FallTrigger", typeof(FallTrigger)).transform;
        fallTrigger.Translate(0, -20, 0);
        fallTrigger.parent = level;

        var fallTriggerCollider = fallTrigger.gameObject.AddComponent<BoxCollider>();
        fallTriggerCollider.size = new Vector3(60, 20, 60);
        fallTriggerCollider.isTrigger = true;

        var levelPassTrigger = new GameObject("LevelPassTrigger", typeof(LevelPassTrigger)).transform;
        levelPassTrigger.Translate(5, 0, 0);
        levelPassTrigger.parent = level;

        var levelPassTriggerCollider = levelPassTrigger.gameObject.AddComponent<BoxCollider>();
        levelPassTriggerCollider.size = new Vector3(60, 20, 60);
        levelPassTriggerCollider.isTrigger = true;

        var lights = new GameObject("Lights").transform;
        lights.parent = level;

        var sun = new GameObject("Directional Light").transform;
        sun.gameObject.AddComponent<Light>().type = LightType.Directional;
        sun.Rotate(30, 50, 0);
        sun.parent = lights;

        var lightProbes = new GameObject("LightProbes").transform;
        lightProbes.parent = level;
        
        //Create Regions
        var area = new GameObject("area00").transform;
        area.parent = level;
        for (int i = 1; i <=regions; i++)
        {
            area = new GameObject(string.Format("area{0:00}", i)).transform;
            area.gameObject.AddComponent<Checkpoint>().number = i;
            var checkpointTrigger = area.gameObject.AddComponent<BoxCollider>();
            checkpointTrigger.isTrigger = true;
            checkpointTrigger.size *= 10;
            area.Translate(10 * i, 0, 0);
            area.parent = level;
        }
        
        Debug.Log("New HFF-Scene Created!");
    }

}
#endif
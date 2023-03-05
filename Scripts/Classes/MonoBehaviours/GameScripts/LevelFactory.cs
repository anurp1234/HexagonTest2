using UnityEngine;
using UnityEngine.UI;
public struct LoaderFactoryInfo
{
    public string environmentPath { get; private set; }

    public string playerCharacterPath { get; private set; }

    public string collisionProcessorPath { get; private set; }

    public string levelCreatorPath { get; private set; }

    public string gameSessionPath { get; private set; }

    public string soundManagerPath { get; private set; }
    public LoaderFactoryInfo(string environmentPath, string playerCharacterPath, string collisionProcessorPath,string levelCreatorPath, string gameSessionPath, string soundManagerPath)
    {
        this.environmentPath = environmentPath;

        this.playerCharacterPath = playerCharacterPath;

        this.collisionProcessorPath = collisionProcessorPath;

        this.levelCreatorPath = levelCreatorPath;

        this.gameSessionPath = gameSessionPath;

        this.soundManagerPath = soundManagerPath;
      }
}
public class LevelFactory : MonoBehaviour
{
    [SerializeField]
    string EnvironmentPath;

    [SerializeField]
    string playerCharacterPath;

    [SerializeField]
    string collisionProcessorPath;

    [SerializeField]
    string levelCreatorPath;

    [SerializeField]
    string gameSessionPath;

    [SerializeField]
    string soundManagerPath;

    [SerializeField]
    Slider loadingBar;

    public void CreateLevel()
    {
        LoaderFactoryInfo info = new LoaderFactoryInfo(EnvironmentPath, playerCharacterPath, collisionProcessorPath, levelCreatorPath, gameSessionPath, soundManagerPath);
        GameObject sceneCreatorGO = new GameObject("SceneCreator");
        SceneCreator sceneCreator = sceneCreatorGO.AddComponent<SceneCreator>();
        StartCoroutine( sceneCreator.LoadScene(this,info));
    }

    public void UpdateLoadingProgress(float progress)
    {
        loadingBar.value = progress;
    }

    public void OnLoadingComplete()
    {
        GameObject.Destroy(transform.root.gameObject);
    }
}

using UnityEngine;
using System.Collections;
public class SceneCreator: MonoBehaviour
{
    /*Ideally this will be done in async manner using addressables, while showing loading bar, given the time constraints
     * loading the scene objects directly from resources
     */

    public IEnumerator LoadScene(LevelFactory factory,LoaderFactoryInfo info)
    {
        int totalObjectsToLoad = 6;
        int objectsLoaded = 0;
        GameObject.Instantiate((GameObject)Resources.Load(info.environmentPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded/ totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        GameObject.Instantiate((GameObject)Resources.Load(info.playerCharacterPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        GameObject.Instantiate((GameObject)Resources.Load(info.levelCreatorPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        GameObject.Instantiate((GameObject)Resources.Load(info.collisionProcessorPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        GameObject.Instantiate((GameObject)Resources.Load(info.gameSessionPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        GameObject snManagerGO = GameObject.Instantiate((GameObject)Resources.Load(info.soundManagerPath));
        SoundManager soundManager = snManagerGO.GetComponent<SoundManager>();
        soundManager.PlayBkgMusic();
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        factory.OnLoadingComplete();
        GameObject.Destroy(gameObject);
    }
}

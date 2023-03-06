using UnityEngine;
using System.Collections;
public class SceneCreator: MonoBehaviour
{
    /*Ideally this will be done in async manner using addressables, while showing loading bar, given the time constraints
     * loading the scene objects directly from resources
     */

    public IEnumerator LoadScene(LevelFactory factory,LoaderFactoryInfo info)
    {
        int totalObjectsToLoad = 5;
        int objectsLoaded = 0;
        
        yield return StartCoroutine(LoadSceneObject(info.environmentPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded/ totalObjectsToLoad);

        yield return StartCoroutine(LoadSceneObject(info.playerCharacterPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);

        yield return StartCoroutine(LoadSceneObject(info.levelCreatorPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);

        yield return StartCoroutine(LoadSceneObject(info.collisionProcessorPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);

        yield return StartCoroutine(LoadSceneObject(info.gameSessionPath));
        objectsLoaded++;
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
       
        factory.UpdateLoadingProgress((float)objectsLoaded / totalObjectsToLoad);
        yield return new WaitForSeconds(0.1f);
        factory.OnLoadingComplete();
        GameObject.Destroy(gameObject);
    }

    IEnumerator LoadSceneObject(string path)
    {
        GameObject.Instantiate((GameObject)Resources.Load(path));
        yield return new WaitForSeconds(0.2f);
    }
}

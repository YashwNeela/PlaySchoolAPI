
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class AssetManyLoader{

 public string[] bundleName;
  public Button[] bundleButtonClick;
}
public class AssetsLoadingAll : MonoBehaviour
{
   
   public static AssetsLoadingAll instance;
        private AssetBundle assetBundle;
        private AsyncOperation asyncOperation;


    public AssetManyLoader _assetMany;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start() {
            
            for(int i = 0 ; i < _assetMany.bundleName.Length; i++)
            {
                int index = i;
                _assetMany.bundleButtonClick[index].onClick.AddListener(()=> LoadGameScene(_assetMany.bundleName[index]) );
            }
        }

        public void LoadGameScene(string bundleName)
        {
            StartCoroutine(LoadSceneFromBundle(bundleName));
            //LoadSceneFromBundle(bundleName);
        }

      

    
        public void UnloadBundle()
        {
            if (assetBundle != null)
            {
                assetBundle.Unload(true);
                assetBundle = null;
                instance = null;
                Destroy(this.gameObject);
            }
        }
   

        private IEnumerator LoadSceneFromBundle(string bundleName)
        {
            AssetBundle.UnloadAllAssetBundles(true);
      

            string filePath = GetBundleFilePath(bundleName);

            var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);

            yield return assetBundleCreateRequest;


            assetBundle = assetBundleCreateRequest.assetBundle;
            string[] scenePaths = assetBundle.GetAllScenePaths();
            if (scenePaths.Length > 0)
            {
                //asyncOperation = SceneManager.LoadSceneAsync(scenePaths[0]);
                asyncOperation = SceneManager.LoadSceneAsync(scenePaths[0]);
                while (!asyncOperation.isDone)
                {
                    //   progress = Mathf.MoveTowards(progress, assetBundleCreateRequest.progress, Time.deltaTime);
            
                    yield return null;
                }
            }
            else
            {
                Debug.LogError("No scene found in the loaded AssetBundle.");
            }

            //  loadingObject.SetActive(false);
        }



        private string GetBundleFilePath(string bundleName)
        {

           return Path.Combine(Application.streamingAssetsPath, bundleName);

        }
        
        
         }

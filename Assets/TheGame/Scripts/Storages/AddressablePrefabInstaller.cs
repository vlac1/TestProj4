using UnityEngine;
using TheGame.Interfaces;
using Zenject;

#if UNITY_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif

namespace TheGame
{
    //TODO
    public class AddressablePrefabInstaller : MonoInstaller
    {
        [SerializeField] private string _assetBundleAddress = "MyBundle";
        [SerializeField] private string _prefabInBundleAddress = "MyPrefab";

#if UNITY_ADDRESSABLES
        public override async void InstallBindings()
        {
            var handle = Addressables.LoadAssetAsync<AssetBundle>(_assetBundleAddress);
            var bundle = await handle.Task;
            //if (handle.Status == AsyncOperationStatus.Failed)...

            var prefab = bundle.LoadAsset<GameObject>(_prefabInBundleAddress);

            //Container.Bind<GameObject>().WithId("PoolPrefab").
            //    FromComponentInNewPrefab(prefab).AsTransient();
        }
        #endif
    }
}
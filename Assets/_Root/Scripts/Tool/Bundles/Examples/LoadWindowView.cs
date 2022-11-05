using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _changeBackgroundButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _changeBackgroundButton.onClick.AddListener(ChangeBackground);

            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _changeBackgroundButton.onClick.RemoveAllListeners();

            _spawnAssetButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
        }


        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetSpritesAssetBundles());
            StartCoroutine(DownloadAndSetAudioAssetBundles());
        }

        private void ChangeBackground()
        {
            _changeBackgroundButton.interactable = false;
            StartCoroutine(DownloadAndSetBackgroundsAssetBundle());
        }


        private void SpawnPrefab()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        private void DespawnPrefabs()
        {
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }
    }
}
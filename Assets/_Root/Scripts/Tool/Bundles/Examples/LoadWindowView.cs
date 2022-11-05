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

        [Header("Addressables Spawning")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [Header("Addressables Background")]
        [SerializeField] private AssetReference _background;
        [SerializeField] private Image _backgroundComponent;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _loadedBackground;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _changeBackgroundButton.onClick.AddListener(ChangeBackground);

            _spawnAssetButton.onClick.AddListener(SpawnPrefab);
            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _changeBackgroundButton.onClick.RemoveAllListeners();

            _spawnAssetButton.onClick.RemoveAllListeners();
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnPrefabs();
            RemoveBackground();
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


        private void AddBackground()
        {
            if (!_loadedBackground.IsValid())
            {
                _loadedBackground = Addressables.LoadAssetAsync<Sprite>(_background);
                _loadedBackground.Completed += OnBackgroundLoaded;
            }
        }

        private void RemoveBackground()
        {
            if (_loadedBackground.IsValid())
            {
                _loadedBackground.Completed -= OnBackgroundLoaded;
                Addressables.Release(_loadedBackground);
                SetBackground(null);
            }
        }

        private void OnBackgroundLoaded(AsyncOperationHandle<Sprite> asyncOperationHandle)
        {
            asyncOperationHandle.Completed -= OnBackgroundLoaded;
            SetBackground(asyncOperationHandle.Result);
        }

        private void SetBackground(Sprite background) =>
            _backgroundComponent.sprite = background;
    }
}
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Tool.Bundles.Examples
{
    // все комментарии в этом классе могли быть выражены кодом
    // комментарии в данном проекте используются только в образовательных целях
    internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string UrlAssetBundleSprites = "https://drive.google.com/uc?export=download&id=1rQzWdcChHhJJBTe4rf1D0Kwi1a43jxWR";
        private const string UrlAssetBundleAudio = "https://drive.google.com/uc?export=download&id=1I7euU6Hv5yrn1ektprUumbGHEikklk3Y";

        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;
        [SerializeField] private DataAudioBundle[] _dataAudioBundles;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;


        protected IEnumerator DownloadAndSetAssetBundles()
        {
            // сначала скачиваем нужные бандлы
            yield return GetSpritesAssetBundle();
            yield return GetAudioAssetBundle();

            //затем устанавливаем контент из скачанных бандлов
            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");

            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }

        private IEnumerator GetSpritesAssetBundle()
        {
            // создаём запрос на получение бандла
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleSprites);

            // отправялем запрос
            yield return request.SendWebRequest();

            // ждём получение ответа
            while (!request.isDone)
                yield return null;

            // получаем из ответа бандл
            StateRequest(request, out _spritesAssetBundle);
        }

        private IEnumerator GetAudioAssetBundle()
        {
            // создаём запрос на получение бандла
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleAudio);

            // отправялем запрос
            yield return request.SendWebRequest();

            // ждём получение ответа
            while (!request.isDone)
                yield return null;

            // получаем из ответа бандл
            StateRequest(request, out _audioAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataSpriteBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetAudioAssets(AssetBundle assetBundle)
        {
            foreach (DataAudioBundle data in _dataAudioBundles)
            {
                data.AudioSource.clip = assetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
                data.AudioSource.Play();
            }
        }
    }
}

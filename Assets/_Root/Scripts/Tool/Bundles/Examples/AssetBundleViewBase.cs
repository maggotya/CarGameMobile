using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Tool.Bundles.Examples
{
    // все комментарии в этом классе могли быть выражены кодом
    // комментарии в данном проекте используются только в образовательных целях
    internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string UrlAssetBundleBackgrounds = "https://drive.google.com/uc?export=download&id=1y2gUPCn4mUzfZ6W8L7K4NeNitS03_Pn0";
        private const string UrlAssetBundleSprites = "https://drive.google.com/uc?export=download&id=1rQzWdcChHhJJBTe4rf1D0Kwi1a43jxWR";
        private const string UrlAssetBundleAudio = "https://drive.google.com/uc?export=download&id=1I7euU6Hv5yrn1ektprUumbGHEikklk3Y";

        [SerializeField] private DataSpriteBundle[] _dataBackgroundBundles;
        [SerializeField] private DataSpriteBundle[] _dataSpriteBundles;
        [SerializeField] private DataAudioBundle[] _dataAudioBundles;

        private AssetBundle _backgroundsAssetBundle;
        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;


        protected IEnumerator DownloadAndSetBackgroundsAssetBundle()
        {
            // скачиваем бандл
            yield return GetBackgroundsAssetBundle();

            // устанавливаем контент из скачанного бандла
            if (_backgroundsAssetBundle != null)
                SetBackgroundAssets(_backgroundsAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_backgroundsAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetSpritesAssetBundles()
        {
            // скачиваем бандл
            yield return GetSpritesAssetBundle();

            // устанавливаем контент из скачанного бандла
            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");
        }

        protected IEnumerator DownloadAndSetAudioAssetBundles()
        {
            // скачиваем бандл
            yield return GetAudioAssetBundle();

            // устанавливаем контент из скачанного бандла
            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }


        private IEnumerator GetBackgroundsAssetBundle()
        {
            // создаём запрос на получение бандла
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetBundleBackgrounds);

            // отправялем запрос
            yield return request.SendWebRequest();

            // ждём получение ответа
            while (!request.isDone)
                yield return null;

            // получаем из ответа бандл
            StateRequest(request, out _backgroundsAssetBundle);
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


        private void SetBackgroundAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _dataBackgroundBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
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

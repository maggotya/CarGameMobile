using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

namespace Tool.Localization.Examples
{
    internal abstract class LocalizationWindow : MonoBehaviour
    {
        private const int EnglishIndex = 0;
        private const int FrenchIndex = 1;
        private const int RussianIndex = 2;

        [Header("Scene Components")]
        [SerializeField] private Button _englishButton;
        [SerializeField] private Button _frenchButton;
        [SerializeField] private Button _russianButton;


        private void Start()
        {
            _englishButton.onClick.AddListener(() => ChangeLanguage(EnglishIndex));
            _frenchButton.onClick.AddListener(() => ChangeLanguage(FrenchIndex));
            _russianButton.onClick.AddListener(() => ChangeLanguage(RussianIndex));
            OnStarted();
        }

        private void OnDestroy()
        {
            _englishButton.onClick.RemoveAllListeners();
            _frenchButton.onClick.RemoveAllListeners();
            _russianButton.onClick.RemoveAllListeners();
            OnDestroyed();
        }


        protected virtual void OnStarted() { }
        protected virtual void OnDestroyed() { }


        private void ChangeLanguage(int index) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}

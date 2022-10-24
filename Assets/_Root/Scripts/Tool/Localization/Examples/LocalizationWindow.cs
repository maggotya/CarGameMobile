using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

namespace Tool.Localization.Examples
{
    internal abstract class LocalizationWindow : MonoBehaviour
    {
        [Header("Scene Components")]
        [SerializeField] private Button _englishButton;
        [SerializeField] private Button _frenchButton;
        [SerializeField] private Button _russianButton;


        private void Start()
        {
            _englishButton.onClick.AddListener(() => ChangeLanguage(0));
            _frenchButton.onClick.AddListener(() => ChangeLanguage(1));
            _russianButton.onClick.AddListener(() => ChangeLanguage(2));
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

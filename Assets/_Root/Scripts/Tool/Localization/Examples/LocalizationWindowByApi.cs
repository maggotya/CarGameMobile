using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Localization.Examples
{
    internal class LocalizationWindowByApi : LocalizationWindow
    {
        [SerializeField] private TMP_Text _changeText;

        [Header("Settings")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _localizationTag;


        protected override void OnStarted()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            UpdateTextAsync();
        }

        protected override void OnDestroyed()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }


        private void OnSelectedLocaleChanged(Locale _) => UpdateTextAsync();
        private void UpdateTextAsync() => StartCoroutine(UpdateTextRoutine());

        private IEnumerator UpdateTextRoutine()
        {
            AsyncOperationHandle<StringTable> loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(_tableName);
            yield return loadingOperation;

            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                StringTable table = loadingOperation.Result;
                _changeText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
            }
            else
            {
                string errorMessage = $"[{GetType().Name}] Could not load String Table: {loadingOperation.OperationException}";
                Debug.LogError(errorMessage);
            }
        }
    }
}

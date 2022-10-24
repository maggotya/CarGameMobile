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


        private void OnSelectedLocaleChanged(Locale _) =>
            UpdateTextAsync();

        private void UpdateTextAsync() =>
            LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;
                        _changeText.text = table.GetEntry(_localizationTag)?.GetLocalizedString();
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
    }
}

using System;
using UnityEngine;
using System.Collections.Generic;
using Features.AbilitySystem.Abilities;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesView
    {
        void Display(IReadOnlyList<IAbilityItem> abilityItems, Action<string> clicked);
        void Clear();
    }

    internal class AbilitiesView : MonoBehaviour, IAbilitiesView
    {
        [SerializeField] private GameObject _abilityButtonPrefab;
        [SerializeField] private Transform _placeForButtons;

        private readonly Dictionary<string, AbilityButtonView> _buttonViews = new();


        private void OnDestroy() => Clear();


        public void Display(IReadOnlyList<IAbilityItem> abilityItems, Action<string> clicked)
        {
            Clear();

            foreach (IAbilityItem abilityItem in abilityItems)
                _buttonViews[abilityItem.Id] = CreateButtonView(abilityItem, clicked);
        }

        public void Clear()
        {
            foreach (AbilityButtonView buttonView in _buttonViews.Values)
                DestroyButtonView(buttonView);

            _buttonViews.Clear();
        }


        private AbilityButtonView CreateButtonView(IAbilityItem item, Action<string> clicked)
        {
            GameObject objectView = Instantiate(_abilityButtonPrefab, _placeForButtons, false);
            AbilityButtonView buttonView = objectView.GetComponent<AbilityButtonView>();

            buttonView.Init
            (
                item.Icon,
                () => clicked?.Invoke(item.Id)
            );

            return buttonView;
        }

        private void DestroyButtonView(AbilityButtonView buttonView)
        {
            buttonView.Deinit();
            Destroy(buttonView.gameObject);
        }
    }
}
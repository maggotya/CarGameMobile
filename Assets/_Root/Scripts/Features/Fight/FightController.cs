using System;
using Profile;
using TMPro;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Fight
{
    internal class FightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/FightView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly FightView _view;
        private readonly Enemy _enemy;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crime;


        public FightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);

            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crime = CreatePlayerData(DataType.Crime);

            Subscribe(_view);
        }

        protected override void OnDispose()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crime);

            Unsubscribe(_view);
        }


        private FightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<FightView>();
        }

        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe(FightView view)
        {
            view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            view.MinusMoneyButton.onClick.AddListener(DecreaseMoney);

            view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            view.MinusHealthButton.onClick.AddListener(DecreaseHealth);

            view.AddPowerButton.onClick.AddListener(IncreasePower);
            view.MinusPowerButton.onClick.AddListener(DecreasePower);

            view.AddCrimeButton.onClick.AddListener(IncreaseCrime);
            view.MinusCrimeButton.onClick.AddListener(DecreaseCrime);

            view.FightButton.onClick.AddListener(Fight);
            view.EscapeButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe(FightView view)
        {
            view.AddMoneyButton.onClick.RemoveAllListeners();
            view.MinusMoneyButton.onClick.RemoveAllListeners();

            view.AddHealthButton.onClick.RemoveAllListeners();
            view.MinusHealthButton.onClick.RemoveAllListeners();

            view.AddPowerButton.onClick.RemoveAllListeners();
            view.MinusPowerButton.onClick.RemoveAllListeners();

            view.AddCrimeButton.onClick.RemoveAllListeners();
            view.MinusCrimeButton.onClick.RemoveAllListeners();

            view.FightButton.onClick.RemoveAllListeners();
            view.EscapeButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseCrime() => IncreaseValue(_crime);
        private void DecreaseCrime() => DecreaseValue(_crime);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
            UpdateEscapeButtonVisibility();
        }


        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _view.CountPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _view.CountMoneyText,
                DataType.Health => _view.CountHealthText,
                DataType.Power => _view.CountPowerText,
                DataType.Crime => _view.CountCrimeText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void UpdateEscapeButtonVisibility()
        {
            const int minCrimeToUse = 0;
            const int maxCrimeToUse = 2;
            const int minCrimeToShow = 0;
            const int maxCrimeToShow = 5;

            int crimeValue = _crime.Value;
            bool canUse = minCrimeToUse <= crimeValue && crimeValue <= maxCrimeToUse;
            bool canShow = minCrimeToShow <= crimeValue && crimeValue <= maxCrimeToShow;

            _view.EscapeButton.interactable = canUse;
            _view.EscapeButton.gameObject.SetActive(canShow);
        }

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Escape()
        {
            string color = "#FFB202";
            string message = "Escaped";

            Debug.Log($"<color={color}>{message}!!!</color>");

            Close();
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
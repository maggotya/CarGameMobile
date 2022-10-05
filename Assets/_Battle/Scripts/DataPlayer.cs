using System.Collections.Generic;

namespace BattleScripts
{
    internal abstract class DataPlayer
    {
        private readonly List<IEnemy> _enemies;

        private int _countMoney;
        private int _countHealth;
        private int _countPower;

        public string TitleData { get; }

        public int Money
        {
            get => _countMoney;
            set => SetValue(DataType.Money, value);
        }

        public int Health
        {
            get => _countHealth;
            set => SetValue(DataType.Health, value);
        }

        public int Power
        {
            get => _countPower;
            set => SetValue(DataType.Power, value);
        }


        protected DataPlayer(string titleData)
        {
            TitleData = titleData;
            _enemies = new List<IEnemy>();
        }


        public void Attach(IEnemy enemy) => _enemies.Add(enemy);
        public void Detach(IEnemy enemy) => _enemies.Remove(enemy);

        protected void Notify(DataType dataType)
        {
            foreach (var investor in _enemies)
                investor.Update(this, dataType);
        }


        private void SetValue(DataType dataType, int value)
        {
            int currentValue = GetValue(dataType);
            if (currentValue == value)
                return;

            switch (dataType)
            {
                case DataType.Money:
                    _countMoney = value;
                    break;
                case DataType.Health:
                    _countHealth = value;
                    break;
                case DataType.Power:
                    _countPower = value;
                    break;
            }

            Notify(dataType);
        }

        private int GetValue(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoney,
                DataType.Health => _countHealth,
                DataType.Power => _countPower,
                _ => default
            };
    }
}

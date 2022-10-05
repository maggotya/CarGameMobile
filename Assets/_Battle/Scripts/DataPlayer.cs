using System.Collections.Generic;

namespace BattleScripts
{
    internal abstract class DataPlayer
    {
        private string _titleData;
        private int _countMoney;
        private int _countHealth;
        private int _countPower;

        private List<IEnemy> _enemies = new List<IEnemy>();

        public string TitleData => _titleData;

        public int Money
        {
            get => _countMoney;
            set
            {
                if (_countMoney != value)
                {
                    _countMoney = value;
                    Notify(DataType.Money);
                }
            }
        }

        public int Health
        {
            get => _countHealth;
            set
            {
                if (_countHealth != value)
                {
                    _countHealth = value;
                    Notify(DataType.Health);
                }
            }
        }

        public int Power
        {
            get => _countPower;
            set
            {
                if (_countPower != value)
                {
                    _countPower = value;
                    Notify(DataType.Power);
                }
            }
        }


        protected DataPlayer(string titleData)
        {
            _titleData = titleData;
        }

        public void Attach(IEnemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Detach(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }

        protected void Notify(DataType dataType)
        {
            foreach (var investor in _enemies)
                investor.Update(this, dataType);
        }
    }
}

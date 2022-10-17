using System.Collections.Generic;

namespace Features.Fight
{
    internal class PlayerData
    {
        private readonly List<IEnemy> _enemies;
        private int _value;

        public DataType DataType { get; }

        public int Value
        {
            get => _value;
            set => SetValue(value);
        }


        public PlayerData(DataType dataType)
        {
            DataType = dataType;
            _enemies = new List<IEnemy>();
        }


        public void Attach(IEnemy enemy) => _enemies.Add(enemy);
        public void Detach(IEnemy enemy) => _enemies.Remove(enemy);

        protected void Notify()
        {
            foreach (var investor in _enemies)
                investor.Update(this);
        }


        private void SetValue(int value)
        {
            if (_value == value)
                return;

            _value = value;
            Notify();
        }
    }
}

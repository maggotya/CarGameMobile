using UnityEngine;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KHealth = 7f;
        private const float KPower = 5f;
        private const float KSummary = 0.2f;
        private const float MaxHealthPlayer = 20;

        private readonly string _name;

        private int _moneyPlayer;
        private int _healthPlayer;
        private int _powerPlayer;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _moneyPlayer = playerData.Value;
                    break;

                case DataType.Health:
                    _healthPlayer = playerData.Value;
                    break;

                case DataType.Power:
                    _powerPlayer = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }


        public int CalcPower()
        {
            float moneyRatio = _moneyPlayer / KMoney;
            float healthRatio = _healthPlayer / KHealth;
            float powerRatio = _powerPlayer / KPower;
            float summaryRatio = moneyRatio + healthRatio + powerRatio;

            return (int)(summaryRatio * KSummary * MaxHealthPlayer);
        }
    }
}
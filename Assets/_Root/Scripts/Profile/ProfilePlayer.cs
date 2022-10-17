using Tool;
using Game.Car;
using Features.Inventory;
using Features.Rewards.Currency;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;
        public readonly CurrencyModel Currency;


        public ProfilePlayer(float speedCar, float jumpHeightCar, GameState initialState) : this(speedCar, jumpHeightCar)
        {
            CurrentState.Value = initialState;
        }

        public ProfilePlayer(float speedCar, float jumpHeightCar)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeightCar);
            Inventory = new InventoryModel();
            Currency = new CurrencyModel();
        }
    }
}
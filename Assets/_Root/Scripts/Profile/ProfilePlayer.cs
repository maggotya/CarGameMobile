using Game.Car;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly GameState CurrentState;
        public readonly CarModel CurrentCar;


        public ProfilePlayer(float speedCar, GameState initialState) : this(speedCar)
        {
            CurrentState = initialState;
        }

        public ProfilePlayer(float speedCar)
        {
            CurrentState = default;
            CurrentCar = new CarModel(speedCar);
        }
    }
}
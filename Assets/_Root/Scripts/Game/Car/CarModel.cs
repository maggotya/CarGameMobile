using Features.Shed.Upgrade;

namespace Game.Car
{
    internal class CarModel : IUpgradable
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultJumpHeight;

        public float Speed { get; set; }
        public float JumpHeight { get; set; }


        public CarModel(float speed, float jumpHeight)
        {
            _defaultSpeed = speed;
            _defaultJumpHeight = jumpHeight;

            Speed = speed;
            JumpHeight = jumpHeight;
        }


        public void Restore()
        {
            Speed = _defaultSpeed;
            JumpHeight = _defaultJumpHeight;
        }
    }
}
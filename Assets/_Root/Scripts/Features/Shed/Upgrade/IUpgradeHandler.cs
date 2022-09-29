namespace Features.Shed.Upgrade
{
    internal interface IUpgradeHandler
    {
        void Upgrade(IUpgradable upgradable);
    }
}

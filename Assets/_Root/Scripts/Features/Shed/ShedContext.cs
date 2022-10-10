using System;
using Tool;
using Profile;
using UnityEngine;
using Features.Inventory;
using Features.Shed.Upgrade;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        private static readonly ResourcePath _viewPath = new("Prefabs/Shed/ShedView");
        private static readonly ResourcePath _dataSourcePath = new("Configs/Shed/UpgradeItemConfigDataSource");


        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(profilePlayer, placeForUi);
        }


        private ShedController CreateController(ProfilePlayer profilePlayer, Transform placeForUi)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            UpgradeHandlersRepository shedRepository = CreateRepository();
            ShedView shedView = LoadView(placeForUi);
            // чтобы представление гаража оказалось "выше" представления инвентаря
            // (иначе инвентарь будет блокировать кнопки)
            // нужно загрузить представления гаража только после инвентаря

            return new ShedController
            (
                shedView,
                profilePlayer,
                shedRepository
            );
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);

            return context;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = LoadConfigs();
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private UpgradeItemConfig[] LoadConfigs() =>
            ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }
    }
}

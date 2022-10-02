using System;
using JetBrains.Annotations;
using Features.Inventory.Items;

namespace Features.Inventory
{
    internal interface IInventoryController
    {
    }

    internal class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly IItemsRepository _repository;


        public InventoryController(
            [NotNull] IInventoryView view,
            [NotNull] IInventoryModel model,
            [NotNull] IItemsRepository repository)
        {
            _view
                = view ?? throw new ArgumentNullException(nameof(view));

            _model
                = model ?? throw new ArgumentNullException(nameof(model));

            _repository
                = repository ?? throw new ArgumentNullException(nameof(repository));

            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _model.EquippedItems)
                _view.Select(itemId);
        }

        protected override void OnDispose() =>
            _view.Clear();


        private void OnItemClicked(string itemId)
        {
            bool equipped = _model.IsEquipped(itemId);

            if (equipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _view.Select(itemId);
            _model.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _view.Unselect(itemId);
            _model.UnequipItem(itemId);
        }
    }
}
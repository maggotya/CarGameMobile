using System;

/// <summary>
/// Made by Feiko Joosten
/// 
/// I have based this code on this blogpost. Decided to give it more functionality. http://blogs.unity3d.com/2015/12/23/1k-update-calls/
/// Use this to speed up your performance when you have a lot of update, fixed update and or late update calls in your scene
/// Let the object you want to give increased performance inherit from OverridableMonoBehaviour
/// Replace your void Update() for public override void UpdateMe()
/// Or replace your void FixedUpdate() for public override void FixedUpdateMe()
/// Or replace your void LateUpdate() for public override void LateUpdateMe()
/// OverridableMonoBehaviour will add the object to the update manager
/// UpdateManager will handle all of the update calls
/// </summary>

namespace JoostenProductions {
    public class UpdateManager : SingletonBehaviour<UpdateManager> {
        // If someone needs this on scene switch, it'll be recreated. Will have to add proper cleanup support in case this is set to true
        protected override bool DoNotDestroyOnLoad { get { return false; } }

        private static event Action OnUpdateEvent;
        private static event Action OnFixedUpdateEvent;
        private static event Action OnLateUpdateEvent;

        private static readonly Type overridableMonoBehaviourType = typeof(OverridableMonoBehaviour);

        public static void SubscribeToUpdate(Action callback) {
            if(Instance == null) return;

            OnUpdateEvent += callback;
        }

        public static void SubscribeToFixedUpdate(Action callback) {
            if(Instance == null) return;

            OnFixedUpdateEvent += callback;
        }

        public static void SubscribeToLateUpdate(Action callback) {
            if(Instance == null) return;

            OnLateUpdateEvent += callback;
        }

        public static void UnsubscribeFromUpdate(Action callback) {
            OnUpdateEvent -= callback;
        }

        public static void UnsubscribeFromFixedUpdate(Action callback) {
            OnFixedUpdateEvent -= callback;
        }

        public static void UnsubscribeFromLateUpdate(Action callback) {
            OnLateUpdateEvent -= callback;
        }

        public static void AddItem(OverridableMonoBehaviour behaviour) {
            if(behaviour == null) throw new NullReferenceException("The behaviour you've tried to add is null!");

            if(isShuttingDown) return;

            AddItemToArray(behaviour);
        }

        public static void RemoveSpecificItem(OverridableMonoBehaviour behaviour) {
            if(behaviour == null) throw new NullReferenceException("The behaviour you've tried to remove is null!");

            if(isShuttingDown) return;

            if(Instance != null) RemoveSpecificItemFromArray(behaviour);
        }

        public static void RemoveSpecificItemAndDestroyComponent(OverridableMonoBehaviour behaviour) {
            if(behaviour == null) throw new NullReferenceException("The behaviour you've tried to remove is null!");

            if(isShuttingDown) return;

            if(Instance != null) RemoveSpecificItemFromArray(behaviour);

            Destroy(behaviour);
        }

        public static void RemoveSpecificItemAndDestroyGameObject(OverridableMonoBehaviour behaviour) {
            if(behaviour == null) throw new NullReferenceException("The behaviour you've tried to remove is null!");

            if(isShuttingDown) return;

            if(Instance != null) RemoveSpecificItemFromArray(behaviour);

            Destroy(behaviour.gameObject);
        }

        private static void AddItemToArray(OverridableMonoBehaviour behaviour) {
            Type behaviourType = behaviour.GetType();

            if(behaviourType.GetMethod("UpdateMe").DeclaringType != overridableMonoBehaviourType)
                SubscribeToUpdate(behaviour.UpdateMe);

            if(behaviourType.GetMethod("FixedUpdateMe").DeclaringType != overridableMonoBehaviourType)
                SubscribeToFixedUpdate(behaviour.FixedUpdateMe);

            if(behaviourType.GetMethod("LateUpdateMe").DeclaringType != overridableMonoBehaviourType)
                SubscribeToLateUpdate(behaviour.LateUpdateMe);
        }

        private static void RemoveSpecificItemFromArray(OverridableMonoBehaviour behaviour) {
            UnsubscribeFromUpdate(behaviour.UpdateMe);
            UnsubscribeFromFixedUpdate(behaviour.FixedUpdateMe);
            UnsubscribeFromLateUpdate(behaviour.LateUpdateMe);
        }

        private void Update() {
            if(OnUpdateEvent != null) OnUpdateEvent.Invoke();
        }

        private void FixedUpdate() {
            if(OnFixedUpdateEvent != null) OnFixedUpdateEvent.Invoke();
        }

        private void LateUpdate() {
            if(OnLateUpdateEvent != null) OnLateUpdateEvent.Invoke();
        }
    }
}
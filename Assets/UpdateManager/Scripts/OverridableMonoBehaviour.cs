using UnityEngine;

namespace JoostenProductions {
    public class OverridableMonoBehaviour : MonoBehaviour {
        [SerializeField] private bool addOnEnable = true;
        [SerializeField] private bool removeOnDisable = true;

        protected virtual void OnEnable() {
            if(addOnEnable) UpdateManager.AddItem(this);
        }

        protected virtual void OnDisable() {
            if(removeOnDisable) UpdateManager.RemoveSpecificItem(this);
        }

        /// <summary>
        /// If your class uses the OnEnable or OnDisable function, please use  protected override void OnEnable() instead. (Or use protected override void OnDisable() if you are overriding OnDisable)
        /// Also don't forget to call base.OnEnable() first. (Or base.OnDisable() if you are overriding OnDisable)
        /// If your class does not use the OnEnable or OnDisable function, this object will be added to the UpdateManager automatically.
        /// Do not forget to replace your Update function with public override void UpdateMe()
        /// </summary>
        public virtual void UpdateMe() {
        }

        /// <summary>
        /// If your class uses the OnEnable or OnDisable function, please use  protected override void OnEnable() instead. (Or use protected override void OnDisable() if you are overriding OnDisable)
        /// Also don't forget to call base.OnEnable() first. (Or base.OnDisable() if you are overriding OnDisable)
        /// If your class does not use the OnEnable or OnDisable function, this object will be added to the UpdateManager automatically.
        /// Do not forget to replace your Fixed Update function with public override void FixedUpdateMe()
        /// </summary>
        public virtual void FixedUpdateMe() {
        }

        /// <summary>
        /// If your class uses the OnEnable or OnDisable function, please use  protected override void OnEnable() instead. (Or use protected override void OnDisable() if you are overriding OnDisable)
        /// Also don't forget to call base.OnEnable() first. (Or base.OnDisable() if you are overriding OnDisable)
        /// If your class does not use the OnEnable or OnDisable function, this object will be added to the UpdateManager automatically.
        /// Do not forget to replace your Late Update function with public override void LateUpdateMe()
        /// </summary>
        public virtual void LateUpdateMe() {
        }
    }
}
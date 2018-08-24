#if EZ_BIND
using Ez.Binding;
using Ez.Binding.Vars;
#endif
using UnityEngine;

namespace Ez.Examples
{
    public class SetRotation : MonoBehaviour
    {
        public bool Enabled = true;
        [Space(20)]
        [Range(-100, 100)]
        public float SpeedMultiplier = 1;
        [HideInInspector]
        public float Speed = 0f;

        [Space(20)]
        public bool getDirectionFromBind = false;
        public string directionBindName;
#if EZ_BIND
        private Bind directionBind = null;

        private Vector3 targetDirection = Vector3.zero;
        public Vector3 TargetDirection
        {
            get
            {
                if(!getDirectionFromBind) { return targetDirection; }
                if(directionBind == null) { directionBind = EzBind.FindBindByName(directionBindName); }
                if(directionBind == null) { return targetDirection; }
                return (Vector3) directionBind.Value;
            }
            set { targetDirection = value; }
        }
#endif
        [Space(20)]
        public bool getSpeedFromBind = false;
        public string speedBindName;
#if EZ_BIND
        private Bind speedBind = null;

        private float targetSpeed = 0;
        public float TargetSpeed
        {
            get
            {
                if(!getSpeedFromBind) { return targetSpeed; }
                if(speedBind == null) { speedBind = EzBind.FindBindByName(speedBindName); }
                if(speedBind == null) { return targetSpeed; }
                return (float) speedBind.Value;
            }
            set { targetSpeed = value; }
        }

        private Vector3 startRotation;

        private void Start()
        {
            startRotation = transform.localRotation.eulerAngles;
            if(!getDirectionFromBind) { targetSpeed = Speed; }
        }

        private void Update()
        {
            if(!Enabled) { transform.localRotation = Quaternion.Euler(startRotation); return; }
            transform.Rotate(TargetDirection * TargetSpeed * SpeedMultiplier * Time.deltaTime);
        }
#endif
    }
}

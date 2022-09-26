using UnityEngine;

namespace Utility
{
    public class BillBoard : MonoBehaviour
    {
        private Transform camTransform;
        private void Start()
        {
            camTransform = Camera.main.transform;

        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + camTransform.forward);
        }
    }
}

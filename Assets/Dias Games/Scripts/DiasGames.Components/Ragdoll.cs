using System.Collections.Generic;
using UnityEngine;

namespace DiasGames.Components
{
    public class Ragdoll : MonoBehaviour
    {
        private Animator _animator;      // reference to animator. It must be deactivated in order to ragdoll works

        // ragdoll rigidbodies
        private List<Rigidbody> _ragdollRigidbodies = new List<Rigidbody>();
        private List<Collider> _ragdollColliders = new List<Collider>();

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            GetRagdollReferences();
        }

        private void GetRagdollReferences()
        {
            if (_animator == null) return;

            for (int i = 0; i < 18; i++)
            {
                var bone = _animator.GetBoneTransform((HumanBodyBones)i);

                // try get rigidbody component
                if (bone.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = true; // deactivate physics
                    _ragdollRigidbodies.Add(rb);
                }
                
                // try get collider component
                if (bone.TryGetComponent(out Collider coll))
                {
                    coll.enabled = false;
                    _ragdollColliders.Add(coll);
                }
            }
        }

        public void ActivateRagdoll()
        {
            if (_animator == null) return;

            _animator.enabled = false;

            // activate rigidbodies
            _ragdollRigidbodies.ForEach(r => { 
                r.isKinematic = false;
                r.useGravity = true;
            });

            // activate colliders
            _ragdollColliders.ForEach(c => c.enabled = true);
        }
    }
}
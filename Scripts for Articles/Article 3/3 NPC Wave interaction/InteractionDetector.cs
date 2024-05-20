using UnityEngine;

namespace Tips.Part_3_End
{
    public class InteractionDetector : MonoBehaviour
    {
        [SerializeField]
        private float m_detectionRange = 1.0f;
        [SerializeField]
        private float m_detectionRadius = 0.5f;
        [SerializeField]
        private float m_height = 1.0f;
        [SerializeField]
        private LayerMask m_detectionLayer;

        public IInteractable CurrentInteractable { get; private set; }
        private Highlight m_currentHighlight;

        private void Update()
        {
            DetectInteractable();
        }

        private void DetectInteractable()
        {
            Collider[] result = Physics.OverlapSphere(transform.position + Vector3.up * m_height + transform.forward * m_detectionRange, m_detectionRadius, m_detectionLayer);
            if (result.Length > 0)
            {
                // Get the IInteractable and Highlight components from the hit object
                IInteractable interactable = result[0].GetComponent<IInteractable>();
                Highlight highlight = result[0].GetComponent<Highlight>();

                if (interactable is IActiveInteractable activeInteractable && activeInteractable.IsInteractionActive == false)
                {
                    ClearCurrentInteractable();
                    return;
                }


                if (interactable != CurrentInteractable)
                {
                    ClearCurrentInteractable();

                    CurrentInteractable = interactable;
                    m_currentHighlight = highlight;

                    if (m_currentHighlight != null)
                    {
                        m_currentHighlight.EnableHighlight();
                    }
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }

        private void ClearCurrentInteractable()
        {
            if (m_currentHighlight != null)
            {
                m_currentHighlight.DisableHighlight();
                m_currentHighlight = null;
            }

            CurrentInteractable = null;
        }

        private void OnDrawGizmosSelected()
        {
            // Draw the starting point of the sphere cast
            Gizmos.color = Color.blue;
            if (CurrentInteractable != null)
            {
                Gizmos.color = Color.green;
            }

            Vector3 center = transform.position + Vector3.up * m_height + transform.forward * m_detectionRange;
            Gizmos.DrawWireSphere(center, 0.5f);

        }
    }
}

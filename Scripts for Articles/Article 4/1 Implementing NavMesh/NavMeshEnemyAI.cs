using UnityEngine;

namespace Tips.Part_4_End
{
    public class NavMeshEnemyAI : MonoBehaviour
    {
        private NavMeshMovementInput m_navMeshMovementInput;
        [field: SerializeField]
        public Transform Target { get; set; }

        [SerializeField]
        private float m_stoppingDistance = 0.3f;


        private void Awake()
        {
            m_navMeshMovementInput = GetComponent<NavMeshMovementInput>();
        }

        private void Update()
        {
            if (m_navMeshMovementInput == null)
            {
                return;
            }
            if (Target == null)
            {
                m_navMeshMovementInput.SetTarget(Target);
                return;
            }
            if (Vector3.Distance(transform.position, Target.position) < m_stoppingDistance)
            {
                m_navMeshMovementInput.SetTarget(null);
                return;
            }
            m_navMeshMovementInput.SetTarget(Target);

        }
    }
}
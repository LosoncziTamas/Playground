using UnityEngine;

namespace Prototype01
{
    public class HitPointUi : MonoBehaviour
    {
        private const int TotalPointCount = 3;

        [SerializeField] private GameObject _hitPoint1;
        [SerializeField] private GameObject _hitPoint2;
        [SerializeField] private GameObject _hitPoint3;
        
        public void Reset()
        {
            SetPointsLeft(TotalPointCount);
        }

        public void SetPointsLeft(int pointCount)
        {
            _hitPoint1.SetActive(pointCount > 0);
            _hitPoint2.SetActive(pointCount > 1);
            _hitPoint3.SetActive(pointCount > 2);
        }
    }
}
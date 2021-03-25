using UnityEngine;

namespace Prototype01
{
	public class Player : MonoBehaviour
	{
		public float collisionImpact = 4.0f;
		
		private const string EnemyTag = "Enemy";

		[SerializeField] private HitPointUi _hitPointUi;
		
		private int _hitPoints = 3;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(EnemyTag))
			{
				Vector2 dir = other.transform.position - transform.position; 
				other.attachedRigidbody.AddForce(dir.normalized * collisionImpact, ForceMode2D.Impulse);
				_hitPointUi.SetPointsLeft(--_hitPoints);
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag(EnemyTag))
			{
				Debug.Log("OnTriggerExit2D");
			}
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if (other.CompareTag(EnemyTag))
			{
				Debug.Log("OnTriggerStay2D");
			}
		}
	}
}

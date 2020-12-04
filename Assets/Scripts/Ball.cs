using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
	[SerializeField] float _speed = 5f;
	[SerializeField] UnityEvent OnScoreUpdated;
	[SerializeField] ParticleSystem _collectParticles;

	Rigidbody _rigidbody;
	Vector3 _input;
	
	public int Score;
	[SerializeField] private float _oppositeSpeed = 10f;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
	}

	private void FixedUpdate()
	{
		var direction = _input;
		if (Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(_input.x))
			direction.x *= _oppositeSpeed;
		if (Mathf.Sign(_rigidbody.velocity.z) != Mathf.Sign(_input.y))
			direction.z *= _oppositeSpeed;

		_rigidbody.AddForce(direction.normalized * _speed);
	}


	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Collectible")) return;

		transform.DOScale(Vector3.one, .2f).From(Vector3.one * 1.2f);
		Instantiate(_collectParticles, other.transform.position, Quaternion.identity);

		Destroy(other.gameObject);

		Score++;
		OnScoreUpdated?.Invoke();
	}
}
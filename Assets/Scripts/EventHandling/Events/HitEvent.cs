using UnityEngine;

public class HitEvent : GameEvent {

	public readonly Collider collider;

	public HitEvent (Collider thisCollider) {
		collider = thisCollider;
	}
}

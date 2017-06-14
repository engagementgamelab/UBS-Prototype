using UnityEngine;

public class HitEvent : GameEvent {

	public readonly Collider collider;
	public readonly GameObject bubble;

	public HitEvent (Collider thisCollider, GameObject thisBubble) {
		collider = thisCollider;
		bubble = thisBubble;
	}
}

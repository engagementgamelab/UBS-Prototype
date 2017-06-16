using UnityEngine;

public class ScoreEvent : GameEvent {

	public readonly float scoreAmount;

	public ScoreEvent (float amount) {
		scoreAmount = amount;
	}
}

using UnityEngine;

public class BossIcon : MonoBehaviour
{
	private string iconName;
	
	public void Icon(string name)
	{
		Sprite img = Resources.Load<Sprite>("icons/icon_" + name);
		GetComponent<SpriteRenderer>().sprite = img;
		
		iconName = name;
	}
	
	void OnMouseUp() {
		
		Events.instance.Raise (new BossIconEvent(iconName, gameObject));

	}
}

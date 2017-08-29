using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class BossBattle : MonoBehaviour
{

	public GameObject icon;

	string[] types = new string[] {
		"carry water",
		"flush",
		"poop",
		"flush",
		"wash hands"
	};
	
	float[] xPos = new float[] { -3, -1.5f, 0, 1.5f, 3 };
	private int iconTouches = 0;
	
	IEnumerator CreateIcons() {
		
		Random rnd = new Random();
		float[] shuffledXPos = xPos.OrderBy(x => rnd.Next()).ToArray();  

		for(int i = 0; i < 5; i++)
		{
			yield return new WaitForSeconds(2);
			
			Vector3 targetPos = new Vector3(xPos[i], -2, 0);
			Vector3 randomPos = new Vector3(shuffledXPos[i], -2, 0);
			BossIcon obj = Instantiate(icon, Vector3.zero, Quaternion.identity).GetComponent<BossIcon>();
			obj.label.text = types[i];

			iTween.MoveTo(obj.gameObject, iTween.Hash("position", targetPos, "islocal", true, "time", 1, "delay", 2+i));
			iTween.MoveTo(obj.gameObject, iTween.Hash("position", randomPos, "islocal", true, "time", 1, "delay", 10));
		}
		
	}
	
	void OnIconEvent(BossIconEvent e)
	{
		iconTouches++;
		if(iconTouches == 5)
		{
			iconTouches = 0;
			StartCoroutine(CreateIcons());
		}
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(CreateIcons());
		Events.instance.AddListener<BossIconEvent> (OnIconEvent);
	}

	void OnDestroyed()
	{
		Events.instance.RemoveListener<BossIconEvent> (OnIconEvent);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

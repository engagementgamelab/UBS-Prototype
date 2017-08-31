using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class BossBattle : MonoBehaviour
{

	public GameObject icon;
	public GameObject iconParent;
	
	public Text countdownText;
	public Text overText;
	public Button restartButton;

	string[] types = new string[] {
		"carry_water",
		"flush",
		"poop",
		"flush",
		"wash_hands"
	};
	
	float[] xPos = new float[] { -2.5f, -1.25f, 0, 1.25f, 2.5f };
	private int iconTouches = 0;
	private int timeLeft = 20;

	void Countdown()
	{

		timeLeft--;

		if(timeLeft == 0)
		{
			restartButton.gameObject.SetActive(true);
			overText.gameObject.SetActive(true);
			countdownText.gameObject.SetActive(false);
			
			return;
		}
		
		countdownText.text = timeLeft + "";

	}
	
	IEnumerator CreateIcons(bool animateIn=false) {
		
		Random rnd = new Random();
		float delay = animateIn ? .7f : .3f;
		float[] shuffledXPos = xPos.OrderBy(x => rnd.Next()).ToArray();
		Vector3[] targets = GameObject.FindGameObjectsWithTag("Waypoint").Select(g => g.transform.position).ToArray();
		Vector3[] shuffledTargets = targets.OrderBy(x => rnd.Next()).ToArray();

		for(int i = 0; i < 5; i++)
		{
			
			int sort = 2*i;
			yield return new WaitForSeconds(delay);
			
			Vector3 targetPos = iconParent ? targets[i] : new Vector3(xPos[i], -2, 0);
			Vector3 randomPos = iconParent ? shuffledTargets[i] : new Vector3(shuffledXPos[i], -2, 0);
			BossIcon obj = Instantiate(icon, Vector3.zero, Quaternion.identity).GetComponent<BossIcon>();
			obj.gameObject.SetActive(false);

			if(iconParent != null)
			{
				obj.transform.parent = iconParent.transform;
				obj.transform.localPosition = targetPos;
				obj.gameObject.SetActive(true);
				
			}

			obj.Icon(types[i]);
			
			SpriteRenderer[] rends = obj.gameObject.GetComponentsInChildren<SpriteRenderer>();

			foreach(SpriteRenderer rend in rends)
			{
				rend.sortingOrder = i + sort;
				sort--;
			}

			if(animateIn)
				iTween.MoveTo(obj.gameObject, iTween.Hash("position", targetPos, "time", 1, "delay", 4.5f-(delay*i)));
			
			iTween.MoveTo(obj.gameObject, iTween.Hash("position", randomPos, "islocal", true, "time", animateIn ? 1 : .5f, "delay", animateIn ? 6f-(delay*i) : 0));
			
		}
		
		yield return new WaitForSeconds(3.5f);
		
		InvokeRepeating("Countdown", 0, 1);
		
	}
	
	void OnIconEvent(BossIconEvent e)
	{
		if(e.iconType != types[iconTouches])
		{
			GameObject[] icons = GameObject.FindGameObjectsWithTag("BossIcon");
			foreach (GameObject icon in icons)
			{
				iTween.ShakePosition(icon, Vector3.one * UnityEngine.Random.Range(.05f, .1f), 1);
			}
			return;
		}
		
		iconTouches++;
		Destroy(e.obj);
		
		if(iconTouches == 5)
		{
			iconTouches = 0;
			StartCoroutine(CreateIcons());
		}
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(CreateIcons(true));
		Events.instance.AddListener<BossIconEvent> (OnIconEvent);
	}

	void OnDestroy()
	{
		Events.instance.RemoveListener<BossIconEvent> (OnIconEvent);
		
	}
	
}

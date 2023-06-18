using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Platformer.Mechanics;

public class MainMenuHandler : MonoBehaviour {
	public Button button;
	public PlayerController player;
	public AudioSource mainMusic;
	public AudioSource voteMusic;

	public VoteHandler vote;
	public InteractableHandler interactable;
	public UICollectable collectable;
	public PointsHandler points;
	public Timer timer;
	public SceneHandler dialog;
	public GameObject collectablesParent;

	public GameObject voteUI;
	public GameObject interactableUI;
	public GameObject collectableUI;
	public GameObject pointsUI;
	public GameObject timerUI;
	public GameObject dialogUI;

	void Start () 
	{
		button.onClick.AddListener(TaskOnClick);
	}

	void OnEnable ()
	{
		mainMusic.Play();
		voteMusic.Stop();
		voteUI.SetActive(false);
		interactableUI.SetActive(false);
		collectableUI.SetActive(false);
		pointsUI.SetActive(false);
		timerUI.SetActive(false);
		dialogUI.SetActive(false);

		player.initialize();
		vote.initialize();
		interactable.initialize();
		collectable.initialize();
		points.initialize();
		timer.initialize();
		dialog.initialize();
		
		foreach ( Transform child in collectablesParent.transform) 
		{
        	child.gameObject.SetActive(true);
    	}

		player.gameObject.transform.position = new Vector3(-80.62f,-16.8f,0f); 
	}

	void TaskOnClick()
	{
		this.gameObject.SetActive(false);
		timerUI.SetActive(true);
		collectableUI.SetActive(true);
		player.controlEnabled = true;
	}
}
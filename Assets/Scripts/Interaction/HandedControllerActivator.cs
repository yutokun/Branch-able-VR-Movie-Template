using UnityEngine;

public class HandedControllerActivator : MonoBehaviour
{
	[SerializeField] GameObject left, right;

	void Update()
	{
		if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote)
		{
			left.SetActive(true);
			right.SetActive(false);
		}
		else
		{
			left.SetActive(false);
			right.SetActive(true);
		}
	}
}
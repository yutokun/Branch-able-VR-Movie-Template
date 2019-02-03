using UnityEngine;
using UnityEngine.SceneManagement;

public class Repeater : MonoBehaviour
{
#if UNITY_EDITOR
	static bool BackButton => Input.GetKeyDown(KeyCode.Backspace);
#else
	static bool BackButton => OVRInput.GetDown(OVRInput.Button.Back);
#endif

	void Update()
	{
		if (BackButton)
		{
			System.GC.Collect();
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
using UnityEngine;

/// <summary>
/// 左手と右手のどちらでも正しくポインターを操作できるようにします。
/// </summary>
public class HandedControllerActivator : MonoBehaviour
{
	[SerializeField] GameObject left, right;

	void Update()
	{
		if (OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote) //左のコントローラーが接続されていれば
		{
			left.SetActive(true);   //左手のポインターをオンに
			right.SetActive(false); //右手のポインターをオフに
		}
		else //そうでなければ
		{
			left.SetActive(false); //左手のポインターをオフに
			right.SetActive(true); //右手のポインターをオンに
		}
	}
}
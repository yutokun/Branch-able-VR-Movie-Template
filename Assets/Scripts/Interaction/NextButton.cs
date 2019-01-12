using UnityEngine;

/// <summary>
/// 選択肢ボタンの動作を定義します
/// </summary>
public class NextButton : MonoBehaviour, IPointable
{
	public Video video;

	//コントローラーで指した時に
	public void Pointed()
	{
		//効果音を再生します
		SoundEffectPlayer.PlayPointedSound();
	}

	public void Unpointed()
	{
	}

	//コントローラーでクリックしたときに
	public void Click()
	{
		SoundEffectPlayer.PlayClickSound();              //クリック音を再生し
		video.Play();                                    //動画を再生し
		GetComponentInParent<BranchCreator>().Destroy(); //選択肢ボタンを削除します
	}
}
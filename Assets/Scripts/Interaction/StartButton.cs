using UnityEngine;

public class StartButton : MonoBehaviour, IPointable
{
	[SerializeField] PlaybackController playback;

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
		SoundEffectPlayer.PlayClickSound();            //クリック音を再生し
		Tween.Scale(this, transform.parent, 0f, 0.5f); //消えるまで小さくし
		Invoke(nameof(Play), 0.5f);                    //動画を再生します（真下の関数）
	}

	void Play()
	{
		playback.PlayFirstVideo();            //最初の動画を再生します
		Destroy(transform.parent.gameObject); //スタートボタンを削除します
	}

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return)) Click(); //Unity 上では Enter キーで開始できるように
	}
#endif
}
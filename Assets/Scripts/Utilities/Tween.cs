using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Tween
{
	static readonly Dictionary<int, Coroutine> PlayingTweens = new Dictionary<int, Coroutine>();

	// TODO : Tween の種類ごとに処理できるように（必要になったら・・・）
	static void StopTweenOnSameTarget(Object target)
	{
		var hash = target.GetHashCode();
		if (PlayingTweens.ContainsKey(hash))
		{
			Coroutine coroutine;
			PlayingTweens.TryGetValue(hash, out coroutine);
			if (coroutine != null) TweenBase.Instance.StopCoroutine(coroutine);
			PlayingTweens.Remove(hash);
		}
	}

	public static Coroutine Scale(this Transform target, float scale, float duration, Action onComplete = null)
	{
		StopTweenOnSameTarget(target);
		var coroutine = TweenBase.Instance.StartCoroutine(ScaleCoroutine(target, scale, duration, onComplete));
		PlayingTweens.Add(target.GetHashCode(), coroutine);
		return coroutine;
	}

	static IEnumerator ScaleCoroutine(Transform target, float scale, float duration, Action onComplete = null)
	{
		var initial = target.localScale;
		var startTime = Time.time;
		var targetScale = Vector3.one * scale;
		while (target.localScale != targetScale)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.localScale = Vector3.Lerp(initial, targetScale, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}

	public static Coroutine FadeText(this TextMeshPro target, Color color, float duration, Action onComplete = null)
	{
		StopTweenOnSameTarget(target);
		var coroutine = TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration, onComplete));
		PlayingTweens.Add(target.GetHashCode(), coroutine);
		return coroutine;
	}

	public static Coroutine FadeTextAlpha(this TextMeshPro target, float alpha, float duration, Action onComplete = null)
	{
		StopTweenOnSameTarget(target);
		var color = target.color;
		color.a = Mathf.Clamp01(alpha);
		var coroutine = TweenBase.Instance.StartCoroutine(FadeTextCoroutine(target, color, duration, onComplete));
		PlayingTweens.Add(target.GetHashCode(), coroutine);
		return coroutine;
	}

	static IEnumerator FadeTextCoroutine(TextMeshPro target, Color color, float duration, Action onComplete = null)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}

	public static Coroutine FadeMaterial(Material target, Color color, float duration, Action onComplete = null)
	{
		StopTweenOnSameTarget(target);
		var coroutine = TweenBase.Instance.StartCoroutine(FadeMaterialCoroutine(target, color, duration, onComplete));
		PlayingTweens.Add(target.GetHashCode(), coroutine);
		return coroutine;
	}

	static IEnumerator FadeMaterialCoroutine(Material target, Color color, float duration, Action onComplete = null)
	{
		var initial = target.color;
		var startTime = Time.time;
		while (target.color != color)
		{
			var lerpTime = (Time.time - startTime) / duration;
			target.color = Color.Lerp(initial, color, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}

	public static Coroutine FadeVolume(this AudioSource audio, float volume, float duration, Action onComplete = null)
	{
		StopTweenOnSameTarget(audio);
		var coroutine = TweenBase.Instance.StartCoroutine(FadeVolumeCoroutine(audio, volume, duration, onComplete));
		PlayingTweens.Add(audio.GetHashCode(), coroutine);
		return coroutine;
	}

	static IEnumerator FadeVolumeCoroutine(AudioSource audio, float volume, float duration, Action onComplete = null)
	{
		var initial = audio.volume;
		var startTime = Time.time;
		while (audio.volume != volume)
		{
			var lerpTime = (Time.time - startTime) / duration;
			audio.volume = Mathf.Lerp(initial, volume, lerpTime);
			yield return null;
		}

		onComplete?.Invoke();
	}
}
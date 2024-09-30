using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WindowView : MonoBehaviour
{
	private static Stack<CanvasGroup> s_previousOpenedWindows = new();
	//private static int s_amountOpenedWindowsOnStart = -1;
	
	private CanvasGroup _canvasGroup;

	private const float k_transitionDuration = 0.2f;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();

		/*if (s_amountOpenedWindowsOnStart == -1)
		{
			s_amountOpenedWindowsOnStart = 0;
			
			var allCanvasGroups = FindObjectsOfType<CanvasGroup>();
			foreach (var eachWindow in allCanvasGroups)
			{
				if (eachWindow.alpha > 0.1f)
					s_amountOpenedWindowsOnStart++;
			}
		}*/
		
		if (_canvasGroup.alpha > 0.1f)
		{
			s_previousOpenedWindows.Push(_canvasGroup);
			//s_amountOpenedWindowsOnStart--;

			/*if (s_amountOpenedWindowsOnStart == 0)
			{
				var reversedList = s_previousOpenedWindows.ToList();
				
				s_previousOpenedWindows.Clear();
				
				foreach(var eachWindow in reversedList)
					s_previousOpenedWindows.Push(eachWindow);
			}*/
				
		}
			
	}

	[ContextMenu("HideAllWindowsAndShowThis")]
	private void HideAllWindowsAndShowThis()
	{
		var allWindows = FindObjectsOfType<CanvasGroup>();
		foreach (var eachWindow in allWindows)
		{
			eachWindow.blocksRaycasts = false;
			eachWindow.alpha = 0;
		}

		var thisWindow = GetComponent<CanvasGroup>();
		thisWindow.blocksRaycasts = true;
		thisWindow.alpha = 1;
	}

	[ContextMenu("ShowThisWindowUpon")]
	private void ShowThisWindowUpon()
	{
		var thisWindow = GetComponent<CanvasGroup>();
		thisWindow.blocksRaycasts = true;
		thisWindow.alpha = 1;
	}
	
	[ContextMenu("HideThisWindow")]
	private void HideThisWindow()
	{
		var thisWindow = GetComponent<CanvasGroup>();
		thisWindow.blocksRaycasts = false;
		thisWindow.alpha = 0;
	}

	public static CanvasGroup GetLastOpenedWindow()
	{
		return s_previousOpenedWindows.Peek();
	}
	
	public static void ClosePreviousWindow()
	{
		if (s_previousOpenedWindows.Count > 0)
		{
			var retrievedPreviousWindow = s_previousOpenedWindows.Pop();
			
			retrievedPreviousWindow.blocksRaycasts = false;
			retrievedPreviousWindow.DOFade(0, k_transitionDuration);
		}
	}

	public static void ClosePreviousAndShowThisWindow(CanvasGroup windowForShow, Action onShowingCompleted = null)
	{
		ClosePreviousWindow();
		
		windowForShow.blocksRaycasts = true;
		windowForShow.DOFade(1, k_transitionDuration).OnComplete(() =>
		{
			onShowingCompleted?.Invoke();
		});
		
		s_previousOpenedWindows.Push(windowForShow);
	}

	public bool IsWindowOpened()
	{
		return _canvasGroup.alpha > 0;
	}

	public void ClosePreviousAndShowThisWindow(Action onShowingCompleted = null)
	{
		ClosePreviousWindow();
		
		_canvasGroup.blocksRaycasts = true;
		_canvasGroup.DOFade(1, k_transitionDuration).OnComplete(() =>
		{
			onShowingCompleted?.Invoke();
		});
		
		s_previousOpenedWindows.Push(_canvasGroup);
	}

	public void ClosePreviousAndShowThisWindow()
	{
		ClosePreviousAndShowThisWindow(null);
	}
	
	public void ShowWindowUponAnotherWindows(bool show)
	{
		ShowWindowUponAnotherWindows(show, null);
	}

	public void ShowWindowUponAnotherWindows(bool show, Action onShowingCompleted = null, bool pushIntoStackOfOpenedWindows = true)
	{
		_canvasGroup.blocksRaycasts = show;
		_canvasGroup.DOFade(show ? 1 : 0, k_transitionDuration).OnComplete(() =>
		{
			onShowingCompleted?.Invoke();
		});
		
		if(!pushIntoStackOfOpenedWindows)
			return;

		if (show)
		{
			s_previousOpenedWindows.Push(_canvasGroup);
		}
		else
		{
			var listOfWindows = s_previousOpenedWindows.ToList();
			listOfWindows.Remove(_canvasGroup);
			s_previousOpenedWindows = new Stack<CanvasGroup>(listOfWindows);
		}
	}
}

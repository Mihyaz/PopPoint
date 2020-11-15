using System.Collections;
using UnityEngine;
using MihyazUtils.Events;

public enum EventTypes
{
	OnGameOver,
	OnGameRestart
}

public class EventBase : Events<EventTypes>
{
	public void Initialize()
	{
		Add(EventTypes.OnGameOver);
		Add(EventTypes.OnGameRestart);
	}
}


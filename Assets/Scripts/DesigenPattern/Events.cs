using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
	public static PlayerWin PlayerWin = new PlayerWin();
	public static PlayerLoss PlayerLoss = new PlayerLoss();
}

public class PlayerWin : GameEvent { }
public class PlayerLoss : GameEvent { }

public class GameStart : GameEvent { }



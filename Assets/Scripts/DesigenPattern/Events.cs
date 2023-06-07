using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
	public static GameClear GameClear = new GameClear();
	public static GameOver GameOver = new GameOver();
	public static PlayerHeathUpdate PlayerHeathUpdate = new PlayerHeathUpdate();
	public static PlayerDead PlayerDead = new PlayerDead();
	public static EnemyAllKill EnemyAllKill = new EnemyAllKill();
	public static EnemyKill EnemyKill = new EnemyKill();
}

public class GameClear : GameEvent { }
public class GameOver : GameEvent { }

public class PlayerHeathUpdate :GameEvent { public Health PlayerHealth; }

public class PlayerDead : GameEvent { }

public class EnemyAllKill: GameEvent { }

public class EnemyKill : GameEvent { public Enemy enemy; }


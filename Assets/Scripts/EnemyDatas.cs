using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EnemyType
{
	Noraml,
	Speed,
	Heavy,
}

public class EnemyDatas
{
	public static string[] EnemyTypeStrings = 
	{
		EnemyType.Noraml.ToString(),
		EnemyType.Speed.ToString(),
		EnemyType.Heavy.ToString()
	};

	public static int[] EnemyTypeValues =
	{
		(int)ObjectPoolKey.Enemy_Normal,
		(int)ObjectPoolKey.Enemy_Speed,
		(int)ObjectPoolKey.Enemy_Heavy,
	};
}

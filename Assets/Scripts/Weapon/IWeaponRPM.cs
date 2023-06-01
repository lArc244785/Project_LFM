using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWeaponInfo
{
	public string Name { get; }
	public int BaseRPM { get; }
	public int AddRPM { get; set; }
}
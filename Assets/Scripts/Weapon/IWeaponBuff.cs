using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWeaponBuff
{
	public BulletType BulletType { set; get; }
	public float FireCoolRatio { set; get; }
}
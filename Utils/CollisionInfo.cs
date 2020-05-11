using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

public class CollisionInfo
{
	public readonly GameObject other;
	public readonly float timeOfImpact;

	public bool horizontal;

	public CollisionInfo(bool pHorizontal, GameObject pOther, float pTimeOfImpact)
	{
		horizontal = pHorizontal;
		other = pOther;
		timeOfImpact = pTimeOfImpact;
	}
}


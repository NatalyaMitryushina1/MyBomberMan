﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectCreator
{
	public static StaticObjectsGeneratorBase StaticObjects()
	{
		return new StaticObjectsGenerator ();
	}
		
}
﻿using System;
using System.Collections;

namespace CubeWar {
	public interface IEventListener {

		void AddEventListener (string name, Action<object> onEvent);
		void RemoveEventListener (string name, Action<object> onEvent);
		void RemoveAllEventListener (string name);
	
	}
}

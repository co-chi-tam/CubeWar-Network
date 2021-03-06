﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CubeWar {
	[Serializable]
	public class CObjectData : CBaseData {

		public string name;
		public CEnum.EObjectType objectType;

		public string fsmPath;
		public string modelPath;
		public string avatarPath;

		public int currentHealth;
		public int maxHealth;

		public CObjectData () : base () {
			this.name = string.Empty;
			this.objectType = CEnum.EObjectType.None;

			this.fsmPath = string.Empty;
			this.modelPath = string.Empty;
			this.avatarPath = string.Empty;

			this.currentHealth = 0;
			this.maxHealth = 0;

		}

	}
}

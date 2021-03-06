﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CubeWar {
	public class CMovableComponent : CComponent {

		public Transform currentTransform;
		public Vector3 targetPosition;

		protected UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
		protected IMovable m_Target;
		protected float m_Angle;
		protected Vector3 m_Direction;

		public static Dictionary<string, IMovable> MovableObjects = new Dictionary<string, IMovable> ();

		public CMovableComponent (IMovable movable, UnityEngine.AI.NavMeshAgent navMeshAgent) : base()
		{
			m_Angle 			= 0f;
			m_Target 			= movable;
			m_NavMeshAgent 		= navMeshAgent;

			if (CMovableComponent.MovableObjects.ContainsKey (m_Target.GetID ()) == false) {
				CMovableComponent.MovableObjects.Add (m_Target.GetID (), m_Target);
			}
		}

		public override void UpdateComponent(float dt) {
			base.UpdateComponent (dt);
		} 

		public virtual void MoveForwardToTarget(float dt) {
			m_Direction = targetPosition - currentTransform.position;
			m_Angle = Mathf.Atan2 (m_Direction.x, m_Direction.z) * Mathf.Rad2Deg;
			var position = m_Direction.normalized * m_Target.GetMoveSpeed () * dt;
			if (m_NavMeshAgent.isOnNavMesh) {
				position *= 0.5f;
				m_NavMeshAgent.Move (position);
			} else {
				currentTransform.position = Vector3.Lerp (currentTransform.position, currentTransform.position + position, 0.5f);
			}
			currentTransform.rotation = Quaternion.Lerp (currentTransform.rotation, Quaternion.AngleAxis (m_Angle, Vector3.up), 0.5f);
#if UNITY_EDITOR
			Debug.DrawRay (currentTransform.position, m_Direction, Color.green);	
#endif
			Reset ();
		}

		public virtual void LookForwardToTarget(Vector3 value) {
			m_Direction = value - currentTransform.position;
			var forward = currentTransform.forward;
			m_Angle = Mathf.Atan2 (m_Direction.x, m_Direction.z) * Mathf.Rad2Deg;
			currentTransform.rotation = Quaternion.Lerp (currentTransform.rotation, Quaternion.AngleAxis (m_Angle, Vector3.up), 0.5f);
		}

		protected virtual void Reset() {
			
		}

		public virtual bool DidMoveToTarget() {
			if (currentTransform == null)
				return true;
			m_Direction = targetPosition - currentTransform.position;
			return m_Direction.sqrMagnitude <= GetMaxDistance();
		}

		public virtual bool DidMoveToTarget(Transform target) {
			if (target == null)
				return true;
			if (currentTransform == null)
				return true;
			var direction = target.position - currentTransform.position;
			return direction.sqrMagnitude <= GetMaxDistance();
		}

		public virtual bool DidMoveToTarget(Vector3 target) {
			if (currentTransform == null)
				return true;
			var direction = target - currentTransform.position;
			return direction.sqrMagnitude <= GetMaxDistance();
		}

		private float GetMaxDistance() {
			return m_Target.GetDistanceToTarget () * m_Target.GetDistanceToTarget ();
		}
	}
}

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Tangatek.ViewManagement
{
	public interface LerpAgent
	{
        View.Phase LerpType { get; }
        IEnumerator LerpBegin(float speed); 
	}

    public static class LerpAgentExtensions
    {
        /// <summary>
        /// Locate all LerpAgents in children
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static LerpAgent[] GetLerpAgentsInChildren(this MonoBehaviour source)
        {
            List<LerpAgent> list = new List<LerpAgent>();
            var behaviours = source.GetComponentsInChildren<MonoBehaviour>();
            foreach (var behaviour in behaviours)
                if (behaviour is LerpAgent)
                    list.Add((LerpAgent)behaviour);
            return list.ToArray();
        }
    }
}

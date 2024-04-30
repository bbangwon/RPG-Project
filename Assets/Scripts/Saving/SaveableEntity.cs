using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System.Text.Json;
using System.Collections.Generic;
using System;





#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();
        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = JsonSerializer.Deserialize<Dictionary<string, object>>(state.ToString());
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeName = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeName))
                {
                    string value = stateDict[typeName].ToString();
                    //JsonSerializer가 string으로 Deserialize할때 처리못하는 이슈가 있어..
                    if (saveable.GetStateType() == typeof(string))
                    {
                        saveable.RestoreState(value);
                        continue;
                    }

                    object stateObject = JsonSerializer.Deserialize(value, saveable.GetStateType());
                    saveable.RestoreState(stateObject);
                }
            }
            //GetComponent<NavMeshAgent>().enabled = false;

            //SerializableVector3 serializablePostion = JsonSerializer.Deserialize<SerializableVector3>(state.ToString());
            //transform.position = serializablePostion.ToVector();

            //GetComponent<NavMeshAgent>().enabled = true;
            //GetComponent<ActionScheduler>().CancelCurrentAction();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return; // "This is a prefab

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();                
                serializedObject.ApplyModifiedProperties();
            }
            globalLookup[property.stringValue] = this;
        }

        private bool IsUnique(string candidate)
        {
            if(!globalLookup.ContainsKey(candidate)) return true;
            if (globalLookup[candidate] == this) return true;
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }


            return false;
        }
#endif
    }
}
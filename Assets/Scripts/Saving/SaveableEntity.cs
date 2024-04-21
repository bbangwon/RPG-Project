using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System.Text.Json;
using System.Collections.Generic;




#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";
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
                    object stateObject = JsonSerializer.Deserialize(stateDict[typeName].ToString(), saveable.GetStateType());
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
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}
using UnityEngine;
using UnityEditor;

namespace A1{
[CustomEditor(typeof(AgentStatus))]
public class AgentStatusEditor : Editor {

	public override void OnInspectorGUI() {
        if(!Application.isPlaying) return;
		AgentStatus self = (AgentStatus)target;
		EditorGUILayout.LabelField(self.status);
    }

}
}

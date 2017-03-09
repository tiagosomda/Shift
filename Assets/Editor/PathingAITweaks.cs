using UnityEditor;

/* PathingAITweaks - v1.0
 *    Modifies the inspector to account for conflicting
 *    variables in the target script.
 * 
 * CoScripts: PathingAI
 * Scribes: Ryan Wood
 */
[CustomEditor(typeof(PathingAI))]
public class PathingAITweaks : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		PathingAI script = (PathingAI) target;
		
		// variable alterations
		if (!script.loop)
			script.iteration = EditorGUILayout.IntField("Iteration", script.iteration);
		if (!script.reverse)
			script.randomPatrol = EditorGUILayout.Toggle("Random Patrol", script.randomPatrol);
		if (!script.randomPatrol)
			script.reverse = EditorGUILayout.Toggle("Reverse", script.reverse);
	}	
	
}

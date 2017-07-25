using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR

using UnityEditor;

public class NormalVisualizationWindows : EditorWindow {

	private Camera _camera;
	private RenderTexture _renderTexture;
	private int _screenWidth, _screenHeight;

	[MenuItem ("Window/Normal Visualization")]
	public static void ShowWindows() {
		EditorWindow.GetWindow<NormalVisualizationWindows> ();
	}

	void Awake() {
		NormalVisualization script = GameObject.FindObjectOfType<NormalVisualization> ();
		_camera = script._camera;
		if (_camera != null) {
			_screenWidth = (int)position.width;
			_screenHeight = (int)position.height;
			_renderTexture = RenderTexture.GetTemporary (_screenWidth, _screenHeight);
		}
		autoRepaintOnSceneChange = true;
	}

	void Update() {
		if (_camera != null) {
			if ((int)position.width != _screenWidth || (int)position.height != _screenHeight) {
				_screenWidth = (int)position.width;
				_screenHeight = (int)position.height;
				RenderTexture.ReleaseTemporary (_renderTexture);
				_renderTexture = RenderTexture.GetTemporary (_screenWidth, _screenHeight);
			}
			_camera.targetTexture = _renderTexture;
			_camera.Render ();
			_camera.targetTexture = null;
		}
	}

	void OnGUI() {
		if (_camera != null) {
			GUI.DrawTexture (new Rect (0, 0, position.width, position.height), _renderTexture);
		}
	}
}

#endif
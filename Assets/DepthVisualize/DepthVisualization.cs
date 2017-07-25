using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DepthVisualization : MonoBehaviour {
	public Camera _camera;
	public Material _mat;
	// Use this for initialization
	void Start () {
		if (_camera == null)
			_camera = GetComponent<Camera> ();
		if (_camera != null)
			_camera.depthTextureMode = DepthTextureMode.Depth;
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, _mat);
	}
}

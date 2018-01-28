using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScannerEffectDemo : MonoBehaviour
{
	public Transform ScannerOrigin;
	public Transform Snake;
	public Material EffectMaterial;
	public float ScanDistance = 0;
	public float CloseDistance = 30;
	public float ScanHalfWayDistance = 0;
	private Camera _camera;

	// Demo Code
	bool _scanning;
	HamsterController[] _scannables;

	void Start()
	{
    }

	void Update()
	{
		_scannables = FindObjectsOfType<HamsterController>();

		if (_scanning)
		{
			ScanDistance += Time.deltaTime * 50;
			foreach (HamsterController s in _scannables)
			{
				if(Vector3.Distance(ScannerOrigin.position, s.transform.position) < ScanDistance && CloseDistance > ScanDistance )
				{
					s.Freeze();
					s.SetVisibility(true, 4.0f);
				}
				else if (Vector3.Distance(ScannerOrigin.position, s.transform.position) < ScanDistance && Vector3.Distance(ScannerOrigin.position, s.transform.position) > ScanDistance)
				{
					s.SetVisibility(true, 4.0f);
				}
				 
			}
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			_scanning = true;
			ScanDistance = 0;
		}
	}
	// End Demo Code


	public void ScanAtPosition(Vector3 ScanPos)
	{
		_scanning = true;
		ScanDistance = 0;
        if (transform != null)
        {
            ScannerOrigin.position = Snake.position;
        }
	}
	void OnEnable()
	{
		_camera = GetComponent<Camera>();
		_camera.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		EffectMaterial.SetVector("_WorldSpaceScannerPos", ScannerOrigin.position);
		EffectMaterial.SetFloat("_ScanDistance", ScanDistance);
		RaycastCornerBlit(src, dst, EffectMaterial);
	}

	void RaycastCornerBlit(RenderTexture source, RenderTexture dest, Material mat)
	{
		// Compute Frustum Corners
		float camFar = _camera.farClipPlane;
		float camFov = _camera.fieldOfView;
		float camAspect = _camera.aspect;

		float fovWHalf = camFov * 0.5f;

		Vector3 toRight = _camera.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
		Vector3 toTop = _camera.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

		Vector3 topLeft = (_camera.transform.forward - toRight + toTop);
		float camScale = topLeft.magnitude * camFar;

		topLeft.Normalize();
		topLeft *= camScale;

		Vector3 topRight = (_camera.transform.forward + toRight + toTop);
		topRight.Normalize();
		topRight *= camScale;

		Vector3 bottomRight = (_camera.transform.forward + toRight - toTop);
		bottomRight.Normalize();
		bottomRight *= camScale;

		Vector3 bottomLeft = (_camera.transform.forward - toRight - toTop);
		bottomLeft.Normalize();
		bottomLeft *= camScale;

		// Custom Blit, encoding Frustum Corners as additional Texture Coordinates
		RenderTexture.active = dest;

		mat.SetTexture("_MainTex", source);

		GL.PushMatrix();
		GL.LoadOrtho();

		mat.SetPass(0);

		GL.Begin(GL.QUADS);

		GL.MultiTexCoord2(0, 0.0f, 0.0f);
		GL.MultiTexCoord(1, bottomLeft);
		GL.Vertex3(0.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 0.0f);
		GL.MultiTexCoord(1, bottomRight);
		GL.Vertex3(1.0f, 0.0f, 0.0f);

		GL.MultiTexCoord2(0, 1.0f, 1.0f);
		GL.MultiTexCoord(1, topRight);
		GL.Vertex3(1.0f, 1.0f, 0.0f);

		GL.MultiTexCoord2(0, 0.0f, 1.0f);
		GL.MultiTexCoord(1, topLeft);
		GL.Vertex3(0.0f, 1.0f, 0.0f);

		GL.End();
		GL.PopMatrix();
	}
}

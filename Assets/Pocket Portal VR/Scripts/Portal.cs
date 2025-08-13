/*
 * Copyright (c) 2017 VR Stuff
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.VR;
using UnityEngine.Assertions;

#if USES_AR_KIT
using UnityEngine.XR.iOS;
#endif

#if USES_AR_FOUNDATION
using UnityEngine.XR.ARFoundation;
#endif

enum TriggerAxis
{
	X,
	Y,
	Z
};

public class Portal : MonoBehaviour
{
	[Tooltip("The \"From\" dimension for this portal.")]
	public Dimension dimension1;

	[Tooltip("The \"To\" dimension for this portal.")]
	public Dimension dimension2;

    [Tooltip("Increase this value to increase quality. Lower this value to increase performance. Default is 1.")]
    public float renderQuality = 1f;

    [Tooltip("The maximum distance at which the portal begins to deform away from the main camera to avoid clipping.")]
	public float maximumDeformRange = 1f;

	[Tooltip("The power with which the portal deforms away from the camera. If you see a flicker as you pass through, increase this variable.")]
	public float deformPower = .5f;

	[Tooltip("The portal deforms away from rigidbodies, but if you give them a tag and set it here, it will ignore them. This is really good for the Vive Controllers.")]
	public string ignoreRigidbodyTag;

	[Tooltip("This mask defines the parts of the scene that are visible in all dimensions. Always set the layers individually, don't use Everything.")]
	public LayerMask alwaysVisibleMask;

	[Tooltip("Oblique Projection clips the camera exactly to the portal rectangle. This is really good if you've got nearby objects. Unfortunately, it also screws with the skybox on Gear VR.")]
	public bool enableObliqueProjection = false;

	// Test against this bool to see if you are currently inside of the starting dimension, or the ending dimension for the portal.
	public bool dimensionSwitched { get; private set; }

	[TextArea]
	[Tooltip(" ")]
	public string Notes = "Hover over each variable to get tooltips with more information on what they do. Quick Tip: " +
		"Don't set the visible mask to Everything. Select each option you want to be always visible.";


    private Camera mainCamera;
    private Camera rightCamera;

    private float minimumDeformRangeSquared;
	private bool isDeforming = false;

	private Renderer meshRenderer;
	private MeshFilter meshFilter;
	private MeshDeformer meshDeformer;

	private bool triggerZDirection;

	private List<PortalTransitionObject> transitionObjects = new List<PortalTransitionObject> ();

	// Rendering & VR Support
	private Camera renderCam;
	private Skybox camSkybox;
	private RenderTexture leftTexture;
#if USES_STEAM_VR || USES_OPEN_VR
	private RenderTexture rightTexture;
#endif

    private float portalSwitchDistance = 0.03f;

	private bool forceRenderNextFrame = true;

	//void LateUpdate()
	//{
	//	if (forceRenderNextFrame && renderCam != null && meshRenderer != null)
	//	{
	//		//RenderMono(mainCamera); // force manual render once
	//		forceRenderNextFrame = false;
	//	}
	//}

	//fun little terrain line
	public nextLine line;
	void OnPreCull()
	{
		if (renderCam != null && meshRenderer != null && Camera.current == mainCamera)
		{
			RenderPortalViewFromMainCamera();
		}
	}
	//IEnumerator DelayedRender()
	//{
	//	yield return new WaitForEndOfFrame(); // let Unity complete initial camera setup
	//	RenderMono(mainCamera);
	//}

	void Awake() {
#if USES_STEAM_VR
		Debug.Log("This build is set up to run with Steam VR (Vive / Rift). To enable another headset or run without VR please edit your settings in Window -> Portal State Manager.");
#elif USES_OPEN_VR
		Debug.Log("This build is set up to run with Open VR (Rift / Gear VR). To enable another headset or run without VR please edit your settings in Window -> Portal State Manager.");
#elif USES_AR_KIT
		Debug.Log("This build is set up to run with the ARKit plugin. Please make sure to also import the Unity ARKit Plugin from the Asset Store. This method is no longer supported and I recommend using ARFoundation instead.");
#elif USES_AR_CORE
		Debug.Log("This build is set up to run with ARCore. Please make sure to follow the instructions here : https://developers.google.com/ar/develop/unity/quickstart to get your environment set up for ARCore. This method is no longer supported and I recommend using ARFoundation instead.");
#elif USES_AR_FOUNDATION
		Debug.Log("This build is set up to function with ARFoundation, the recommended method for ARKit and ARCore support.");
#else
        Debug.Log("This build is set up to run without VR or ARKit. To enable VR / AR support please edit your settings in Window -> Portal State Manager.");
#endif

#if USES_OPEN_VR
        Shader.SetGlobalInt("OpenVRRender", 1);
#else
        Shader.SetGlobalInt("OpenVRRender", 0);
#endif
    }

    // Use this for initialization
    void Start () {
		meshRenderer = GetComponent<Renderer> ();
		meshFilter = GetComponent<MeshFilter> ();
		meshDeformer = GetComponent<MeshDeformer> ();

#if USES_OPEN_VR
        OVRCameraRig rig = GameObject.FindObjectOfType<OVRCameraRig>();
        Assert.IsNotNull(rig, "To use Open VR Portal mode you need to have an OVRCameraRig in your scene.");
        this.mainCamera = rig.leftEyeCamera;
        this.rightCamera = rig.rightEyeCamera;
#else
        this.mainCamera = Camera.main;
#endif
        Assert.IsNotNull(this.mainCamera, "Pocket Portal could not find a main camera in your scene.");

        this.gameObject.layer = FromDimension ().layer;

		dimension1.connectedPortals.Add (this);
		dimension2.connectedPortals.Add (this);

        dimension1.showChildrenWithTag(this.ignoreRigidbodyTag);
        dimension2.showChildrenWithTag(this.ignoreRigidbodyTag);

		minimumDeformRangeSquared = maximumDeformRange * maximumDeformRange;

		Vector3 convertedPoint = transform.InverseTransformPoint (mainCamera.transform.position);
		triggerZDirection = (convertedPoint.z > 0);

        if (!mainCamera.GetComponent<MainCameraLayerManager> ()) {
			mainCamera.gameObject.AddComponent<MainCameraLayerManager> ();  // this allows us to alter layers before / after a render!
		}
	}

	private void OnDestroy()
	{
		if (renderCam != null)
		{
			Destroy(renderCam.gameObject);
			renderCam = null;
		}

		if (leftTexture != null) {
			Destroy (leftTexture);
		}
#if USES_STEAM_VR || USES_OPEN_VR
		if (rightTexture != null) {
			Destroy (rightTexture);
		}
#endif
	}

	/// <summary>
	///  Call this method to instantly switch between dimensions. This will switch the Main Character (IE: the main camera) as well.
	/// </summary>
	public void SwitchDimensions ()
	{
		DimensionChanger.SwitchCameraRender (this.mainCamera, FromDimension ().layer, ToDimension ().layer, ToDimension ().customSkybox);
		DimensionChanger.SwitchDimensions (this.mainCamera.gameObject, FromDimension (), ToDimension ());

        if (this.rightCamera != null) {
            DimensionChanger.SwitchCameraRender(this.rightCamera, FromDimension().layer, ToDimension().layer, ToDimension().customSkybox);
            DimensionChanger.SwitchDimensions(this.rightCamera.gameObject, FromDimension(), ToDimension());
        }

        ToDimension ().SwitchConnectingPortals ();
		if (dimensionSwitched)
		{
			line.LimboBack();
		}
		else
		{
			line.LimboGone();
		}
	}

	// ---------------------------------
	// Rendering and Display
	// ---------------------------------

	void OnWillRenderObject ()
	{
		if (Camera.current == null)
			return;  // Prevent null reference

		// Create the textures and camera if they don't exist.
		if (!leftTexture)
		{
			Vector2 texSize = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);
			leftTexture = new RenderTexture((int)(texSize.x * renderQuality), (int)(texSize.y * renderQuality), 16);
			Debug.Log("RenderTexture created: " + leftTexture.width + "x" + leftTexture.height);
#if USES_STEAM_VR || USES_OPEN_VR
			rightTexture = new RenderTexture ((int)(texSize.x * renderQuality), (int)(texSize.y * renderQuality), 16);
#endif
			renderCam = new GameObject(gameObject.name + " render camera", typeof(Camera), typeof(Skybox)).GetComponent<Camera>();
			renderCam.cameraType = CameraType.Game;
			renderCam.forceIntoRenderTexture = true;
			renderCam.transform.position = mainCamera.transform.position;
			renderCam.transform.rotation = mainCamera.transform.rotation;

			SetupRenderCameraForAR();  // this will get the camera ready to render for ARKit or ARCore

			renderCam.name = gameObject.name + " render camera";
			renderCam.tag = "Untagged";
			//renderCam.transform.position = new Vector3(2.9f, 8.21f, 1.52f);
			renderCam.transform.rotation = Quaternion.identity; // Or set to something more meaningful
			if (renderCam.GetComponent<Skybox>())
			{
				camSkybox = renderCam.GetComponent<Skybox>();
			}
			else
			{
				renderCam.gameObject.AddComponent<Skybox>();
				camSkybox = renderCam.GetComponent<Skybox>();
			}

			CameraExtensions.ClearCameraComponents(renderCam.GetComponent<Camera>());

			renderCam.hideFlags = HideFlags.HideInHierarchy;
			renderCam.enabled = false;

			//StartCoroutine(DelayedRender());
		}

		if (ToDimension().customSkybox)
		{
			camSkybox.material = ToDimension().customSkybox;
		}

		meshRenderer.material.SetFloat("_RecursiveRender", (gameObject.layer != Camera.current.gameObject.layer) ? 1 : 0);
		RenderPortal(Camera.current);

		
		
	}

	private void SetupRenderCameraForAR() {
#if USES_AR_KIT
		if (mainCamera.GetComponent<UnityARVideo> ()) {
		renderCam.clearFlags = CameraClearFlags.SolidColor;
		ARKitCameraRender component = renderCam.gameObject.AddComponent<ARKitCameraRender> ();
		component.m_ClearMaterial = mainCamera.GetComponent<UnityARVideo> ().m_ClearMaterial;
		}
#endif


#if USES_AR_CORE
		if (mainCamera.GetComponent<GoogleARCore.ARCoreBackgroundRenderer> ()) {
			renderCam.clearFlags = CameraClearFlags.SolidColor;
			GoogleARCore.ARCoreBackgroundRenderer component = renderCam.gameObject.AddComponent<GoogleARCore.ARCoreBackgroundRenderer> ();
			component.BackgroundMaterial = mainCamera.GetComponent<GoogleARCore.ARCoreBackgroundRenderer> ().BackgroundMaterial;

			// This sucks, the first enabling fails automatically because there isn't a background material.  By doing this we still
			// get an error on the log, but it at least works.. :/
			component.enabled = false;
			component.enabled = true;
		}
#endif

#if USES_AR_FOUNDATION
        if (mainCamera.GetComponent<ARCameraBackground>()) {
            ARCameraBackground component = renderCam.gameObject.AddComponent<ARCameraBackground>();
        }
#endif
	}

	private void RenderPortal (Camera camera)
	{
#if UNITY_EDITOR
		if (camera.name == "SceneCamera")
			return;
#endif

		if (!this.enableObliqueProjection) {
			Vector3 deltaTransform = transform.position - camera.transform.position;
			renderCam.nearClipPlane = Mathf.Max (deltaTransform.magnitude - meshRenderer.bounds.size.magnitude, 0.01f);
		}
			
#if USES_STEAM_VR || USES_OPEN_VR
		if (camera.stereoEnabled) {  // IE: If we're in VR

/* Open VR Special */
#if USES_STEAM_VR
			this.RenderSteamVR (camera);
#endif

/* Gear VR Special */
#if USES_OPEN_VR
			this.RenderOpenVR (camera);
#endif
        }
        else {  // We're rendering in mono regardless
			this.RenderMono (camera);
		}
#else
	//this.RenderMono (camera);   // We force mono in things like ARKit & Hololens
#endif

	}

	private void RenderSteamVR(Camera camera) {
#if USES_STEAM_VR
		if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Left) {
			Vector3 eyePos = camera.transform.TransformPoint (SteamVR.instance.eyes [0].pos);
			Quaternion eyeRot = camera.transform.rotation * SteamVR.instance.eyes [0].rot;
			Matrix4x4 projectionMatrix = GetSteamVRProjectionMatrix (camera, Valve.VR.EVREye.Eye_Left);
			RenderTexture target = leftTexture;

			RenderPlane (renderCam, target, eyePos, eyeRot, projectionMatrix);
			meshRenderer.material.SetTexture ("_LeftTex", target);
		}

		if (camera.stereoTargetEye == StereoTargetEyeMask.Both || camera.stereoTargetEye == StereoTargetEyeMask.Right) {
			Vector3 eyePos = camera.transform.TransformPoint (SteamVR.instance.eyes [1].pos);
			Quaternion eyeRot = camera.transform.rotation * SteamVR.instance.eyes [1].rot;
			Matrix4x4 projectionMatrix = GetSteamVRProjectionMatrix (camera, Valve.VR.EVREye.Eye_Right);
			RenderTexture target = rightTexture;

			RenderPlane (renderCam, target, eyePos, eyeRot, projectionMatrix);
			meshRenderer.material.SetTexture ("_RightTex", target);
		}
#endif
	}

	private void RenderOpenVR(Camera camera) {
#if USES_OPEN_VR
        Transform trackingSpace = GameObject.FindObjectOfType<OVRCameraRig>().trackingSpace;

        Vector3 leftPosition = trackingSpace.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.LeftEye));
        Vector3 rightPosition = trackingSpace.TransformPoint(UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightEye));

        Quaternion leftRot = trackingSpace.transform.rotation * UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.LeftEye);
        Quaternion rightRot = trackingSpace.transform.rotation * UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.RightEye);
        
        RenderPlane(renderCam, leftTexture, leftPosition, leftRot, camera.projectionMatrix);
		meshRenderer.material.SetTexture("_LeftTex", leftTexture);

		RenderPlane(renderCam, rightTexture, rightPosition, rightRot, camera.projectionMatrix);
		meshRenderer.material.SetTexture("_RightTex", rightTexture);
#endif
	}

	//private void RenderMono(Camera camera) {

	//	//Vector3 camPos = mainCamera.transform.position;
	//	//Quaternion camRot = mainCamera.transform.rotation;
	//	// Transform the main camera position & rotation relative to the portal
	//	Transform from = transform;
	//	Transform to = transform; // you can later replace with actual portal transform if you have an exit portal

	//	// Mirror the main camera's position across the portal
	//	Matrix4x4 m = Matrix4x4.TRS(from.position, from.rotation, Vector3.one);
	//	Matrix4x4 inverse = m.inverse;
	//	Matrix4x4 relative = inverse * Matrix4x4.TRS(mainCamera.transform.position, mainCamera.transform.rotation, Vector3.one);
	//	Matrix4x4 finalMatrix = Matrix4x4.TRS(to.position, to.rotation, Vector3.one) * relative;

	//	Vector3 camPos = finalMatrix.GetColumn(3);
	//	Quaternion camRot = finalMatrix.rotation;

	//	RenderTexture target = leftTexture;
	//	RenderPlane(renderCam, target, camPos, camRot, camera.projectionMatrix);

	//	Material portalMaterial = meshRenderer.material;
	//	portalMaterial.SetTexture("_LeftTex", target);
	//	portalMaterial.SetFloat("_RecursiveRender", 1);

	//	Debug.Log("RenderTexture set on material: " + portalMaterial.name);
	//}

	private void RenderPortalViewFromMainCamera()
	{
		// Calculate the relative transform from main camera to portal
		Transform main = mainCamera.transform;
		Transform portal = transform;

		// Mirror camera position and rotation across the portal plane
		Vector3 relativePos = portal.InverseTransformPoint(main.position);
		relativePos = new Vector3(-relativePos.x, relativePos.y, -relativePos.z);
		Vector3 newWorldPos = portal.TransformPoint(relativePos);

		Vector3 forward = portal.InverseTransformDirection(main.forward);
		forward = new Vector3(-forward.x, forward.y, -forward.z);
		Vector3 newWorldForward = portal.TransformDirection(forward);
		Quaternion newWorldRot = Quaternion.LookRotation(newWorldForward, portal.up);

		// Apply to renderCam
		renderCam.transform.SetPositionAndRotation(newWorldPos, newWorldRot);

		// Actually render
		RenderTexture target = leftTexture;
		RenderPlane(renderCam, target, newWorldPos, newWorldRot, mainCamera.projectionMatrix);

		// Set texture to portal material
		Material portalMaterial = meshRenderer.material;
		portalMaterial.SetTexture("_LeftTex", target);
		portalMaterial.SetFloat("_RecursiveRender", 1);
	}

	protected void RenderPlane(Camera portalCamera, RenderTexture targetTexture, Vector3 camPosition, Quaternion camRotation, Matrix4x4 camProjectionMatrix)
	{
		Debug.Log($"RenderCamera Pos: {camPosition}, Rot: {camRotation.eulerAngles}, Projection: {camProjectionMatrix}");
		Debug.Log($"ToDimension layer: {ToDimension().layer}, CullingMask: {portalCamera.cullingMask}");
		// Set camera position and rotation
		portalCamera.transform.position = camPosition;
		portalCamera.transform.rotation = camRotation;

		// Set the target texture
		portalCamera.targetTexture = targetTexture;
		portalCamera.ResetWorldToCameraMatrix();

		// Set projection matrix (optionally oblique)
		Vector3 pos = transform.position;
		Vector3 normal = transform.forward;
		bool isForward = transform.InverseTransformPoint(portalCamera.transform.position).z < 0;

		Vector4 clipPlane = CameraSpacePlane(portalCamera, pos, normal, isForward ? 1.0f : -1.0f);
		Matrix4x4 projection = camProjectionMatrix;

		if (enableObliqueProjection)
		{
			CalculateObliqueMatrix(ref projection, clipPlane);
		}

		portalCamera.projectionMatrix = projection;

		// Culling settings
		portalCamera.enabled = false;
		portalCamera.cullingMask = 0;

		CameraExtensions.LayerCullingShow(portalCamera, ToDimension().layer);
		CameraExtensions.LayerCullingShowMask(portalCamera, alwaysVisibleMask);

		// Match the main camera's rendering settings
		portalCamera.farClipPlane = mainCamera.farClipPlane;
		portalCamera.nearClipPlane = mainCamera.nearClipPlane;
		portalCamera.orthographic = mainCamera.orthographic;
		portalCamera.fieldOfView = mainCamera.fieldOfView;
		portalCamera.aspect = mainCamera.aspect;
		portalCamera.orthographicSize = mainCamera.orthographicSize;

		portalCamera.ResetProjectionMatrix();
		portalCamera.ResetWorldToCameraMatrix();
		portalCamera.enabled = true;
		// Render the view
		try
		{
			portalCamera.Render();
		}
		catch (System.Exception e)
		{
			Debug.LogWarning("RenderCamera failed: " + e.Message);
		}

		// Reset the target texture
		portalCamera.targetTexture = null;
		CameraExtensions.LayerCullingShow(portalCamera, ToDimension().layer);
		int portalLayer = gameObject.layer;
		portalCamera.cullingMask = ((1 << ToDimension().layer) | alwaysVisibleMask) & ~(1 << portalLayer);
	}

	// Creates a clip plane for the projection matrix that clips to the portal.
	private static void CalculateObliqueMatrix (ref Matrix4x4 projection, Vector4 clipPlane) {
		Vector4 q = projection.inverse * new Vector4(
			sgn(clipPlane.x),
			sgn(clipPlane.y),
			1.0f,
			1.0f
		);
		Vector4 c = clipPlane * (2.0F / (Vector4.Dot (clipPlane, q)));

		// third row = clip plane - fourth row
		projection[2] = c.x - projection[3];
		projection[6] = c.y - projection[7];
		projection[10] = c.z - projection[11];
		projection[14] = c.w - projection[15];
	}

	// Given position/normal of the plane, calculates plane in camera space.
	private Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign) {
        Vector3 offsetPos = pos + normal * portalSwitchDistance * (triggerZDirection ? -1 : 1);
		Matrix4x4 m = cam.worldToCameraMatrix;
		Vector3 cpos = m.MultiplyPoint( offsetPos );
		Vector3 cnormal = m.MultiplyVector( normal ).normalized * sideSign;
		return new Vector4( cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos,cnormal) );
	}

	// Extended sign: returns -1, 0 or 1 based on sign of a
	private static float sgn(float a) {
		if (a > 0.0f) return 1.0f;
		if (a < 0.0f) return -1.0f;
		return 0.0f;
	}

#if USES_STEAM_VR
	public static Matrix4x4 GetSteamVRProjectionMatrix (Camera cam, Valve.VR.EVREye eye)
	{
	Valve.VR.HmdMatrix44_t proj = SteamVR.instance.hmd.GetProjectionMatrix (eye, cam.nearClipPlane, cam.farClipPlane);
	Matrix4x4 m = new Matrix4x4 ();
	m.m00 = proj.m0;
	m.m01 = proj.m1;
	m.m02 = proj.m2;
	m.m03 = proj.m3;
	m.m10 = proj.m4;
	m.m11 = proj.m5;
	m.m12 = proj.m6;
	m.m13 = proj.m7;
	m.m20 = proj.m8;
	m.m21 = proj.m9;
	m.m22 = proj.m10;
	m.m23 = proj.m11;
	m.m30 = proj.m12;
	m.m31 = proj.m13;
	m.m32 = proj.m14;
	m.m33 = proj.m15;
	return m;
	}
#endif

	// ---------------------------------
	// Portal Dimension Switching and Deformation
	// ---------------------------------

	void Update ()
	{
		if (mainCamera.gameObject.layer != this.FromDimension ().layer) {
			return;  // don't transition if we are in different worlds.
		}

		Vector3 portalSize = meshFilter.mesh.bounds.size;
		bool shouldDeform = 
			(Mathf.Pow (transform.InverseTransformDirection (mainCamera.transform.position - this.transform.position).z, 2) <= minimumDeformRangeSquared) && // z direction is close
			Mathf.Abs (transform.InverseTransformDirection (mainCamera.transform.position - this.transform.position).x) <= (portalSize.x * transform.lossyScale.x) / 2f &&
			Mathf.Abs (transform.InverseTransformDirection (mainCamera.transform.position - this.transform.position).y) <= (portalSize.y * transform.lossyScale.y) / 2f;

		if (shouldDeform) {
			DeformPortalWithTransform (mainCamera.transform);
		} else if (isDeforming) {
			isDeforming = false;
			meshDeformer.ClearDeformingForce ();
		}

        CheckForTransitionObjects();
    }

	private void DeformPortalWithTransform (Transform otherTransform)
	{
		Vector3 convertedPoint = transform.InverseTransformPoint(otherTransform.position);

		if ((convertedPoint.z > 0) != triggerZDirection && Mathf.Abs(convertedPoint.z) > portalSwitchDistance) {
			triggerZDirection = (convertedPoint.z > 0);
			if (isDeforming) {  // if we're not deforming before this, the user could have walked AROUND the portal.
				SwitchDimensions ();
			}
		}

		meshDeformer.AddDeformingForce (otherTransform.position, deformPower, triggerZDirection);
		isDeforming = true;
	}

	public void SwitchPortalDimensions ()
	{
		dimensionSwitched = !dimensionSwitched;
		gameObject.layer = FromDimension ().layer;
	}

    // -----------------------------------------------
    // Moving other objects through the portal
    // -----------------------------------------------

    void CheckForTransitionObjects() {
        Vector3 portalSize = meshFilter.mesh.bounds.size;
        PortalTransitionObject[] objects = FindObjectsOfType<PortalTransitionObject>();
        foreach(PortalTransitionObject obj in objects) {
            bool shouldDeform =
                (Mathf.Pow(transform.InverseTransformDirection(obj.transform.position - this.transform.position).z, 2) <= minimumDeformRangeSquared) && // z direction is close
                Mathf.Abs(transform.InverseTransformDirection(obj.transform.position - this.transform.position).x) <= (portalSize.x * transform.lossyScale.x) / 2f &&
                Mathf.Abs(transform.InverseTransformDirection(obj.transform.position - this.transform.position).y) <= (portalSize.y * transform.lossyScale.y) / 2f;

            if (shouldDeform) {
                HandleTransition(obj);
            } else {
                if (this.transitionObjects.Contains(obj)) {
                    this.transitionObjects.Remove(obj);
                    if (this.transitionObjects.Count == 0 && !isDeforming) {
                        meshDeformer.ClearDeformingForce();
                    }
                }
            }
        }
    }

    void HandleTransition(PortalTransitionObject transitionObject) {
        if (!this.transitionObjects.Contains(transitionObject)) {
            if (CameraExtensions.CameraForObject(transitionObject.gameObject) != mainCamera && (ignoreRigidbodyTag == "" || !transitionObject.gameObject.CompareTag(ignoreRigidbodyTag))) {
                transitionObject.triggerZDirection = (transform.InverseTransformPoint(transitionObject.transform.position).z > 0);
                this.transitionObjects.Add(transitionObject);
            }
        }

        Vector3 convertedPoint = transform.InverseTransformPoint(transitionObject.transform.position);
        if ((convertedPoint.z > 0) != transitionObject.triggerZDirection) {
            if (transitionObject.gameObject.layer == FromDimension().layer) {
                DimensionChanger.SwitchDimensions(transitionObject.gameObject, FromDimension(), ToDimension());
            } else {
                DimensionChanger.SwitchDimensions(transitionObject.gameObject, ToDimension(), FromDimension());
            }
            transitionObject.triggerZDirection = !transitionObject.triggerZDirection;
        }

        if (!isDeforming) { // Only deform if the main camera isn't deforming.
            Vector3 transformPosition = transitionObject.transform.position;
            if (Mathf.Abs(convertedPoint.z) < maximumDeformRange) {
                convertedPoint.z += triggerZDirection ? maximumDeformRange : -maximumDeformRange;
                transformPosition = transform.TransformPoint(convertedPoint);
            }
            meshDeformer.AddDeformingForce(transformPosition, deformPower);
        }
    }

	/* Convenience */
	public Dimension ToDimension ()
	{
		if (dimensionSwitched) {
			return dimension1;

		}
		else {
			return dimension2;
		}
	}

	public Dimension FromDimension ()
	{
		if (dimensionSwitched) {
			return dimension2;
		} else {
			return dimension1;
		}
	}

	private void SaveRenderTextureToPNG(RenderTexture rt, string filename = "PortalRender")
	{
		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = rt;

		Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
		tex.Apply();

		byte[] bytes = tex.EncodeToPNG();
		string path = Application.dataPath + "/" + filename + ".png";
		System.IO.File.WriteAllBytes(path, bytes);

		Debug.Log("Saved RenderTexture to: " + path);

		RenderTexture.active = currentRT;
		Destroy(tex);
	}
}

using UnityEngine;

public class MasterController : MonoBehaviour
{
	[SerializeField, Range( 0, 500 )]
	private float _MasterSpeed;
	[SerializeField]
	private float _MaxZoom = 20f;
	[SerializeField]
	private float _MinZoom= 5f;
	[SerializeField]
	private float _Distance = 20f;
	[SerializeField]
	private float _Sensitivity = 5;
	[SerializeField]
	private float _Damping = 5f;
	
	private Camera _MyCam;
	private Vector3 _Pos;
	private float _PosY = 0f;
	private void Awake()
	{
		_MyCam = GetComponentInChildren<Camera>();
		_Distance = _MyCam.fieldOfView;
		_Pos = transform.position;
		_PosY = _Pos.y;

	}

	private void Update()
	{
		if(LoadingScreen.IsLoadingScreen == true)
			return;
		
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		_Pos += transform.forward * (vertical * _MasterSpeed * Time.deltaTime );
		_Pos += transform.right* ( horizontal* _MasterSpeed * Time.deltaTime );

		_Pos.x = Mathf.Clamp( _Pos.x , -200.24f / 2f, 457.24f / 2f);
		_Pos.z = Mathf.Clamp( _Pos.z, -457.24f / 2f , 150.24f / 2f );

		SetPos(_Pos);
		Zoom();

	}

	public void SetPos(Vector3 pos)
	{
		_Pos = pos;
		_Pos.y = _PosY;
		transform.position = _Pos;

	}

	private void Zoom()
	{
		_Distance += Input.mouseScrollDelta.y * -_Sensitivity;
		float Distance = Mathf.Clamp(_Distance, _MinZoom, _MaxZoom);
		_MyCam.fieldOfView = Mathf.Lerp(_MyCam.fieldOfView, Distance, Time.deltaTime * _Damping );	
	}
}

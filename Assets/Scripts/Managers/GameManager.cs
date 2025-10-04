using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [field:SerializeField] public UnitModel[] UnitsModels { get; private set; }
    [SerializeField] private LayerMask tileLayer;

    public static GameManager Instance;
    private Camera _camera;
    private Tile _selectedTile;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        _camera = Camera.main;
    }

    private void Start()
    {
        BuildingContainerUI.onAnyClick += BuildingContainerUI_onAnyClick;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            CheckClickOnTile();
        }
    }

    private void CheckClickOnTile()
    {
        bool hasHit = Physics.Raycast(GetCameraRay(), out RaycastHit hitInfo, float.MaxValue, tileLayer);

        if (!hasHit)
        {
            BuildingUI.Instance.HideUI();
            return;
        }

        if (hitInfo.collider.TryGetComponent<Tile>(out Tile tile))
        {
            _selectedTile = tile;
            BuildingUI.Instance.ShowUI();
        }
    }

    private Ray GetCameraRay()
    {
        return _camera.ScreenPointToRay(Input.mousePosition);
    }

    private void BuildingContainerUI_onAnyClick(ResourceProducer producer)
    {
        if (_selectedTile == null) { return; }

        if (_selectedTile.IsEmpty)
        {
            _selectedTile.SpawnBuilding(producer);
        }
    }

}

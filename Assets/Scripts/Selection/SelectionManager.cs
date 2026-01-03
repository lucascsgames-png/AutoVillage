using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    public static event Action<Unit> OnSelectionChanged;

    [SerializeField] private InputActionReference onClick;
    [SerializeField] private InputActionReference onMousePosition;

    private Unit selected;

    private Camera currentCamera;
    private Vector2 mousePosition;

    private void Start()
    {
        currentCamera = Camera.main;

        onClick.action.performed += OnClick;
        onMousePosition.action.performed += OnMousePosition;
    }


    private void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private async void OnClick(InputAction.CallbackContext context)
    {
        await Task.Delay(1);
        if (EventSystem.current.IsPointerOverGameObject()) return;

        selected = null;
        OnSelectionChanged?.Invoke(null);

        Ray ray = currentCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.transform.TryGetComponent(out Unit unit))
            {
                selected = unit;
                OnSelectionChanged?.Invoke(selected);
            }
        }
    }


}

using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteractionComponent : MonoBehaviour
{
    private PlayerInput _playerInput;
    // ... другие поля

    private void Awake() {
        _playerInput = GetComponent<PlayerInput>();
        // Инициализация ссылок, поиск компонентов и пр.
    }

    private void OnEnable() {
        // Привязка событий ввода к методам
        _playerInput.actions["PickUpLeft"].performed += ctx => OnPickUpLeft();
        _playerInput.actions["PickUpRight"].performed += ctx => OnPickUpRight();
        _playerInput.actions["ThrowItem"].performed += ctx => OnThrowStart();
        _playerInput.actions["ThrowItem"].canceled += ctx => OnThrowRelease();
        _playerInput.actions["PlaceItem"].performed += ctx => OnPlaceItem();
        _playerInput.actions["SwitchHands"].performed += ctx => OnSwitchHands();
    }

    private void OnDisable() {
        // Отписка при выключении, чтобы не было утечек событий
        _playerInput.actions["PickUpLeft"].performed -= ctx => OnPickUpLeft();
        // ... аналогично для остальных
    }

    // Ниже будут методы OnPickUpLeft, OnThrowStart, OnThrowRelease и т.д.
}

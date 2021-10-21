// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/player/gameActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""gameActions"",
    ""maps"": [
        {
            ""name"": ""playerActions"",
            ""id"": ""2fd9b14b-8292-477c-9e2c-b63d00479eff"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8153c226-630c-4c96-81b8-42a9fd76d694"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseXMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""95d0ed14-c198-4002-b748-5199163b75f9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseYMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""267961e2-ccf5-4b6b-89d4-b37068df2d6e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""02781b52-0478-428e-bb13-21b32a92ea86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""2e87ca3c-03aa-49db-9258-9ee920dccc99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""cb0e0afe-f5a5-47c4-87d3-d5fd4258cdc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""7ed2722b-79be-44a2-99a3-e67394e49e0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""e44db02e-8e9f-4add-ad8e-390e5a2c74dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadXMove"",
                    ""type"": ""Value"",
                    ""id"": ""6ee683a9-6d00-4a16-aae4-263286382b26"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""AxisDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GamepadYMove"",
                    ""type"": ""Value"",
                    ""id"": ""0d317346-5bec-48de-84d3-dca824c9a8ee"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""AxisDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""12e3b070-4fc7-4b9f-9648-e2130b3b5900"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""AxisDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""75998de7-8869-47f0-b942-a70a5b8f7f96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""AxisDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""82478836-c310-4e2c-8e59-75eed63960a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""AxisDeadzone"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSD"",
                    ""id"": ""9c535dd3-8e1b-48e7-af94-660d859d070d"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f1ab6844-f600-4865-abaf-d6cd17e10b79"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ad4b245d-e56c-4056-8064-81ff48f94bed"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0b41734e-23d7-4bef-962b-6b54df9c2a1e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8b246cef-1fdf-430f-838c-5e2cbf1456d7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a61cf15d-62d3-45ea-bf63-f4e530377ff5"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cbfd576e-26b1-41a0-875e-ba6acea13207"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""11060391-b399-4ed8-9d57-83388dcaf83f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2766f952-d1ed-4859-9eaf-72b34f25b58c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""88139607-9642-4df8-91d9-08f4f87ff08b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2d207397-e93f-4d48-bb87-9e343e34998e"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseXMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ecb2ed9-d8f1-40ee-8016-14cbd34025c5"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MouseYMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcbf69b6-9ca6-4188-bfe3-fec0f8029a44"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45653613-c91c-4f8e-8899-2d676ba3f6fc"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e71cd596-2cf5-4c99-8e06-4af3a05c0932"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7045544-67ad-457a-81ea-dd002c8b6575"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0330fee6-897c-4680-9537-8625ef2bc48c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ff6f2eb-fe78-49f2-a3e0-db0a75ccf089"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7f3b233-a5f2-4f15-9637-7505b133c1ea"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c207b0c1-8855-4600-9897-e87ac665c0f4"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ceea38e-fac1-4a4f-a67d-e6e55bca4812"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f2ade6f-cfd9-48fb-848b-a873358f2a1b"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48d4986e-b318-44fe-b46b-2ebda7ad6c54"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadXMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed2104c2-72f7-494a-902d-1b371e27c71c"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""GamepadYMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d8a3320-5725-4ea1-829e-5e52e41149d3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edef284b-9de6-479e-9616-5481b8ca05de"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d261394-456b-4e39-ad6e-1c52edfb647e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f880eb5e-f17a-41de-bfb8-b98539c8f944"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be1a4498-598f-42e2-99cf-058cb9d83ad6"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60007320-e1bc-4749-bf1c-ed0da331f54f"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a48af6e4-b5aa-4b77-b24f-bf2f78cafe39"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4800de80-4a42-4df6-b5f3-ed94a999f70e"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e327f683-ebb5-4e36-9ad0-aae94970e024"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eed3d3f3-cb10-4da3-8ae0-205a1ffac5b2"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // playerActions
        m_playerActions = asset.FindActionMap("playerActions", throwIfNotFound: true);
        m_playerActions_Move = m_playerActions.FindAction("Move", throwIfNotFound: true);
        m_playerActions_MouseXMove = m_playerActions.FindAction("MouseXMove", throwIfNotFound: true);
        m_playerActions_MouseYMove = m_playerActions.FindAction("MouseYMove", throwIfNotFound: true);
        m_playerActions_Jump = m_playerActions.FindAction("Jump", throwIfNotFound: true);
        m_playerActions_Shoot = m_playerActions.FindAction("Shoot", throwIfNotFound: true);
        m_playerActions_Reload = m_playerActions.FindAction("Reload", throwIfNotFound: true);
        m_playerActions_Run = m_playerActions.FindAction("Run", throwIfNotFound: true);
        m_playerActions_Crouch = m_playerActions.FindAction("Crouch", throwIfNotFound: true);
        m_playerActions_GamepadXMove = m_playerActions.FindAction("GamepadXMove", throwIfNotFound: true);
        m_playerActions_GamepadYMove = m_playerActions.FindAction("GamepadYMove", throwIfNotFound: true);
        m_playerActions_Action = m_playerActions.FindAction("Action", throwIfNotFound: true);
        m_playerActions_Aim = m_playerActions.FindAction("Aim", throwIfNotFound: true);
        m_playerActions_ChangeWeapon = m_playerActions.FindAction("ChangeWeapon", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // playerActions
    private readonly InputActionMap m_playerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_playerActions_Move;
    private readonly InputAction m_playerActions_MouseXMove;
    private readonly InputAction m_playerActions_MouseYMove;
    private readonly InputAction m_playerActions_Jump;
    private readonly InputAction m_playerActions_Shoot;
    private readonly InputAction m_playerActions_Reload;
    private readonly InputAction m_playerActions_Run;
    private readonly InputAction m_playerActions_Crouch;
    private readonly InputAction m_playerActions_GamepadXMove;
    private readonly InputAction m_playerActions_GamepadYMove;
    private readonly InputAction m_playerActions_Action;
    private readonly InputAction m_playerActions_Aim;
    private readonly InputAction m_playerActions_ChangeWeapon;
    public struct PlayerActionsActions
    {
        private @GameActions m_Wrapper;
        public PlayerActionsActions(@GameActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_playerActions_Move;
        public InputAction @MouseXMove => m_Wrapper.m_playerActions_MouseXMove;
        public InputAction @MouseYMove => m_Wrapper.m_playerActions_MouseYMove;
        public InputAction @Jump => m_Wrapper.m_playerActions_Jump;
        public InputAction @Shoot => m_Wrapper.m_playerActions_Shoot;
        public InputAction @Reload => m_Wrapper.m_playerActions_Reload;
        public InputAction @Run => m_Wrapper.m_playerActions_Run;
        public InputAction @Crouch => m_Wrapper.m_playerActions_Crouch;
        public InputAction @GamepadXMove => m_Wrapper.m_playerActions_GamepadXMove;
        public InputAction @GamepadYMove => m_Wrapper.m_playerActions_GamepadYMove;
        public InputAction @Action => m_Wrapper.m_playerActions_Action;
        public InputAction @Aim => m_Wrapper.m_playerActions_Aim;
        public InputAction @ChangeWeapon => m_Wrapper.m_playerActions_ChangeWeapon;
        public InputActionMap Get() { return m_Wrapper.m_playerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMove;
                @MouseXMove.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseXMove;
                @MouseXMove.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseXMove;
                @MouseXMove.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseXMove;
                @MouseYMove.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseYMove;
                @MouseYMove.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseYMove;
                @MouseYMove.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseYMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Shoot.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnShoot;
                @Reload.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReload;
                @Run.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnRun;
                @Crouch.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCrouch;
                @GamepadXMove.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadXMove;
                @GamepadXMove.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadXMove;
                @GamepadXMove.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadXMove;
                @GamepadYMove.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadYMove;
                @GamepadYMove.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadYMove;
                @GamepadYMove.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnGamepadYMove;
                @Action.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAction;
                @Aim.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                @ChangeWeapon.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnChangeWeapon;
                @ChangeWeapon.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnChangeWeapon;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MouseXMove.started += instance.OnMouseXMove;
                @MouseXMove.performed += instance.OnMouseXMove;
                @MouseXMove.canceled += instance.OnMouseXMove;
                @MouseYMove.started += instance.OnMouseYMove;
                @MouseYMove.performed += instance.OnMouseYMove;
                @MouseYMove.canceled += instance.OnMouseYMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @GamepadXMove.started += instance.OnGamepadXMove;
                @GamepadXMove.performed += instance.OnGamepadXMove;
                @GamepadXMove.canceled += instance.OnGamepadXMove;
                @GamepadYMove.started += instance.OnGamepadYMove;
                @GamepadYMove.performed += instance.OnGamepadYMove;
                @GamepadYMove.canceled += instance.OnGamepadYMove;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @ChangeWeapon.started += instance.OnChangeWeapon;
                @ChangeWeapon.performed += instance.OnChangeWeapon;
                @ChangeWeapon.canceled += instance.OnChangeWeapon;
            }
        }
    }
    public PlayerActionsActions @playerActions => new PlayerActionsActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActionsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMouseXMove(InputAction.CallbackContext context);
        void OnMouseYMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnGamepadXMove(InputAction.CallbackContext context);
        void OnGamepadYMove(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnChangeWeapon(InputAction.CallbackContext context);
    }
}

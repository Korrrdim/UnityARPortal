using UnityEngine.XR.ARFoundation;

public static class ServiceLocator
{
    public static ARRaycastManager RaycastManager { get; private set; }
    public static ARPlaneManager PlaneManager { get; private set; }

    public static RenderLayersConfig RenderLayersConfig { get; private set; }
    public static ResourcesConfig ResourcesConfig { get; private set; }
    public static IResourceService ResourceService { get; private set; }

    public static User User { get; private set; }
    public static IUserController UserController { get; private set; }

    public static IPortalController PortalController { get; private set; }
    public static IPortalPlacementController PortalPlacementController { get; private set; }

    public static IStateMachine StateMachine { get; private set; }


    public static void Initialize(ARRaycastManager raycastManager, ARPlaneManager planeManager, ResourcesConfig resourcesConfig, User user, RenderLayersConfig renderLayersConfig)
    {
        InitializeARManagers(raycastManager, planeManager);
        InitializeResourceService(resourcesConfig, renderLayersConfig);
        InitializeUser(user);
        InitializePortal();

        InitializeStateMachine();
    }

    private static void InitializeARManagers(ARRaycastManager raycastManager, ARPlaneManager planeManager)
    {
        RaycastManager = raycastManager;
        PlaneManager = planeManager;
    }

    private static void InitializeResourceService(ResourcesConfig resourcesConfig, RenderLayersConfig renderLayersConfig)
    {
        RenderLayersConfig = renderLayersConfig;
        ResourcesConfig = resourcesConfig;
        ResourceService = new ResourceService(ResourcesConfig);
    }

    private static void InitializeUser(User user)
    {
        User = user;
        UserController = new UserController();
    }

    private static void InitializePortal()
    {
        PortalPlacementController = new PortalPlacementController();
        PortalController = new PortalController();
    }

    private static void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        StateMachine.RegisterState<InitialState>(new InitialState(StateMachine));
        StateMachine.RegisterState<GameState>(new GameState(StateMachine));
        StateMachine.SetState<InitialState>();
    }
}

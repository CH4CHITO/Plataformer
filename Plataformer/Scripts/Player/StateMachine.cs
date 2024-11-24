using Godot;

public partial class StateMachine : Node
{
	// Nodo que vamos a controlar
	private Node controlledNode;

	// Estado por defecto
    [Export] 
	public StateBase DefaultState { get; set; }

    // Estado en ejecución en cada momento
    private StateBase currentState = null;

    public override void _Ready()
    {
        controlledNode = GetParent();
        CallDeferred(nameof(StateDefaultStart));
    }

    private void StateDefaultStart()
    {
        currentState = DefaultState;
        StateStart();
    }

    // Método para cambiar a un nuevo estado
    public void ChangeTo(string newState)
    {
        if (currentState != null && currentState.HasMethod("End"))
            currentState.Call("End");

        currentState = GetNode<StateBase>(newState);
        StateStart();
    }

    // Función que prepara las variables para el nuevo estado y lanza su Start
    private void StateStart()
    {
        GD.Print("StateMachine", controlledNode.Name, "start state", currentState.Name);
        currentState.ControlledNode = controlledNode;
        currentState.StateMachine = this;
        currentState.Call("Start");
    }

#region Sobreescribimos los métodos de Godot del nodo

    public override void _Process(double delta)
    {
        if (currentState != null && currentState.HasMethod("OnProcess"))
            currentState.Call("OnProcess", delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (currentState != null && currentState.HasMethod("OnPhysicsProcess"))
            currentState.Call("OnPhysicsProcess", delta);
    }

    public override void _Input(InputEvent @event)
    {
        if (currentState != null && currentState.HasMethod("OnInput"))
            currentState.Call("OnInput", @event);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (currentState != null && currentState.HasMethod("OnUnhandledInput"))
            currentState.Call("OnUnhandledInput", @event);
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (currentState != null && currentState.HasMethod("OnUnhandledKeyInput"))
            currentState.Call("OnUnhandledKeyInput", @event);
    }

#endregion
}
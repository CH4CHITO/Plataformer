using Godot;

public partial class StateBase : Node
{
    // Referencia al nodo que vamos a controlar
    public Node ControlledNode;

    // Referencia a la máquina de estados
    public StateMachine StateMachine;

    #region Métodos comunes

    // Método que se ejecuta al entrar en el estado
    public virtual void Start()
    {
        // Implementar lógica de inicio en clases derivadas si es necesario
    }

    // Método que se ejecuta al abandonar el estado
    public virtual void End()
    {
        // Implementar lógica de finalización en clases derivadas si es necesario
    }

    public virtual void OnPhysicsProcess(double delta) 
    {
         // Implementar lógica de las físicas en clases derivadas si es necesario
    }
}

    #endregion

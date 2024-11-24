using Godot;

public partial class Plataforma : StaticBody2D
{
    public override void _Ready()
    {
        // Llama a la función que espera 3 segundos y luego destruye el objeto
        _ = DestroyAfterDelay();
    }

    private async System.Threading.Tasks.Task DestroyAfterDelay()
    {
        await ToSignal(GetTree().CreateTimer(3.0f), "timeout"); // Espera 3 segundos
        QueueFree(); // Destruye el objeto
    }
}
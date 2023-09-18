using Godot;

namespace ThronesEra
{
    // TODO implement a proper file logger
    public static class Logger
    {
        public static void INFO(string message)
        {
            GD.PrintRich($"(DEBUG.INFO) {message}");
        }
    }
}
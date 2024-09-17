using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;
namespace REngine
{

    /// <summary>
    /// Basic Input actions
    /// </summary>
    public enum InputAction
    {
        Up,
        Down,
        Left,
        Right,
        Action,
        None
    }

    public interface IUpdateable
    {
        public void Update();
    }
    /// <summary>
    /// Interface to definea controllable object
    /// Returns the next input action based on type of input(keyboard/mouse/AI)
    /// to be processed as approp.
    /// </summary>
    public interface IControllable
    {
        //public Queue<InputAction> ActionQueue { get; set; }
        public InputAction GetNextInput();

    }
    /// <summary>
    /// Keyboard impl. of a controllable 
    /// </summary>
    public class KeyboardControllable : IControllable
    {
        //public Queue<InputAction> ActionQueue { get ; set; }

        public InputAction GetNextInput()
        {
            if (Raylib.IsKeyDown(KeyboardKey.D)) return InputAction.Right;
            if (Raylib.IsKeyDown(KeyboardKey.A)) return InputAction.Left;
            if (Raylib.IsKeyDown(KeyboardKey.W)) return InputAction.Up;
            if (Raylib.IsKeyDown(KeyboardKey.S)) return InputAction.Down;
            return InputAction.None;
        }
    }

    public class MockAIControllable : IControllable
    {
        //public Queue<InputAction> ActionQueue { get ; set; }

        public InputAction GetNextInput()
        {
            throw new NotImplementedException();// TODO: Add mock AI using random directions and actions
        }
    }
}

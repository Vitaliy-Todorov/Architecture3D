using UnityEngine;

namespace Scripts.Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool AttackButtonUp { get; }
    }
}
using MFramework.Services.Common.Abstract;
using MFramework.Services.Common.Enums;
using MFramework.Services.Entities.Abstract;

namespace BlogVue.WebAPI.Entities.Abstract
{
    public abstract class Entity<TKey> : EntityBase<TKey>, IState<State>
    {
        public State State { get; set; } = State.Active;
    }
}

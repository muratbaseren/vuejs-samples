using MFramework.Services.Common.Abstract;
using MFramework.Services.Common.Enums;

namespace BlogVue.WebAPI.Models.Abstract
{
    public abstract class ModelBase<TKey> : IState<State>
    {
        public TKey Id { get; set; }
        public State State { get; set; }
        public string StateString { get; set; }
    }
}

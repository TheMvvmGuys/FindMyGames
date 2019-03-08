using System;
using System.Dynamic;

namespace TheMvvmGuys.FindMyGames.UI.Utilities
{
    public class NameProvider : DynamicObject
    {
        private readonly Func<string, object> _getter;
        public string Prefix { get; set; }
        public NameProvider(Func<string, object> getter, string prefix = "PART_")
        {
            _getter = getter;
            Prefix = prefix;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var parameter = GetName(binder.Name);
            result = _getter(parameter);
            return true;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = GetName(binder.Name);
            result = _getter?.Invoke(name) ?? GetName(binder.Name);
            return true;
        }

        private string GetName(string name)
        {
            return $"{Prefix}{name}";
        }
    }
}
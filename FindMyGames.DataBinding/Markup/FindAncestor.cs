using System;
using System.Windows.Data;

namespace TheMvvmGuys.FindMyGames.DataBinding.Markup
{
    public class FindAncestor : RelativeSource
    {
        public FindAncestor()
        {
            Mode = RelativeSourceMode.FindAncestor;           
        }
        public FindAncestor(Type ancestorType) : base(RelativeSourceMode.FindAncestor, ancestorType, 0)
        {

        }
    }
}
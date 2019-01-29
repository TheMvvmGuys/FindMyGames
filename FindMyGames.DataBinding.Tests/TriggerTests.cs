using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using TheMvvmGuys.FindMyGames.DataBinding.Tests.Annotations;
using TheMvvmGuys.FindMyGames.DataBinding.Triggers;

namespace TheMvvmGuys.FindMyGames.DataBinding.Tests
{
    // Gotta test everything :p
    [TestClass]
    public class TriggerTests : DependencyObject
    {
        [PublicAPI]
        public bool TestPassTemp { get; set; }

        private static TrueTrigger TrueTrigger = new TrueTrigger();
        [TestMethod]
        public void TrueTrigger_Normal_BindingIsTrue() // duh
        {
            Assert.IsTrue(TrueTrigger.Binding.Equals(TrueTrigger.Value));
        }

        [TestMethod]
        public void TrueTrigger_ActionAttached_IsCalled()
        {
            var trigger = new TrueTrigger();
            trigger.Actions.Add(new ChangePropertyAction
            {
                TargetObject = this,
                PropertyName = nameof(TestPassTemp),
                IsEnabled = true,
                Value = true
            });
            trigger.Attach(this);
            trigger.Binding = true; // force it to update
            Assert.IsTrue(TestPassTemp);
        }
    }
}

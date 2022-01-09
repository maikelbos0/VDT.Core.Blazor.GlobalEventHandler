using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.Wizard.Tests {
    public class WizardTests {
        /*
         * TODO
         * TryCompleteStep
         * BuildRenderTree?
         * ActiveStep: index check, bounds check
         * IsFirstStepActive
         * IsLastStepActive
         */

        [Fact]
        public async Task Wizard_Start_Works() {
            WizardStartedEventArgs? arguments = null;
            var wizard = new Wizard() {
                OnStart = EventCallback.Factory.Create<WizardStartedEventArgs>(this, args => arguments = args)
            };
            
            await wizard.Start();

            Assert.Equal(0, typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task Wizard_Start_Does_Nothing_When_Active() {
            WizardStartedEventArgs? arguments = null;
            var wizard = new Wizard() {
                OnStart = EventCallback.Factory.Create<WizardStartedEventArgs>(this, args => arguments = args)
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 2);

            await wizard.Start();

            Assert.Equal(2, typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.Null(arguments);
        }

        [Fact]
        public async Task Wizard_Stop_Works() {
            WizardStoppedEventArgs? arguments = null;
            var wizard = new Wizard() {
                OnStop = EventCallback.Factory.Create<WizardStoppedEventArgs>(this, args => arguments = args)
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 2);

            await wizard.Stop();

            Assert.Null(typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task Wizard_Stop_Does_Nothing_When_Inactive() {
            WizardStoppedEventArgs? arguments = null;
            var wizard = new Wizard() {
                OnStop = EventCallback.Factory.Create<WizardStoppedEventArgs>(this, args => arguments = args)
            };

            await wizard.Stop();

            Assert.Null(arguments);
        }

        [Fact]
        public async Task Wizard_AddStep_Works() {
            var wizard = new Wizard();
            var step = new WizardStep();

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 0);

            await wizard.AddStep(step);

            Assert.Equal(step, Assert.Single(wizard.GetSteps()));
        }

        [Fact]
        public async Task Wizard_AddStep_Initializes_First_Step() {
            WizardStepInitializedEventArgs? arguments = null;
            var wizard = new Wizard();
            var step = new WizardStep() {
                OnInitialize = EventCallback.Factory.Create<WizardStepInitializedEventArgs>(this, args => arguments = args)
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 0);

            await wizard.AddStep(step);

            Assert.NotNull(arguments);
        }

        [Fact]
        public async Task Wizard_AddStep_Does_Not_Initialize_Subsequent_Steps() {
            WizardStepInitializedEventArgs? arguments = null;
            var wizard = new Wizard();
            var step = new WizardStep() {
                OnInitialize = EventCallback.Factory.Create<WizardStepInitializedEventArgs>(this, args => arguments = args)
            };

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 0);

            await wizard.AddStep(new WizardStep());
            await wizard.AddStep(step);

            Assert.Null(arguments);
        }

        [Fact]
        public async Task Wizard_AddStep_Does_Nothing_For_Existing_Step() {
            var wizard = new Wizard();
            var step = new WizardStep();

            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 0);

            await wizard.AddStep(step);
            await wizard.AddStep(step);

            Assert.Equal(step, Assert.Single(wizard.GetSteps()));
        }

        [Fact]
        public async Task Wizard_GoToPreviousStep_Works() {
            WizardStepInitializedEventArgs? arguments = null; 
            var wizard = new Wizard();
            var step = new WizardStep() {
                OnInitialize = EventCallback.Factory.Create<WizardStepInitializedEventArgs>(this, args => arguments = args)
            };
            var stepsInternal = (List<WizardStep>)typeof(Wizard).GetField("stepsInternal", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard)!;

            stepsInternal.Add(step);
            typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(wizard, 1);

            await wizard.GoToPreviousStep();

            Assert.Equal(0, typeof(Wizard).GetField("activeStepIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(wizard));
            Assert.NotNull(arguments);
        }
    }
}

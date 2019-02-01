using MvvmCross;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.ViewModels;
using TodoList.Core;

namespace TodoList.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            var registry = Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }
        protected override IMvxApplication CreateApp()
        {
            CreatableTypes()
                 .EndingWith("Service")
                 .AsInterfaces()
                 .RegisterAsLazySingleton();
            return base.CreateApp();
        }
    }
}
using Microsoft.Practices.Unity;
using Rapid.Core.Common;
using Rapid.Core.Data;
using Rapid.Core.Logging;
using Rapid.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rapid.Service
{
    public class ServiceLocator
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            // Register your types here
            var assemblies = new Assembly[] 
            {
                typeof(IConfigGateway).Assembly,
                typeof(IEmployeeRepository).Assembly,
                typeof(IEmployeeManager).Assembly
            };

            //TransientLifetimeManager 每次通过Resolve或ResolveAll调用对象的时候都会重新创建一个新的对象
            //ContainerControlledLifetimeManager  单件实例，UnityContainer会维护一个对象实例的强引用，每次调用的时候都会返回同一对象
            //HierarchicalLifetimeManager 分层生命周期管理器,Unity的容器时可以嵌套的,单件实例
            //PerResolveLifetimeManager 循环引用生命周期管理,第一调用的时候会创建一个新的对象，而再次通过循环引用访问到的时候就会返回先前创建的对象实例（单件实例）
            //PerThreadLifetimeManager 每线程生命周期管理器，就是保证每个线程返回同一实例
            //ExternallyControlledLifetimeManager 外部控制生命周期管理器,一般情况每次调用Resolve都会返回同一对象（单件实例）

            Func<Type, LifetimeManager> getLifetimeManager = type =>
            {
                return new ContainerControlledLifetimeManager();
            };

            container.RegisterTypes(AllClasses.FromAssemblies(assemblies),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    getLifetimeManager)
                .RegisterType<IDatabase, Database>(
                        getLifetimeManager(typeof(Database)),
                        new InjectionConstructor(Constants.RAPID_DB_CONNECTION));

            container.RegisterType<IAnimalManager, DogManager>(Constants.AnimalType.DOG);
            container.RegisterType<IAnimalManager, CatManager>(Constants.AnimalType.CAT);
            container.RegisterType<IAnimalManager, OtherAnimalManager>(Constants.AnimalType.OTHER);

            TraceRegistrations(container);
        }

        public static T Resolve<T>()
        {
            return container.Value.Resolve<T>();
        }

        public static T Resolve<T>(string name)
        {
            return container.Value.Resolve<T>(name);
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            return container.Value.ResolveAll<T>();
        }

        internal static void TraceRegistrations(IUnityContainer container)
        {
#if DEBUG
            foreach (var registration in container.Registrations.OrderBy(x => x.RegisteredType.FullName))
            {
                RapidLogger.Trace("Unity Registrations",
                    string.Format("{0} => {1} using {2}",
                        registration.RegisteredType.FullName,
                        registration.MappedToType.FullName,
                        registration.LifetimeManagerType.Name
                    )
                );
            }
#endif
        }
    }

}

using System;
using System.Collections.Generic;

namespace Infrastucture
{
    public sealed class ServiceLocator
    {
        private static ServiceLocator m_instance;
        private readonly Dictionary<Type, object> m_services = new();

        public static void Register<T>(T instance) where T : class
        {
            if (instance == null)
            {
                return;
            }

            m_instance ??= new ServiceLocator();
            m_instance.m_services[typeof(T)] = instance;
        }

        public static T Resolved<T>() where T : class
        {
            if (m_instance == null)
            {
                throw new NullReferenceException("Service locator is null");
            }

            return m_instance.m_services[typeof(T)] as T;
        }

        public static void Clear() =>
            m_instance?.m_services.Clear();
    }
}

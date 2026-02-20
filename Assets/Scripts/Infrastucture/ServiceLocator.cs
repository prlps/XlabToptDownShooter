using System;
using System.Collections.Generic;

namespace Infrastucture
{
    public class ServiceLocator
    {
        private static ServiceLocator m_serviceLocator;
        
        private Dictionary<Type, object> m_services = new();
        
        public static void Register<T>(T instance)
        {
          m_serviceLocator ??= new ServiceLocator();
          m_serviceLocator.m_services.Add(typeof(T), instance);
        }

        public T Resolve<T>()
        {
            if (m_serviceLocator is null)
                throw new NullReferenceException("SL is null");

            return m_serviceLocator.m_services[typeof(T)] as T;
        }
    }
}
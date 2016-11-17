﻿namespace NewCRM.Domain.Factory.Domain.ConcreteFactory
{
    public sealed class DefaultDomainFactory : DomainFactory
    {
        /// <summary>
        /// 创建并返回一个领域对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T Create<T>()
        {
            return new T();
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using Anatoli.Business.Proxy.Interfaces;
using Anatoli.DataAccess.Models;
using Anatoli.ViewModels;
using Anatoli.DataAccess.Models.Identity;

namespace Anatoli.Business.Proxy
{
    public abstract class AnatoliProxy<TSource, TOut> : IAnatoliProxy<TSource, TOut>
        where TSource : BaseModel, new()
        where TOut : BaseViewModel, new()
    {

        public abstract TOut Convert(TSource data);
        public abstract TSource ReverseConvert(TOut data);

        public virtual List<TOut> Convert(List<TSource> data)
        {
            var result = new List<TOut>();

            data.ForEach(itm =>
            {
                result.Add(Convert(itm));
            });

            return result;
        }
        public virtual List<TSource> ReverseConvert(List<TOut> data)
        {
            var result = new List<TSource>();

            data.ForEach(itm =>
            {
                result.Add(ReverseConvert(itm));
            });

            return result;
        }
        public virtual TSource ReverseConvert(string id, Guid PrivateOwnerId)
        {
            return new TSource
            {
                Number_ID = 0,
                Id = Guid.Parse(id),

                PrivateLabelOwner = new Principal { Id = PrivateOwnerId },
            };
        }


        /// <summary>
        ///  Proxy factory
        ///  Codesmell: this method could be optimized or replaced with IoC  in order to resolve proxy faster.
        /// </summary>
        /// <returns>IAnatoliProxy</returns>
        public static IAnatoliProxy<TSource, TOut> Create(string FullName = "")
        {
            var interfaceType = typeof(IAnatoliProxy<TSource, TOut>);

            var list = AppDomain.CurrentDomain.GetAssemblies()
                               .SelectMany(x => x.GetTypes())
                               .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                               .Select(x => Activator.CreateInstance(x));
            object proxy;
            if (FullName != null && FullName != "")
                proxy = list.First(p => p.GetType().FullName == FullName);
            else
                proxy = list.First();

            return (IAnatoliProxy<TSource, TOut>)proxy;
        }
    }
}
